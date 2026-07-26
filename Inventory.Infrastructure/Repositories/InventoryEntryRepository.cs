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

    public async Task<IEnumerable<InventoryEntry>> GetAll() =>
        await _context.InventoryEntries.AsNoTracking().Where(x => x.Active).ToListAsync();

    public async Task<InventoryEntry?> GetById(int id) =>
       await _context.InventoryEntries.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);

}
