using DbUp;
using KaupunkipyoraAPI.Services.Settings;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace KaupunkipyoraAPI.Services
{
    /// <summary>
    /// Db-up migraatiot, migraatiotiedostot Migrations-kansioon ja tiedoston build action: Embedded resource
    /// </summary>
    public class DatabaseMigrator : IStartupFilter
    {
        private readonly APIOptions Asetukset;
        public DatabaseMigrator(IOptionsMonitor<APIOptions> asetukset)
        {
            Asetukset = asetukset.CurrentValue;
        }

        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            var connectionString = Asetukset.ConnectionStrings.Default;

            EnsureDatabase.For.SqlDatabase(connectionString);

            var dbUpgradeEngineBuilder = DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .WithTransaction()
                .LogToConsole();

            var dbUpgradeEngine = dbUpgradeEngineBuilder.Build();
            if (dbUpgradeEngine.IsUpgradeRequired())
            {
                Console.WriteLine("Upgrades have been detected. Upgrading database ... ");
                var operation = dbUpgradeEngine.PerformUpgrade();
                if (operation.Successful)
                    Console.WriteLine("Database upgrade completed successfully");
                else
                {
                    Console.WriteLine("Error upgrading database: " + operation.Error.ToString());
                    throw new ApplicationException("Error upgrading database.");
                }
            }

            return next;
        }
    }
}
