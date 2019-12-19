# Dapr-Microservice-Template
A Microservice Template for visual studio using Dapr.io and Kubernetes

### Introduction ###
This VS template makes it easy to create microservices that can be deployed as Dapr applications into a Kubernetes Cluster.
Includes a .VSIX project to create a VS Extention installer.

Available from the Visual Studio Marketplace : https://marketplace.visualstudio.com/items?itemName=IgnitionGroup.DaprMicroService

### Prerequisites
* Visual Studio 2017 and above
* .NET Core 3.0

## Getting Started
Install the Dapr microservice template from the link above.
Once installed click on Extensions -> Manage Extensions from the menu in visual Studio.
Under installed extentions you should find the Extension Installed. 

![VS Dapr template Extension](Screenshots/Extension.PNG)

To create a new project in visual studio using the template
* Add a new project by selecting File -> New Project.
* Select Dapr Microservice Template for the project type and fill in the project information. Click on Create
* A custom prompt will ask for the microservice application name and Dapr app port number to be used by Dapr on deployment into the Kubernetes cluster.

The information captured in the customer prompt will replace the template variables in the deployment yaml file found in the Deploy folder.


*annotations:
        dapr.io/enabled: "true"
        dapr.io/id: "$daprAppName$"
        dapr.io/port: "$daprport$"*

A new project will be created as below

![Microservice Project](Screenshots/newproject.PNG)

## DaprMicroserviceTemplate project ##
<details>
  <summary>Building a Docker Image</summary>
  <p>DockerFileCI - Contains the commands to build a Docker image for the microservice.
</details>

<details>
  <summary>YAML Files</summary>
  <p>DaprMicroServiceTemplatedeploy.yaml - Contains the information for deployment into kubernetes cluster</p>
  <p>Azure-Pipelines.yaml - File used to Create the microservice Deployment pipeline for Micrsoft Azure Dev Ops</p>
</details>

## TemplateInstaller - VSIX Project ##
<p>This project will create a .VSIX installer for the Microservice Template. </p>

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
