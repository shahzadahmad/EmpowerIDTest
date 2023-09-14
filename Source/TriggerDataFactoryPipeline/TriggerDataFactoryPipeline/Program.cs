using System;
using System.Configuration;

namespace TriggerDataFactoryPipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            var runPipeline = new pipeline();
            string resourceGroup = ConfigurationManager.AppSettings["resourceGroup"];
            string dataFactoryName = ConfigurationManager.AppSettings["dataFactoryName"];
            string pipelineName = ConfigurationManager.AppSettings["pipelineName"];

            //Trigger the Data Factory Pipeline for push data into search index.
            runPipeline.StartPipeline(resourceGroup, dataFactoryName, pipelineName);

            //Search for products using all available parameters.
            Console.WriteLine("Please enter search query:");
            string searchQuery = Console.ReadLine();

            var result = new AzureSearch().Search(searchQuery).Result;
            foreach (var r in result)
                Console.WriteLine(r);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
