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
        /// <summary>
        /// data member
        /// </summary>
        public IDataFactoryManagementClient client;

        /// <summary>
        /// Logger variable
        /// </summary>
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// configuration variables
        /// </summary>
        private string applicationId = ConfigurationManager.AppSettings["applicationId"];
        private string clientSecret = ConfigurationManager.AppSettings["clientSecret"];
        private string subscriptionId = ConfigurationManager.AppSettings["subscriptionId"];
        private string tenantID = ConfigurationManager.AppSettings["tenantID"];
        private readonly string activeDirectoryEndpoint = ConfigurationManager.AppSettings["activeDirectoryEndpoint"];
        private readonly string windowsManagementUri = ConfigurationManager.AppSettings["windowsManagementUri"];

        /// <summary>
        /// Constructor
        /// </summary>
        public pipeline()
        {
            create_adf_client();
        }

        /// <summary>
        /// creating ADF client.
        /// </summary>
        public void create_adf_client()
        {
            try
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
            catch
            {                
                throw;
            }
        }        

        /// <summary>
        /// Method to run ADF pipeline activity.
        /// </summary>
        /// <param name="resourceGroup"></param>
        /// <param name="dataFactoryName"></param>
        /// <param name="pipelineName"></param>
        public void StartPipeline(string resourceGroup, string dataFactoryName, string pipelineName)
        {
            try
            {
                // Create a Pipeline Run  
                Console.WriteLine("Product Cognitive Search - Refreshing Data Activity");
                Console.WriteLine("======================================================");
                logger.Info("Creating pipeline run...");
                var runResponse = client.Pipelines.CreateRunWithHttpMessagesAsync(resourceGroup, dataFactoryName, pipelineName).Result.Body;
                logger.Info("Pipeline run ID: " + runResponse.RunId);

                // Monitor the Pipeline Run  
                logger.Info("Checking Pipeline Run Status...");
                PipelineRun pipelineRun;
                while (true)
                {
                    pipelineRun = client.PipelineRuns.Get(resourceGroup, dataFactoryName, runResponse.RunId);
                    logger.Info("Status: " + pipelineRun.Status);
                    if (pipelineRun.Status == "InProgress")
                        System.Threading.Thread.Sleep(15000);
                    else
                        break;
                }

                // Check the Cognitive Search Data Update Activity Run Details  
                logger.Info("Checking cognitive search data update activity run details...");
                if (pipelineRun.Status == "Succeeded")
                {
                    logger.Info("Cognitive Search Data Update Activity Succeeded!");
                }
                else
                {
                    logger.Info("Cognitive Search Data Update Activity Failed!");
                }

                Console.WriteLine("\nPress any key to continue search activity...");
                Console.ReadKey();
            }
            catch
            {                
                throw;
            }
        }
    }
}
