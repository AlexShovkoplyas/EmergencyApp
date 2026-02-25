@description('Azure region — required by Aspire provisioner but unused (ACS is always global).')
param location string = 'global'

@description('The principal ID that will be granted access (managed identity in prod, current user in local dev).')
param principalId string = ''

@description('The type of the principal: ServicePrincipal for managed identity, User for local dev.')
param principalType string = 'ServicePrincipal'

// Name is generated from the resource group to guarantee global uniqueness across envs
var acsName = 'acs-${uniqueString(resourceGroup().id)}'
var emailServiceName = 'email-${uniqueString(resourceGroup().id)}'

resource emailService 'Microsoft.Communication/emailServices@2023-03-31' = {
  name: emailServiceName
  location: 'global'
  properties: {
    dataLocation: 'United States'
  }
}

resource emailDomain 'Microsoft.Communication/emailServices/domains@2023-03-31' = {
  parent: emailService
  name: 'AzureManagedDomain'
  location: 'global'
  properties: {
    domainManagement: 'AzureManaged'
    userEngagementTracking: 'Disabled'
  }
}

resource communicationService 'Microsoft.Communication/communicationServices@2023-04-01' = {
  name: acsName
  location: 'global'
  properties: {
    dataLocation: 'United States'
    linkedDomains: [
      emailDomain.id
    ]
  }
}

// "Communication and Email Service Owner" — grants data-plane access (send email / SMS)
// via Azure AD / DefaultAzureCredential. The generic Contributor role is management-plane
// only and results in 401 on data-plane calls.
var commEmailOwnerRoleId = '09976791-48a7-449e-bb21-39d1a415f350'

resource roleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (!empty(principalId)) {
  name: guid(communicationService.id, principalId, commEmailOwnerRoleId)
  scope: communicationService
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', commEmailOwnerRoleId)
    principalId: principalId
    principalType: principalType
  }
}

@description('The ACS endpoint passed to the web app for DefaultAzureCredential usage.')
output endpoint string = 'https://${communicationService.properties.hostName}'

@description('Primary connection string (includes access key) for data-plane auth.')
output connectionString string = communicationService.listKeys().primaryConnectionString

@description('The sender email address from the Azure Managed Domain.')
output senderAddress string = 'DoNotReply@${emailDomain.properties.fromSenderDomain}'
