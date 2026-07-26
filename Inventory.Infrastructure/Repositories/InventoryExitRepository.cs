using Dapper;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories;

public class InventoryExitRepository : IInventoryExitRepository
{
    private readonly InventoryDbContext _context;
    private readonly IConnection _connection;
    public InventoryExitRepository(InventoryDbContext context, IConnection connection)
    {
        _context = context;
        _connection = connection;
    }

    #region Transacctions

    /// <summary>
    /// Creates a new inventory exit in the database.
    /// </summary>
    /// <param name="inventoryExit"></param>
    /// <returns>ID</returns>
    public async Task<int> Create(InventoryExit inventoryExit)
    {
        const string sql = @"
            INSERT INTO InventoryExits
            (Client, OrderNumber, DeliveredDate, Active, UserId, CreatedAt, UpdatedAt)
            VALUES
            (@Client, @OrderNumber, @DeliveredDate, @Active, @UserId, @CreatedAt, @UpdatedAt);
            SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";
        using var connection = _connection.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, inventoryExit);
    }

    /// <summary>
    /// Deletes an inventory exit from the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task Delete(int id)
    {
        const string sql = @"
            UPDATE InventoryExits
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
    /// Updates an existing inventory exit in the database.
    /// </summary>
    /// <param name="inventoryExit"></param>
    /// <returns></returns>
    public async Task Update(InventoryExit inventoryExit)
    {
        const string sql = @"
            UPDATE InventoryExits
            SET Client = @Client,
                OrderNumber = @OrderNumber,
                DeliveredDate = @DeliveredDate,
                UpdatedAt = @UpdatedAt,
                UserId = @UserId
            WHERE Id = @Id;";
        using var connection = _connection.CreateConnection();
        await connection.ExecuteAsync(sql, inventoryExit);
    }
    #endregion


    /// <summary>
    /// List all inventory exits in the database.
    /// </summary>
    /// <returns>IEnumerable<InventoryExit></returns>
    public async Task<IEnumerable<InventoryExit>> GetAll() =>
        await _context.InventoryExits.AsNoTracking().Where(x => x.Active).ToListAsync();


    /// <summary>
    /// Gets an inventory exit by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>InventoryExit</returns>
    public async Task<InventoryExit?> GetById(int id) =>
       await _context.InventoryExits.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);

}
