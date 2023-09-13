using System;
using System.Collections.Generic;
using System.Text;

namespace TriggerADFPipeline
{
    public class AzureDataFactoryModel
    {

        public AzureDataFactoryModel()
        {
            if (Parameters == null)
            {
                Parameters = new Dictionary<string, object>();
            }
        }
        public Dictionary<string, object> Parameters { get; set; }

        public string ResourceGroupName { get; set; }
        public string FactoryName { get; set; }
        public string PipeLineName { get; set; }

    }
}
