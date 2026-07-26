using Dapper;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories;

public class InventoryEntryRepository : IInventoryEntryRepository
{
    private readonly InventoryDbContext _context;
    private readonly IConnection _connection;
    public InventoryEntryRepository(InventoryDbContext context, IConnection connection)
    {
        _context = context;
        _connection = connection;
    }

    #region Transacctions

    /// <summary>
    /// Creates a new inventory entry in the database.
    /// </summary>
    /// <param name="inventoryEntry"></param>
    /// <returns>ID</returns>
    public async Task<int> Create(InventoryEntry inventoryEntry)
    {
        const string sql = @"
            INSERT INTO InventoryEntries
            (Supplier, InvoiceNumber, ReceivedDate, Active, UserId, CreatedAt, UpdatedAt)
            VALUES
            (@Supplier, @InvoiceNumber, @ReceivedDate, @Active, @UserId, @CreatedAt, @UpdatedAt);
            SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";
        using var connection = _connection.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, inventoryEntry);
    }

    /// <summary>
    /// Deletes an inventory entry from the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task Delete(int id)
    {
        const string sql = @"
            UPDATE InventoryEntries
            SET Active = 0, 
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id;";
        using var connection = _connection.CreateConnection();
        await connection.ExecuteAsync(sql, new
        {
            Id = id,
            UpdatedAt = DateTime.UtcNow
        });
    }

    /// <summary>
    /// Updates an existing inventory entry in the database.
    /// </summary>
    /// <param name="inventoryEntry"></param>
    /// <returns></returns>
    public async Task Update(InventoryEntry inventoryEntry)
    {
        const string sql = @"
            UPDATE InventoryEntries
            SET Supplier = @Supplier,
                InvoiceNumber = @InvoiceNumber,
                ReceivedDate = @ReceivedDate,
                UpdatedAt = @UpdatedAt,
                UserId = @UserId
            WHERE Id = @Id;";
        using var connection = _connection.CreateConnection();
        await connection.ExecuteAsync(sql, inventoryEntry);
    }
    #endregion

    /// <summary>
    /// List all inventory entries in the database.
    /// </summary>
    /// <returns>IEnumerable<InventoryEntry></returns>
    public async Task<IEnumerable<InventoryEntry>> GetAll() =>
        await _context.InventoryEntries.AsNoTracking().Where(x => x.Active).ToListAsync();

    /// <summary>
    /// Gets an inventory entry by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>InventoryEntry</returns>
    public async Task<InventoryEntry?> GetById(int id) =>
       await _context.InventoryEntries.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
}
