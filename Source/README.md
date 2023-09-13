# Triggering Azure Data Factory Pipeline from Console/Windows Application (C#)

Azure Data Factory mostly use for ETL activity which has proper schedule to run on the stipulated time in batch mode, but in some of scenario it is required on call adf pipeline on demand. Trigger point could be anything like Console Application, windows Services or even web apis. In this article I have created a console application that call already create ADF pipeline.

# Prerequisite

## Azure Details
- ResourceGroupName = "xxxx";
- FactoryName = "xxxx";
- PipeLineName = "xxxx";
- tenantId = "xxxx";
- clientId = "xxxx";
- clientSecret = "xxxx";
- subscriptionId = "xxxx";
- windowsManagementUri = "https://management.core.windows.net/";
- activeDirectoryEndpoint = "https://login.windows.net/";

## .Net Supporting Library 
- Microsoft.Azure.Management.DataFactory;
- Microsoft.Rest;
- Microsoft.IdentityModel.Clients.ActiveDirectory;
- System.Threading.Tasks;

