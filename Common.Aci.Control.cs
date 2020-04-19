using System.Threading.Tasks;
using Microsoft.Azure.Management.Fluent;

using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
namespace X5ff.Aci.Control.Common
{

  public class ContainerInstanceControl
  {
    private readonly IAzure azure;

    public ContainerInstanceControl()
    {
      var credentials = SdkContext.AzureCredentialsFactory.FromSystemAssignedManagedServiceIdentity(MSIResourceType.AppService, AzureEnvironment.AzureGlobalCloud);
      this.azure = Azure.Configure().Authenticate(credentials).WithDefaultSubscription();
    }

    public async Task Start(string name, string resourceGroup)
    {
      await this.azure.ContainerGroups.StartAsync(resourceGroup, name);
    }


    public async Task Stop(string name, string resourceGroup)
    {
      var group = await this.azure.ContainerGroups.GetByResourceGroupAsync(resourceGroup, name);
      await group?.StopAsync();
    }

    public async Task<string?> GetStatus(string name, string resourceGroup)
    {
      var group = await this.azure.ContainerGroups.GetByResourceGroupAsync(resourceGroup, name);

      if (group == null)
      {
        return null;
      }
      else
      {
        return group.State;
      }
    }
  }
}
