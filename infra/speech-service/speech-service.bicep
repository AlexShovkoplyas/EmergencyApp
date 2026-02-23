@description('The location used for all deployed resources')
param location string = resourceGroup().location

@description('The principal ID that will be granted access (managed identity in prod).')
param principalId string = ''

@description('The type of the principal: ServicePrincipal for managed identity.')
param principalType string = 'ServicePrincipal'

var speechName = 'speech-${uniqueString(resourceGroup().id)}'

// Cognitive Services Speech User role — allows data-plane STT/TTS operations via AAD tokens
var speechUserRoleId = 'f2dc8367-1007-4938-bd23-fe263f013447'

resource speechService 'Microsoft.CognitiveServices/accounts@2023-05-01' = {
  name: speechName
  location: location
  kind: 'SpeechServices'
  sku: {
    name: 'S0'
  }
  properties: {
    publicNetworkAccess: 'Enabled'
    customSubDomainName: speechName
  }
}

resource roleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (!empty(principalId)) {
  name: guid(speechService.id, principalId, speechUserRoleId)
  scope: speechService
  properties: {
    roleDefinitionId: subscriptionResourceId('Microsoft.Authorization/roleDefinitions', speechUserRoleId)
    principalId: principalId
    principalType: principalType
  }
}

@description('Subscription key used server-side to exchange for a short-lived STS token for the browser Speech SDK.')
output key string = speechService.listKeys().key1

@description('The Azure region of the Speech Service resource (used as region param in the JS SDK).')
output location string = speechService.location
