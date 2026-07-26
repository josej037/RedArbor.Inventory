using Dapper;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly InventoryDbContext _context;
    private readonly IConnection _connection;
    public CategoryRepository(InventoryDbContext context, IConnection connection)
    {
        _context = context;
        _connection = connection;
    }

    #region Transacctions

    /// <summary>
    /// Creates a new category in the database.
    /// </summary>
    /// <param name="category"></param>
    /// <returns>ID</returns>
    public async Task<int> Create(Category category)
    {
        const string sql = @"
            INSERT INTO Categories
            (Name, Description, Active, UserId, CreatedAt, UpdatedAt)
            VALUES
            (@Name, @Description, @Active, @UserId, @CreatedAt, @UpdatedAt);
            SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";
        using var connection = _connection.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, category);
    }

    /// <summary>
    /// Deletes a category from the database.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task Delete(int id)
    {
        const string sql = @"
            UPDATE Categories
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
    /// Updates an existing category in the database.
    /// </summary>
    /// <param name="category"></param>
    /// <returns></returns>
    public async Task Update(Category category)
    {
        const string sql = @"
            UPDATE Categories
            SET Name = @Name,
                Description = @Description,
                UpdatedAt = @UpdatedAt,
                UserId = @UserId
            WHERE Id = @Id;";
        using var connection = _connection.CreateConnection();
        await connection.ExecuteAsync(sql, category);
    }
    #endregion

    /// <summary>
    /// List all categories in the database.
    /// </summary>
    /// <returns>IEnumerable<Category></returns>
    public async Task<IEnumerable<Category>> GetAll() =>
        await _context.Categories.AsNoTracking().Where(x => x.Active).ToListAsync();

    /// <summary>
    /// Gets a category by its ID.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Category</returns>
    public async Task<Category?> GetById(int id) =>
    await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
}
