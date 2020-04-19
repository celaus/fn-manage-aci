using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Management.Fluent;
using X5ff.Aci.Control.Common;

namespace X5ff.Aci.Control
{

  public static class Stop
  {

    private static IAzure GetAzureContext()
    {
      IAzure azure = Azure.Authenticate("credentials.json").WithDefaultSubscription();
      var currentSubscription = azure.GetCurrentSubscription();
      return azure;
    }
    
    [FunctionName("stop")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        ILogger log)
    {

       var aci = Environment.GetEnvironmentVariable("ACI_NAME");
      var resourceGroup = Environment.GetEnvironmentVariable("ACI_RESOURCE_GROUP");
      
      log.LogInformation($"Stopping ACI {aci}@{resourceGroup}");
      var control = new ContainerInstanceControl();

      await control.Stop(aci, resourceGroup);
      return new OkResult();
    }
  }
}
