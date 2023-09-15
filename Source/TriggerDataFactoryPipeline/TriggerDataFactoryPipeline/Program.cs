using NLog;
using System;
using System.Configuration;

namespace TriggerDataFactoryPipeline
{
    class Program
    {
        /// <summary>
        /// Logger variable
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        static void Main(string[] args)
        {
            try
            {
                var runPipeline = new pipeline();
                string resourceGroup = ConfigurationManager.AppSettings["resourceGroup"];
                string dataFactoryName = ConfigurationManager.AppSettings["dataFactoryName"];
                string pipelineName = ConfigurationManager.AppSettings["pipelineName"];

                //Trigger the Data Factory Pipeline for push data into search index.
                runPipeline.StartPipeline(resourceGroup, dataFactoryName, pipelineName);

                //Search for products using all available parameters.
                Console.WriteLine("\n");
                Console.WriteLine("Product Search - The search method for products allows the user to search using all available parameters");
                Console.WriteLine("--------------------------------------------------------------------------------------------------------");                
                Console.WriteLine("Please enter your search query:");
                string searchQuery = Console.ReadLine();

                 Console.WriteLine("\n");
                Console.WriteLine("Generating Search Result...\n");                

                var result = new AzureSearch().Search(searchQuery).Result;
                foreach (var r in result)
                    Console.WriteLine(r);

                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                logger.Error(ex);
                Console.Read();
            }
        }
    }
}
