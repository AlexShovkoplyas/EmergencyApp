@description('The principal ID that will be granted access (managed identity in prod, current user in local dev).')
param principalId string = ''

@description('The type of the principal: ServicePrincipal for managed identity, User for local dev.')
param principalType string = 'ServicePrincipal'

// Name is generated from the resource group to guarantee global uniqueness across envs
var acsName = 'acs-${uniqueString(resourceGroup().id)}'

resource communicationService 'Microsoft.Communication/communicationServices@2023-04-01' = {
  name: acsName
  location: 'global'
  properties: {
    dataLocation: 'United States'
  }
}

// Contributor role — required for ACS data-plane access via Azure AD / DefaultAzureCredential
var contributorRoleId = 'b24988ac-6180-42a0-ab88-20f7382dd24c'

resource roleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (!empty(principalId)) {
  name: guid(communicationService.id, principalId, contributorRoleId)
  scope: communicationService
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', contributorRoleId)
    principalId: principalId
    principalType: principalType
  }
}

@description('The ACS endpoint passed to the web app for DefaultAzureCredential usage.')
output endpoint string = 'https://${communicationService.properties.hostName}'
