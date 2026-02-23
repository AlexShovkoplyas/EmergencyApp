console.log('[Speech] ChatInput.razor.js module loaded');

let dotNetRef;
let recognizer;
let sdkLoadPromise = null;
let speechConfig;
let audioConfig;

export async function init(elem, dotNetReference) {
    console.log('[Speech] init called');
    dotNetRef = dotNetReference;

    elem.focus();

    elem.addEventListener('input', () => resizeToFit(elem));
    afterPropertyWritten(elem, 'value', () => resizeToFit(elem));

    elem.addEventListener('keydown', (e) => {
        if (e.key === 'Enter' && !e.shiftKey) {
            e.preventDefault();
            elem.dispatchEvent(new CustomEvent('change', { bubbles: true }));
            elem.closest('form').dispatchEvent(new CustomEvent('submit', { bubbles: true, cancelable: true }));
        }
    });

    return await checkSpeechAvailable();
}

async function checkSpeechAvailable() {
    if (!navigator.mediaDevices?.getUserMedia) return false;

    try {
        const res = await fetch('/api/speech/available');
        if (!res.ok) return false;
        return await res.json() === true;
    } catch {
        return false;
    }
}

// language: a BCP-47 tag like 'en-US' or 'uk-UA', or null/'' for auto-detect.
export async function startListening(language) {
    console.log(`[Speech] startListening called with language: '${language}'`);
    try {
        const sdk = await loadSpeechSDK();
        console.log(`[Speech] SDK loaded. Version: ${sdk.SpeechRecognizer.Action}`); // Just accessing something to check

        const res = await fetch('/api/speech/token');
        if (!res.ok) throw new Error(`Failed to fetch speech token (HTTP ${res.status})`);

        const { token, region } = await res.json();
        console.log(`[Speech] Token fetched for region: ${region}`);

        speechConfig = sdk.SpeechConfig.fromAuthorizationToken(token, region);
        audioConfig = sdk.AudioConfig.fromDefaultMicrophoneInput();

        // Explicitly set the language property
        if (language && language.trim() !== "") {
            console.log(`[Speech] Configuring explicit language: ${language}`);
            speechConfig.speechRecognitionLanguage = language;
            recognizer = new sdk.SpeechRecognizer(speechConfig, audioConfig);
        } else {
            console.log('[Speech] Configuring auto-detect language');
            const autoDetectConfig = sdk.AutoDetectSourceLanguageConfig.fromLanguages([
                'en-US', 'ru-RU', 'uk-UA', 'de-DE',
            ]);
            recognizer = sdk.SpeechRecognizer.FromConfig(speechConfig, autoDetectConfig, audioConfig);
        }

        recognizer.recognizing = (s, e) => {
             console.log(`[Speech] Recognizing: ${e.result.text}`);
        };

        recognizer.recognized = (s, e) => {
            if (e.result.reason === sdk.ResultReason.RecognizedSpeech && e.result.text) {
                console.log(`[Speech] Recognized: ${e.result.text}`);
                dotNetRef.invokeMethodAsync('OnSpeechRecognized', e.result.text);
            } else if (e.result.reason === sdk.ResultReason.NoMatch) {
                console.log('[Speech] No match found.');
            }
        };

        recognizer.canceled = (s, e) => {
            console.error(`[Speech] Canceled: ${e.reason}`);
            if (e.reason === sdk.CancellationReason.Error) {
                console.error(`[Speech] Error details: ${e.errorDetails}`);
                console.error(`[Speech] Error code: ${e.errorCode}`);
            }
            dotNetRef.invokeMethodAsync('OnSpeechStopped');
            stopListening();
        };

        recognizer.sessionStopped = (s, e) => {
            console.log('[Speech] Session stopped.');
            dotNetRef.invokeMethodAsync('OnSpeechStopped');
            stopListening();
        };

        console.log('[Speech] Starting continuous recognition...');
        await new Promise((resolve, reject) => {
            recognizer.startContinuousRecognitionAsync(
                () => {
                    console.log('[Speech] Continuous recognition started successfully');
                    resolve();
                },
                (err) => {
                    console.error('[Speech] Failed to start continuous recognition:', err);
                    reject(err);
                }
            );
        });
    } catch (error) {
        console.error('[Speech] Error in startListening:', error);
        throw error;
    }
}

export function stopListening() {
    console.log('[Speech] stopListening called');
    if (recognizer) {
        const r = recognizer;
        recognizer = null; // Prevent re-entry
        
        r.stopContinuousRecognitionAsync(
            () => {
                console.log('[Speech] Recognition stopped.');
                r.close();
                
                if (speechConfig) {
                    speechConfig.close();
                    speechConfig = null;
                }
                if (audioConfig) {
                    audioConfig.close();
                    audioConfig = null;
                }
            },
            (error) => {
                console.error('[Speech] Error stopping recognition:', error);
                r.close();
            }
        );
    }
}

function loadSpeechSDK() {
    if (sdkLoadPromise) return sdkLoadPromise;

    sdkLoadPromise = new Promise((resolve, reject) => {
        if (window.SpeechSDK) {
            resolve(window.SpeechSDK);
            return;
        }

        const script = document.createElement('script');
        script.src = '/lib/speech/microsoft.cognitiveservices.speech.sdk.bundle-min.js';
        script.onload = () => {
            if (window.SpeechSDK) {
                console.log('[Speech] SDK script loaded');
                resolve(window.SpeechSDK);
            } else {
                sdkLoadPromise = null;
                reject(new Error('Speech SDK script loaded but window.SpeechSDK is not defined'));
            }
        };
        script.onerror = () => {
            sdkLoadPromise = null;
            reject(new Error('Failed to load Azure Speech SDK from /lib/speech/'));
        };
        document.head.appendChild(script);
    });

    return sdkLoadPromise;
}

function resizeToFit(elem) {
    const lineHeight = parseFloat(getComputedStyle(elem).lineHeight);
    elem.rows = 1;
    const numLines = Math.ceil(elem.scrollHeight / lineHeight);
    elem.rows = Math.min(5, Math.max(1, numLines));
}

function afterPropertyWritten(target, propName, callback) {
    const descriptor = getPropertyDescriptor(target, propName);
    Object.defineProperty(target, propName, {
        get: function () {
            return descriptor.get.apply(this, arguments);
        },
        set: function () {
            const result = descriptor.set.apply(this, arguments);
            callback();
            return result;
        }
    });
}

function getPropertyDescriptor(target, propertyName) {
    return Object.getOwnPropertyDescriptor(target, propertyName)
        || getPropertyDescriptor(Object.getPrototypeOf(target), propertyName);
}
