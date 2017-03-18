
az login

az group create -l westus2 -n learn-phrases

az appservice plan create -g learn-phrases -n app-plan --sku B1

az appservice web create -g learn-phrases -p app-plan -n learn-phrases

Site: http://learn-phrases.azurewebsites.net
