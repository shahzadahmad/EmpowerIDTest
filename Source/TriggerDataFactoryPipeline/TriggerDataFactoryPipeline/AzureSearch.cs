using Azure;
using Azure.Search.Documents;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text.Json;
using System.Threading.Tasks;

namespace TriggerDataFactoryPipeline
{
    public class AzureSearch
    {
        /// <summary>
        /// Method to search query into Cognitive Serach index.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> Search(string query)
        {
            SearchClient searchClient = CreateSearchClientForQueries(ConfigurationManager.AppSettings["searchIndexName"]);
            SearchOptions options = new SearchOptions()
            {
                IncludeTotalCount = true,
                //Filter = "",
                //Size = pageSize,
                //Skip = (page - 1) * pageSize
            };
            var results = await searchClient.SearchAsync<Product>(query, options);

            List<string> documents = new List<string>();
            Console.WriteLine(results.Value.TotalCount);
            foreach (var s in results.Value.GetResults())
            {
                documents.Add(JsonSerializer.Serialize<Product>(s.Document));
            }
            return documents;
        }

        /// <summary>
        /// Method to create search client object.
        /// </summary>
        /// <param name="indexName"></param>
        /// <returns></returns>
        private static SearchClient CreateSearchClientForQueries(string indexName)
        {
            string searchServiceEndPoint = ConfigurationManager.AppSettings["searchServiceEndPoint"];
            string queryApiKey = ConfigurationManager.AppSettings["searchQueryApiKey"];

            SearchClient searchClient = new SearchClient(new Uri(searchServiceEndPoint), indexName, new AzureKeyCredential(queryApiKey));
            return searchClient;
        }
    }
}
