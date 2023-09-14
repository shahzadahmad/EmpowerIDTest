using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using System;

namespace TriggerDataFactoryPipeline
{
    class pipeline
    {
        public IDataFactoryManagementClient client;

        private string applicationId = "4d0ff30d-bcdd-449b-9eeb-c06d93fe8961";
        private string clientSecret = "nvN8Q~SByqgEVlJuskc2gJdNUKyJLtOHACGMyboc";
        private string subscriptionId = "b19f4ee4-a01e-4706-8a8c-96558113ac54";
        private string tenantID = "1363ebc4-b673-4c1d-b408-71fb8624a0d1";
        private readonly string activeDirectoryEndpoint = "https://login.windows.net/";
        private readonly string windowsManagementUri = "https://management.core.windows.net/";

        public void create_adf_client()
        {
            // Authenticate and create a data factory management client
            var authority = new Uri(new Uri(activeDirectoryEndpoint), this.tenantID);
            var authenticationContext = new AuthenticationContext(authority.AbsoluteUri);
            var clientCredential = new ClientCredential(applicationId, clientSecret);
            
            var result = authenticationContext.AcquireTokenAsync(windowsManagementUri, clientCredential).Result;            

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
            try
            {
                // Create a Pipeline Run  
                Console.WriteLine("Creating pipeline run...");
                var runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(resourceGroup, dataFactoryName, pipelineName).Result.Body;
                Console.WriteLine("Pipeline run ID: " + runResponse.RunId);

                // Monitor the Pipeline Run  
                Console.WriteLine("Checking Pipeline Run Status...");
                PipelineRun pipelineRun;
                while (true)
                {
                    pipelineRun = client.PipelineRuns.Get(resourceGroup, dataFactoryName, runResponse.RunId);
                    Console.WriteLine("Status: " + pipelineRun.Status);
                    if (pipelineRun.Status == "InProgress")
                        System.Threading.Thread.Sleep(15000);
                    else
                        break;
                }

                // Check the Copy Activity Run Details  
                Console.WriteLine("Checking copy activity run details...");
                if (pipelineRun.Status == "Succeeded")
                {
                    Console.WriteLine("Copy Activity Succeeded!");
                }
                else
                {
                    Console.WriteLine("Copy Activity Failed!");
                }
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
