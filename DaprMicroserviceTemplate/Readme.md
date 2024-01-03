# Getting Started with Dapr microservices for .Net
The sample shows how to create a .Net API,integrate with Dapr and invoke its endpoints on the client application. 

## Prerequistes
* [.Net Core SDK 8.0](https://dotnet.microsoft.com/download)
* [Dapr CLI](https://github.com/dapr/cli)
* [Dapr DotNet SDK](https://github.com/dapr/dotnet-sdk)




 ## Running the Sample

 To run the service locally run this comment in $daprAppName$ directory:
 ```sh
 dapr run --port 3500 --app-id demo_app --app-port 3000 dotnet run
 ```

 The API application will listen on port 3000 for HTTP.

 ### Making Client calls.


Following curl call will return values on from the values controller 
(below calls on MacOs, Linux & Windows are exactly the same except for escaping quotes on Windows for curl)

On Linux, MacOS:
 ```sh
curl -X GET http://localhost:3500/v1.0/invoke/"$daprAppName$"/method/weatherforecast
 ```
 On Windows:
 ```sh
curl -X GET http://localhost:3500/v1.0/invoke/"$daprAppName$"/method/weatherforecast
 ```
 
 
 ## Creating an Azure Devops pipeline

 1. Create a pipeline from existing yaml file.(azure-pipelines.yml)
 2. Create two variable groups, Test and Prod. Populate with required variables as required by the azure-pipelines.yml file.
 3. Update the azure-pipelines.yml to reference the two new variable groups.
 4. Link the two variable groups to the pipeline. (Edit, Triggers, Variables, Link Variable Group)


