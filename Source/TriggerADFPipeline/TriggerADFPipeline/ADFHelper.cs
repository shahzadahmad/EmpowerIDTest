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

        private readonly string tenantId = "1363ebc4-b673-4c1d-b408-71fb8624a0d1";
        private readonly string clientId = "4d0ff30d-bcdd-449b-9eeb-c06d93fe8961";
        private readonly string clientSecret = "uDd8Q~J_h74DwFnao70aA5ZT.hjMkJGzw28uCcKb";
        private readonly string subscriptionId = "b19f4ee4-a01e-4706-8a8c-96558113ac54";
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
                Console.WriteLine("Creating pipeline run...");
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
