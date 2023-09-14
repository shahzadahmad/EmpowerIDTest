using Microsoft.Azure.Management.DataFactory;
using Microsoft.Azure.Management.DataFactory.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using System;
using System.Configuration;

namespace TriggerDataFactoryPipeline
{
    class pipeline
    {
        public IDataFactoryManagementClient client;

        private string applicationId = ConfigurationManager.AppSettings["applicationId"];
        private string clientSecret = ConfigurationManager.AppSettings["clientSecret"];
        private string subscriptionId = ConfigurationManager.AppSettings["subscriptionId"];
        private string tenantID = ConfigurationManager.AppSettings["tenantID"];
        private readonly string activeDirectoryEndpoint = ConfigurationManager.AppSettings["activeDirectoryEndpoint"];
        private readonly string windowsManagementUri = ConfigurationManager.AppSettings["windowsManagementUri"];

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

                // Check the Cognitive Search Data Update Activity Run Details  
                Console.WriteLine("Checking cognitive search data update activity run details...");
                if (pipelineRun.Status == "Succeeded")
                {
                    Console.WriteLine("Cognitive Search Data Update Activity Succeeded!");
                }
                else
                {
                    Console.WriteLine("Cognitive Search Data Update Activity Failed!");
                }

                Console.WriteLine("\nPress any key to continue search item...");
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
