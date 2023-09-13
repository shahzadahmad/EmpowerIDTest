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

            runPipeline.StartPipeline(resourceGroup, dataFactoryName, pipelineName);
        }
    }
}
