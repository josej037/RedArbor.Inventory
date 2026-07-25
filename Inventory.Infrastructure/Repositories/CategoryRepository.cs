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

    public async Task<IEnumerable<Category>> GetAll() => 
        await _context.Categories.AsNoTracking().Where(x => x.Active).ToListAsync();
    public async Task<Category?> GetById(int id) => 
       await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
}
