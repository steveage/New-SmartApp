# New-SmartApp

New-SmartApp is a sample ASP.Net Core application that explains how to write Samsung SmarThings hub automation using the new SmartThings API. The automation provides a very basic functionality. All it does is allows user to select a contact sensor to log open and close events.

![Logged Event Screenshot](Screenshots/Log_Result.PNG?raw=true "Event logged in Azure Application Insights")

This application is based on the [Weather Color Light SmartApp](https://github.com/SmartThingsCommunity/weather-color-light-smartapp-nodejs)  Node.js sample referenced on the [Getting Started](https://smartthings.developer.samsung.com/docs/getting-started/automation.html) documentation section of [SmartThings Developers](https://smartthings.developer.samsung.com/docs/index.html) website.

The application is a webhook hosted in Azure that receives and sends requests to SmartThings cloud where the automation connector is registered.

The design of the application is kept simple. The intention here is to show the building blocks of establishing communication with SmartThings.

## Specifics
* The application is written using Visual Studio ASP.Net Core Web API project template.
* The applicaiton is based on .Net Core 2.2.
* The application is based on strongly typed SmartThings lifecycle objects located in Models subfolder.
* The application is hosted in Azure App Service.
* Logs and telemetry are collected in Azure Application Insights.
* Requests received by the application from SmartThings are authenticated with signature authentication scheme RSA-SHA256.
* Public key the application uses to verify requests is stored in Azure Key Vault secret. 
* Requests the application sends to SmartThings are protected with OAuth 2.0 bearer tokens.

## Prerequisites

Here is what you need to run and deploy the application.
* Visual Studio with .Net Core and Azure SDKs
* Samsung SmartThings Hub (any version should work)
* SmartThings app (not the SmartThings Classic) installed on your phone
* One IOT device - this project uses a contact sensor but anything should work. You can use a virtual device if you do not have a physical one.
* Microsoft Azure account (free account should work)
* Samsung SmartThings developer account
* Ngrok application to expose your app to SmartThings cloud in development environment

## Get the app
1. Type in the following in Bash
```
git clone https://github.com/steveage/New-SmartApp.git
```
2. Install Node dependencies
```
cd New-SmartApp/newsmartapp.smartappapi
npm install
```
## Starting the app
1. Open the solution from Visual Studio and run the application (F5).
2. Create a tunnel that will expose your application to SmartThings for testing and debugging.
Start Ngrok and enter the following command.
```
ngrok http 53869 -host-header="localhost:53869"
```
3. Copy the https address. You will need it in the next section.
![Ngrok Screenshot](Screenshots/Ngrok.PNG?raw=true "Ngrok")
## Register the app as automation
At this point your application is up and running but SmartThings does not know about it. In this section we will change that by creating an automation in SmartThings. The process is explained in the [Developer Workspace documentation](https://smartthings.developer.samsung.com/docs/workspace/tutorials/create-an-automation.html).
1. Log in the [Developer Workspace](https://smartthings.developer.samsung.com/workspace/).
2. Create automation project and open it.
3. Click on Automation under Develop section on the right.
4. Select the Webhook Endpoint, paste the https address from Ngrok and click Next.
![Developer Workspace Hosting](Screenshots/Developer_Workspace_Hosting.PNG?raw=true "Setting up automation hosting.")
5. Select read and update device scopes and click Next.
![Developer Workspace Scopes](Screenshots/Developer_Workspace_AppScope.PNG?raw=true "Selecting device scopes.")
6. Enter the name for your automation and click Save.
![Developer Workspace Settings](Screenshots/Developer_Workspace_AppSettings.PNG?raw=true "Automation settings.")
7. When the automation is being saved, SmartThings will send PING lifecycle POST request. Once the application responds properly, you should see connecton save confirmation with public key. Copy the key. You will need it to verify the incomming requests from SmartThings. The key will be securely stored in Azure Key Vault in the next section. When debugging the application locally with Ngrok you can use this key directly in the controller.
![Public Key Controller](Screenshots/Contoller_PublicKey.PNG?raw=true "Public key location in controller.")
## Install the automation
Now we are ready to add the automation in SmartThings app. 
1. Enable [developer mode](https://smartthings.developer.samsung.com/docs/guides/testing/developer-mode.html#Enable-Developer-Mode "Enable Developer Mode") that allows logging of any activity on the automation. This might be useful when troubleshooting or debugging.
2. [Add the automation](https://smartthings.developer.samsung.com/docs/guides/testing/developer-mode.html#Add-your-Automation "Add your Automation") to SmartThings app.

![Automation Selection Screenshot](Screenshots/Automation.jpg?raw=true "Automation Selection")

![Install First Page Screenshot](Screenshots/Install_First_Page.jpg?raw=true "First page of automation installation")

![Install Last Page Screenshot](Screenshots/Sensor_Selection.jpg?raw=true "Sensor selection")

![Authorization Screenshot](Screenshots/Permissions.jpg?raw=true "Setting device permissions")

Once the automation is successfully installed you should see POST requests containing device lifecycle every time the selected sensor gets opened or closed.
## Azure Deployment
With the automation registered in SmartThings and tested locally on your computer, it is time to host it in the cloud. The process is the same as for any web application that is deployed to Azure. There are however several customizations and workarounds that need to be placed before successful communication between SmartThings cloud and Azure is established. This section will explain all that. (Comming soon)

### Create AzureApp Service
Log in to your windows account in Visual Studio.
With the solution open in Visual Studio, right click on the project file and select *Publish*. On the *Pick a publish target* window keep the default settings and click on  *Publish*.
![Publish 1 Screenshot](Screenshots/Publish_1.PNG?raw=true "Publish 1")

On the *Create App Service* window: 
1. enter the name of your smart app in *App Name*
2. select your Azure subscription
3. Create resource group by clicking on *New...*
4. Create hosting plan by clicking on *New...*.
5. Select Application Insights region.
6. Click on *Create*.
![Publish 2 Screenshot](Screenshots/Publish_2.PNG?raw=true "Publish 2")

### Log in to Azure Portal
Once the app service is created and the application is published, log in to [Azure Portal](https://portal.azure.com) to view the app service. Click on *All Resources* on the *Home* tab on the left and find your app service.

### Add Application Insights
Once published, Application Insights service should be created and set up for the web service. The APPINSIGHTS_INSTRUMENTATIONKEY setting should exist in the app configuration and information about requests, dependencies, exceptions, etc. should be automatically logged.
![Azure Configuration Screenshot](Screenshots/Azure_Configuration.PNG?raw=true "Azure Configuration")
The topic is well described in [Application Insights for ASP.NET Core applications](https://docs.microsoft.com/en-us/azure/azure-monitor/app/asp-net-core).

### Add Key Vault Secret

### Publish

### Deploy Node.js Dependencies

### Change Node.js Settings

### Update Webhook Address
