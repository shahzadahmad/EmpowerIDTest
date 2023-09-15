# Triggering Azure Data Factory Pipeline from Console/Windows Application (C#)
# Make Search Data into Cognitive Search from Console/Windows Application (C#)

Azure Data Factory mostly use for ETL activity which has proper schedule to run on the stipulated time in batch mode, but in some of scenario it is required on call adf pipeline on demand. Trigger point could be anything like Console Application, windows Services or even web apis. In this article I have created a console application that first call already created ADF pipeline for refreshing/updating data into Search Index. And in Second Step we make search into Cognitive Search.

# Prerequisite

## Azure Details
- ResourceGroupName = "xxxx";
- DataFactoryName = "xxxx";
- PipeLineName = "xxxx";
- SearchIndexName = "xxxx";
- subscriptionId = "xxxx";
- tenantId = "xxxx";
- clientId = "xxxx";
- clientSecret = "xxxx";
- searchQueryApiKey = "xxxx";
- windowsManagementUri = "https://management.core.windows.net/";
- activeDirectoryEndpoint = "https://login.windows.net/";
- searchServiceEndPoint = "https://MySearchService.search.windows.net";

## .Net Supporting Library 
- Azure.Search.Documents;
- Microsoft.Azure.Management.DataFactory;
- Microsoft.IdentityModel.Clients.ActiveDirectory;
- Microsoft.Rest.ClientRuntime;
- Microsoft.Rest.ClientRuntime.Azure;
- NLog.Config;

