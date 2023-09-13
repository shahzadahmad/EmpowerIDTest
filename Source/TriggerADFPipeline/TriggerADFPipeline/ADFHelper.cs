using Microsoft.Azure.Management.DataFactory;
using Microsoft.Rest;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;

namespace TriggerADFPipeline
{
    public class ADFHelper
    {
        private TokenCredentials _tokenCredential;
        private DataFactoryManagementClient _dataFactoryClient;

        private readonly string tenantId = "xxxx";
        private readonly string clientId = "xxxx";
        private readonly string clientSecret = "xxxx";
        private readonly string subscriptionId = "xxxx";
        private readonly string windowsManagementUri = "https://management.core.windows.net/";
        private readonly string activeDirectoryEndpoint = "https://login.windows.net/";
        public ADFHelper()
        {
            AuthenticateUser();
            SetupClient();
        }

        private void AuthenticateUser()
        {
            var authority = new Uri(new Uri(activeDirectoryEndpoint), this.tenantId);
            var context = new AuthenticationContext(authority.AbsoluteUri);
            var credential = new ClientCredential(this.clientId, this.clientSecret);

            _tokenCredential = new TokenCredentials(context.AcquireTokenAsync(windowsManagementUri, credential).Result.AccessToken);
        }
        private void SetupClient()
        {
            _dataFactoryClient = new DataFactoryManagementClient(_tokenCredential) { SubscriptionId = subscriptionId };
        }

        
        public async Task<string> TriggerAdfAsync(AzureDataFactoryModel azureDataFactoryModel)
        {
            try
            {
                
                var runResponse = await _dataFactoryClient.Pipelines.CreateRunWithHttpMessagesAsync(
                                                           azureDataFactoryModel.ResourceGroupName,
                                                           azureDataFactoryModel.FactoryName,
                                                          azureDataFactoryModel.PipeLineName,
                                                           parameters: azureDataFactoryModel.Parameters
                ).ConfigureAwait(false);
              
                return runResponse.Body.RunId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

    }
}
