using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using X5ff.Aci.Control.Common;
using System;

namespace X5ff.Aci.Control
{

  public static class Start
  {

    [FunctionName("start")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        ILogger log)
    {
      var aci = Environment.GetEnvironmentVariable("ACI_NAME");
      var resourceGroup = Environment.GetEnvironmentVariable("ACI_RESOURCE_GROUP");
      
      log.LogInformation($"Starting ACI {aci}@{resourceGroup}");
      var control = new ContainerInstanceControl();
      await control.Start(aci, resourceGroup);

      return new OkResult();
    }
  }
}
