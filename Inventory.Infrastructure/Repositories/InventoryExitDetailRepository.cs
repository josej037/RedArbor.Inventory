using Dapper;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories;

internal class InventoryExitDetailRepository : IInventoryExitDetailRepository
{
    private readonly InventoryDbContext _context;
    private readonly IConnection _connection;
    public InventoryExitDetailRepository(InventoryDbContext context, IConnection connection)
    {
        _context = context;
        _connection = connection;
    }

    #region Transacctions
    public async Task<int> Create(InventoryExitDetail detail)
    {
        const string sql = @"
            INSERT INTO InventoryExitDetails
            (InventoryExitId, ProductId, Quantity, UnitCost, Active, CreatedAt, UpdatedAt)
            VALUES
            (@InventoryExitId, @ProductId, @Quantity, @UnitCost, @Active, @CreatedAt, @UpdatedAt);
            SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";
        using var connection = _connection.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, detail);
    }

    public async Task DeleteByExitId(int inventoryExitId)
    {
        const string sql = @"
            UPDATE InventoryExitDetails
            SET Active = 0, 
                UpdatedAt = @UpdatedAt
            WHERE InventoryExitId = @InventoryExitId;";
        using var connection = _connection.CreateConnection();
        await connection.ExecuteAsync(sql, new
        {
            InventoryExitId = inventoryExitId,
            UpdatedAt = DateTime.UtcNow
        });
    }
    #endregion

    public async Task<IEnumerable<InventoryExitDetail>> GetByExitId(int inventoryExitId) =>
        await _context.InventoryExitDetails.AsNoTracking().Where(x => x.InventoryExitId == inventoryExitId && x.Active).ToListAsync();
}
