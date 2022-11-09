using System.Data.SqlClient;
using System.Data;
using KaupunkipyoraAPI.Services.Settings;
using Microsoft.Extensions.Options;

namespace KaupunkipyoraAPI.Context
{
    public class DapperContext
    {
        private readonly APIOptions _APIOptions;

        public DapperContext(IOptionsMonitor<APIOptions> options)
        {
            _APIOptions = options.CurrentValue;
        }
        public IDbConnection CreateConnection() => new SqlConnection(_APIOptions.ConnectionStrings.Default);
    }
}
