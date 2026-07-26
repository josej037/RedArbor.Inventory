using Dapper;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories;

internal class InventoryEntryDetailRepository : IInventoryEntryDetailRepository
{
    private readonly InventoryDbContext _context;
    private readonly IConnection _connection;
    public InventoryEntryDetailRepository(InventoryDbContext context, IConnection connection)
    {
        _context = context;
        _connection = connection;
    }

    #region Transacctions
    public async Task<int> Create(InventoryEntryDetail detail)
    {
        const string sql = @"
            INSERT INTO InventoryEntryDetails
            (InventoryEntryId, ProductId, Quantity, UnitCost, Active, CreatedAt, UpdatedAt)
            VALUES
            (@InventoryEntryId, @ProductId, @Quantity, @UnitCost, @Active, @CreatedAt, @UpdatedAt);
            SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";
        using var connection = _connection.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, detail);
    }

    public async Task DeleteByEntryId(int inventoryEntryId)
    {
        const string sql = @"
            UPDATE InventoryEntryDetails
            SET Active = 0, 
                UpdatedAt = @UpdatedAt
            WHERE InventoryEntryId = @InventoryEntryId;";
        using var connection = _connection.CreateConnection();
        await connection.ExecuteAsync(sql, new
        {
            InventoryEntryId = inventoryEntryId,
            UpdatedAt = DateTime.UtcNow
        });
    }
    #endregion

    public async Task<IEnumerable<InventoryEntryDetail>> GetByEntryId(int inventoryEntryId) =>
        await _context.InventoryEntryDetails.AsNoTracking().Where(x => x.InventoryEntryId == inventoryEntryId && x.Active).ToListAsync();
}
