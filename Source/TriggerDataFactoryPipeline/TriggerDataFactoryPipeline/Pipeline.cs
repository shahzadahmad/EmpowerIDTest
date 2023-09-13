using Microsoft.Azure.Management.DataFactory;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using System;

namespace TriggerDataFactoryPipeline
{
    class pipeline
    {
        public IDataFactoryManagementClient client;

        private string applicationId = "4d0ff30d-bcdd-449b-9eeb-c06d93fe8961";
        private string clientSecret = "uDd8Q~J_h74DwFnao70aA5ZT.hjMkJGzw28uCcKb";
        private string subscriptionId = "b19f4ee4-a01e-4706-8a8c-96558113ac54";
        private string tenantID = "1363ebc4-b673-4c1d-b408-71fb8624a0d1";
        private readonly string activeDirectoryEndpoint = "https://login.windows.net/";
        private readonly string windowsManagementUri = "https://management.azure.com/";

        public void create_adf_client()
        {
            // Authenticate and create a data factory management client
            var authority = new Uri(new Uri(activeDirectoryEndpoint), this.tenantID);
            var context = new AuthenticationContext(authority.AbsoluteUri);
            var ClientCredential = new ClientCredential(applicationId, clientSecret);
            
            var result = context.AcquireTokenAsync(windowsManagementUri, ClientCredential).Result;

            var cred = new TokenCredentials(result.AccessToken);
            client = new DataFactoryManagementClient(cred)
            {
                SubscriptionId = subscriptionId
            };
        }

        public pipeline()
        {
            create_adf_client();
        }

        public void StartPipeline(string resourceGroup, string dataFactoryName, string pipelineName)
        {
            Console.WriteLine("Creating pipeline run...");

            var runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(resourceGroup, dataFactoryName, pipelineName).Result.Body;

            Console.WriteLine("Pipeline run ID: " + runResponse.RunId);
        }
    }
}
