# New-SmartApp

New-SmartApp is a sample ASP.Net Core application that demonstrates how to write Samsung SmarThings hub automation using the new SmartThings API. The automation provides a very basic functionality. All it does is allows user to select a contact sensor to capture open and close events.

![Automation Selection Screenshot](Screenshots/Automation.jpg?raw=true "Automation Selection")

![Install First Page Screenshot](Screenshots/Install_First_Page.jpg?raw=true "First page of automation installation")

![Install Last Page Screenshot](Screenshots/Sensor_Selection.jpg?raw=true "Sensor selection")

![Authorization Screenshot](Screenshots/Permissions.jpg?raw=true "Setting device permissions")

![Logged Event Screenshot](Screenshots/Log_Result.PNG?raw=true "Event logged in Azure Application Insights")

This application is based on the [Weather Color Light SmartApp](https://github.com/SmartThingsCommunity/weather-color-light-smartapp-nodejs)  Node.js sample referenced on the [Getting Started](https://smartthings.developer.samsung.com/docs/getting-started/automation.html) documentation section of [SmartThings Developers](https://smartthings.developer.samsung.com/docs/index.html) website.

The application is an ASP.Net Core webhook hosted in Azure that receives and sends requests to SmartThings cloud where the automation connector is registered. 

Welcome. Do you happen to own a SmartThings hub in your home and ever wondered how to write an application for it? DotNet-SmartApp is a sample application that connects you to your SmartThings hub. With that the world is a limit. You can create a custom automation that could not be built with your SmartThings app on your phone. You can log the device events that are captured in the hub or display them on a dashboard.
DotNet SmartApp is a web application which works as a webhook endpoint. written in ASP.Net Core that captures the events from a contact sensor connected to your hub.



## Getting Started

Here is all you need to know about getting a copy of the project to your computer. Please refer to the deployment section when you are ready to deploy the project.

### Prerequisites

Here is what you need to run and deploy the application.
* Visual Studio with .Net Core and Azure SDKs
* Samsung SmartThings Hub (any version should work)
* SmartThings app (not the SmartThings Classic) installed on your phone
* One IOT device - this project uses a contact sensor but anything should work. You can use a virtual device if you do not have a physical one.
* Microsoft Azure account (free account should work)
* Samsung SmartThings developer account
* Ngrok application to expose your app to SmartThings cloud for development environment

### Installing

Here is what you need to do to run the application in development environment.
#### Getting the source code
Get the source code to your machine by clicking on Clone or download button, then Open in Visual Studio button or type the following in the Command Prompt:
```
git clone https://github.com/steveage/DotNet-SmartApp.git
```
#### Starting the project for the first time

#### Exposing the app to the web

#### Connecting the app to SmartThings Cloud

#### Enabling test mode


1. Connect SmartThings hub to your home network.
2. Add a device to the hub.
3. Fork the project from GitHub.
4. Start the application from Visual Studio.
5. Start ngrok application and run the following command:
```
ngrok http 53869 -host-header="localhost:53869"
```
6. Log in to your SmartThings developer account on [Developer Workspace](https://devworkspace.developer.samsung.com/smartthingsconsole/iotweb/site/#/).

### Deployment
Follow the steps below to deploy and set up your solution in Azure.
#### Create web service
#### Publish
#### Upload Node.js dependencies
#### Retrieve Public Key from SmartThings dev portal
#### Create Key Vault
#### Store public key in Key Vault
#### Authorize the app to access Key Vault.

### Running the App
