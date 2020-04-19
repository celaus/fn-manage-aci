using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.Management.Fluent;
using X5ff.Aci.Control.Common;

namespace X5ff.Aci.Control
{
  public static class Status
  {

    [FunctionName("status")]
    public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
        ILogger log)
    {
      var aci = Environment.GetEnvironmentVariable("ACI_NAME");
      var resourceGroup = Environment.GetEnvironmentVariable("ACI_RESOURCE_GROUP");
      log.LogInformation($"Retrieving status for ACI {aci}@{resourceGroup}");

      var control = new ContainerInstanceControl();
      var status = await control.GetStatus(aci, resourceGroup);
      if (String.IsNullOrEmpty(status))
      {
        return new BadRequestResult();
      }
      else
      {
        return new OkObjectResult(status);
      }
    }
  }
}
