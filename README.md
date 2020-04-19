# Control your Container Instance with Azure Functions

This is the start/stop/status code for managing the [Factorio server on Azure Container Instances](https://github.com/celaus/factorio-aci-terraform). It provides 3 Functions (on a consumption plan):

1. /api/start   - Starts the container instance
1. /api/stop    - Stops the container instance
1. /api/status  - Fetches the current status as a simple string (or HTTP/400 in case of errors)

The Function authenticates via a managed identity role assignment for Contributor access. Check out the Terraform deployment to find out more. For managing other ACI instances, be sure to set `ACI_NAME` and `ACI_RESOURCE_GROUP` to make it connect properly (and give the Function the Contributor role using the portal or CLI).

# License

MIT