using DbUp;
using System.Reflection;

//Pull connection string from secret store and pass in CI/CD
var connectionString = args.FirstOrDefault(x => x.StartsWith("--ConnectionString", StringComparison.OrdinalIgnoreCase))
    ?? "";

if (String.IsNullOrEmpty(connectionString))
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Please provide a connection string");
    Console.ResetColor();
    return -1;
}

connectionString = connectionString.Substring(connectionString.IndexOf("=") + 1).Replace(@"""", string.Empty);

var shoppingApiPassword = args.FirstOrDefault(x => x.StartsWith("--ApiUserPassword", StringComparison.OrdinalIgnoreCase)) 
        ?? "";
shoppingApiPassword = shoppingApiPassword.Substring(shoppingApiPassword.IndexOf("=") + 1).Replace(@"""", string.Empty);


//Create database if it does not exist
EnsureDatabase.For.PostgresqlDatabase(connectionString);

var upgrader =
    DeployChanges.To
        .PostgresqlDatabase(connectionString)
        .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
        .WithVariable("api_pwd", shoppingApiPassword)
        .LogToConsole()
        .Build();

var result = upgrader.PerformUpgrade();

if (!result.Successful)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(result.Error);
    Console.ResetColor();
#if DEBUG
    Console.ReadLine();
#endif
    return -1;
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Success!");
Console.ResetColor();

return 0;