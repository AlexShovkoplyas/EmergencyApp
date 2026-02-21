using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmergencyApp.Web.Areas.Identity.Pages.Account;

public class ExternalLoginModel : PageModel
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ILogger<ExternalLoginModel> _logger;

    public ExternalLoginModel(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        ILogger<ExternalLoginModel> logger)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
    }

    // Entry point: redirect to external provider
    public IActionResult OnGet() => RedirectToPage("/");

    public IActionResult OnPost(string provider, string? returnUrl = null)
    {
        var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new { returnUrl });
        var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
        return new ChallengeResult(provider, properties);
    }

    // Callback from external provider
    public async Task<IActionResult> OnGetCallbackAsync(string? returnUrl = null, string? remoteError = null)
    {
        returnUrl ??= Url.Content("~/");

        if (remoteError != null)
        {
            _logger.LogWarning("Error from external provider: {Error}", remoteError);
            return RedirectToPage("/");
        }

        var info = await _signInManager.GetExternalLoginInfoAsync();
        if (info is null)
        {
            _logger.LogWarning("Could not load external login info.");
            return RedirectToPage("/");
        }

        // Try signing in with an existing external login link
        var signInResult = await _signInManager.ExternalLoginSignInAsync(
            info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);

        if (signInResult.Succeeded)
        {
            _logger.LogInformation("{Provider} login succeeded for {Key}.", info.LoginProvider, info.ProviderKey);
            return LocalRedirect(returnUrl);
        }

        // No existing link — auto-register using the email from the external provider
        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(email))
        {
            _logger.LogWarning("No email claim received from {Provider}.", info.LoginProvider);
            return RedirectToPage("/");
        }

        // Reuse existing account with same email, or create a new one
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            user = new IdentityUser { UserName = email, Email = email, EmailConfirmed = true };
            var createResult = await _userManager.CreateAsync(user);
            if (!createResult.Succeeded)
            {
                _logger.LogError("Failed to create user for {Email}: {Errors}",
                    email, string.Join(", ", createResult.Errors.Select(e => e.Description)));
                return RedirectToPage("/");
            }
        }

        var addLoginResult = await _userManager.AddLoginAsync(user, info);
        if (!addLoginResult.Succeeded)
        {
            _logger.LogError("Failed to add external login for {Email}: {Errors}",
                email, string.Join(", ", addLoginResult.Errors.Select(e => e.Description)));
            return RedirectToPage("/");
        }

        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);
        _logger.LogInformation("Auto-registered and signed in {Email} via {Provider}.", email, info.LoginProvider);
        return LocalRedirect(returnUrl);
    }
}
