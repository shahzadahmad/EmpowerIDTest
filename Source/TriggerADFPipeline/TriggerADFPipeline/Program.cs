using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TriggerADFPipeline
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Calling ADF pipeline from C#....");

            ADFHelper _azureDataFactory = new ADFHelper();
            AzureDataFactoryModel azureDataFactoryModel = new AzureDataFactoryModel();
            azureDataFactoryModel.ResourceGroupName = "empowerID_TestRG";
            azureDataFactoryModel.FactoryName = "empoweridtestdb-df";
            azureDataFactoryModel.PipeLineName = "pipelineCopyDataToProductCognitiveSearch";

            //azureDataFactoryModel.Parameters = new Dictionary<string, object>
            //{
            //    { "P1", "parameter 1"},
            //    { "P2", "parameter 2"},
            //    { "P3", "parameter 3"}
            //};

            var runId = _azureDataFactory.TriggerAdfAsync(azureDataFactoryModel).ConfigureAwait(false);
            Console.WriteLine($"Pipeline run ID: {runId}");
        }

    }
}
