using Dapper;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Domain.Enums;
using Inventory.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

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

    /// <summary>
    /// Creates a new inventory movement in the database.
    /// </summary>
    /// <param name="movement"></param>
    /// <returns>ID</returns>
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

    /// <summary>
    /// List all inventory movements by type of entries/exits.
    /// </summary>
    /// <returns>IEnumerable<InventoryMovement></returns>
    public async Task<IEnumerable<InventoryMovement>> GetAllByMovementType(MovementType MovementType) =>
        await _context.InventoryMovements.AsNoTracking().Include(x => x.Product)
        .Where(x => x.MovementType == MovementType && x.Active).ToListAsync();
}
