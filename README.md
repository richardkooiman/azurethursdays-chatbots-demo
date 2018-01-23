# Azure Thursdays: Chatbots - Demo
This is a demo chatbot application based on Microsoft's Bot Framework and LUIS.

## Prerequisites
1. Install the Visual Studio 2017 project templates for a Bot Application as described on https://docs.microsoft.com/en-us/bot-framework/dotnet/bot-builder-dotnet-quickstart
2. Install the Bot Framework Emulator from https://docs.microsoft.com/en-us/bot-framework/bot-service-debug-emulator

## Create the bot
1. Open Visual Studio
2. Create new project from Visual C# -> Bot Application
3. Build (to restore missing NuGet packages)
4. Start / debug
5. Open Bot Framework Emulator
6. Set endpoint url to http://localhost:3979/api/messages and click on 'Connect'
7. Test your bot by sending a test message