using Azure.Identity;
using Azure.Security.KeyVault.Secrets;


var appSettings = KeyVaultDemo.Configuration.ConfigurationHelper.LoadSettings();
string kvUri = appSettings.KeyVaultUri;
string keyVaultName = new Uri(kvUri).Host.Split('.')[0];

var client = new SecretClient(new Uri(kvUri), new DefaultAzureCredential());

Console.WriteLine("Enter the secret name (or press Enter to use default):");
string? userSecretName = Console.ReadLine();
string secretName = string.IsNullOrWhiteSpace(userSecretName) ? "secret-name-demo" : userSecretName;
Console.WriteLine($"Using secret name: '{secretName}'");

Console.WriteLine("Enter a secret value:");
var secretValue = Console.ReadLine() ?? string.Empty;

Console.Write($"Creating a secret in {keyVaultName} called '{secretName}' with the value '{secretValue}' ...");
await client.SetSecretAsync(secretName, secretValue);
Console.WriteLine("\nSecret created.");

Console.WriteLine("Press any key to continue.");
Console.ReadKey();

Console.WriteLine($"Retrieving your secret value from {keyVaultName}.");
var secret = await client.GetSecretAsync(secretName);
Console.WriteLine($"Your secret value is '{secret.Value.Value}'.");

Console.WriteLine("Press any key to continue.");
Console.ReadKey();

Console.Write($"Deleting your secret from {keyVaultName} ...");
DeleteSecretOperation operation = await client.StartDeleteSecretAsync(secretName);
await operation.WaitForCompletionAsync();
Console.WriteLine("\nSecret deleted.");

Console.WriteLine("Press any key to continue.");
Console.ReadKey();

Console.Write($"Purging your secret from {keyVaultName} ...");

try
{
    await client.PurgeDeletedSecretAsync(secretName);
    Console.WriteLine("\nDone.");
}
catch (Azure.RequestFailedException ex) when (ex.Status == 403)
{
    Console.WriteLine("\nSkipped (insufficient permissions).");
    Console.WriteLine("Note: Purge permission is required to permanently delete secrets.");
}

Console.WriteLine("Press any key to close.");
Console.ReadKey();
