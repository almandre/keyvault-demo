# Azure Key Vault Demo

This project demonstrates how to interact with Azure Key Vault secrets using C# and the Azure SDK. It covers creating, retrieving, deleting, and purging secrets programmatically.

## Features
- Set the secret name and value via console input
- Create a secret in Azure Key Vault
- Retrieve the secret value
- Delete the secret (soft delete)
- Purge the secret (permanently delete)
- Handles permission errors gracefully

## Prerequisites
- .NET 9.0 SDK
- Azure subscription
- An existing Azure Key Vault
- Access permissions to manage secrets in the Key Vault
- [Azure CLI](https://docs.microsoft.com/en-us/cli/azure/install-azure-cli) (optional, for authentication)

## Setup
1. Clone this repository or copy the files to your local machine.
2. Update `appsettings.json` with your Key Vault URI:
   ```json
   {
     "KeyVaultUri": "https://<your-keyvault-name>.vault.azure.net/",
     "SecretName": "secret-name-demo"
   }
   ```
3. Make sure your user or service principal has the following permissions on the Key Vault:
   - get, list, set, delete, recover, backup, restore, purge
   You can configure this via Azure Portal (Access policies or IAM).

## How to Run
1. Open a terminal in the project directory.
2. Run the following command:
   ```sh
   dotnet run
   ```
3. Follow the prompts:
   - Enter the secret name (or press Enter to use the default)
   - Enter the secret value
   - The app will create, retrieve, delete, and attempt to purge the secret

## Notes
- If you do not have purge permission, the app will skip the purge step and display a warning.
- Deleted secrets (soft deleted) can be viewed in the Azure Portal under the "Deleted" tab in the Key Vault secrets blade.

## Useful Azure CLI Commands
- List deleted secrets:
  ```sh
  az keyvault secret list-deleted --vault-name <your-keyvault-name>
  ```
- Set access policy for purge:
  ```sh
  az keyvault set-policy --name <your-keyvault-name> --upn <your-user-email> --secret-permissions get list set delete recover backup restore purge
  ```

## License
This project is for educational/demo purposes.
