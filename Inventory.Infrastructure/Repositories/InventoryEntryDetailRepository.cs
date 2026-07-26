using Dapper;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories;

public class InventoryEntryDetailRepository : IInventoryEntryDetailRepository
{
    private readonly InventoryDbContext _context;
    private readonly IConnection _connection;
    public InventoryEntryDetailRepository(InventoryDbContext context, IConnection connection)
    {
        _context = context;
        _connection = connection;
    }

    #region Transacctions

    /// <summary>
    /// Creates a new entry detail in the database.
    /// </summary>
    /// <param name="detail"></param>
    /// <returns>ID</returns> 
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

    /// <summary>
    /// Deletes a entry detail from the database.
    /// </summary>
    /// <param name="inventoryEntryId"></param>
    /// <returns></returns>
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

    /// <summary>
    /// List all entry details in the database.
    /// </summary>
    /// <param name="inventoryEntryId"></param>
    /// <returns>IEnumerable<InventoryEntryDetail></returns>
    public async Task<IEnumerable<InventoryEntryDetail>> GetByEntryId(int inventoryEntryId) =>
        await _context.InventoryEntryDetails.AsNoTracking().Where(x => x.InventoryEntryId == inventoryEntryId && x.Active).ToListAsync();
}
