using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Inventory.Infrastructure.Connections;

public class InventoryConn : IConnection
{
    private readonly IConfiguration _configuration;

    public InventoryConn(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection CreateConnection()
    {
        var connectionString = _configuration.GetConnectionString("InventoryDb");
        return new SqlConnection(connectionString);
    }
}
