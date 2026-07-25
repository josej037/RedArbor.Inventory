using System.Data;

namespace Inventory.Infrastructure.Connections;

public interface IConnection
{
    IDbConnection CreateConnection();
}
