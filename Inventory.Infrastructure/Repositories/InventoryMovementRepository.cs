using Dapper;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Connections;

namespace Inventory.Infrastructure.Repositories;

public class InventoryMovementRepository : IInventoryMovementRepository
{
    private readonly InventoryDbContext _context;
    private readonly IConnection _connection;
    public InventoryMovementRepository(InventoryDbContext context, IConnection connection)
    {
        _context = context;
        _connection = connection;
    }

    public async Task<int> Create(InventoryMovement movement)
    {
        const string sql = @"
            INSERT INTO InventoryMovements
            (ProductId, MovementType, ReferenceId, Quantity, StockBefore, StockAfter, Active, UserId, CreatedAt, UpdatedAt)
            VALUES
            (@ProductId, @MovementType, @ReferenceId, @Quantity, @StockBefore, @StockAfter, @Active, @UserId, @CreatedAt, @UpdatedAt);
            SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";
        using var connection = _connection.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, movement);
    }
}
