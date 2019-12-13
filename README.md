# Dapr-Microservice-Template
A Microservice Template for visual studio using Dapr.io and Kubernetes

### Introduction ###
This VS template makes it easy to create microservices that can be deployed as Dapr applications into a Kubernetes Cluster.
Includes a VSIX project to create a VS Extention installer.

Available from the Visual Studio Marketplace : https://marketplace.visualstudio.com/items?itemName=IgnitionGroup.ignDaprMicroservice

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


