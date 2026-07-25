using Dapper;
using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;
namespace Inventory.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly InventoryDbContext _context;
    private readonly IConnection _connection;
    public ProductRepository(InventoryDbContext context, IConnection connection)
    {
        _context = context;
        _connection = connection;
    }

    #region Transacctions
    public async Task<int> Create(Product product)
    {
        const string sql = @"
            INSERT INTO Products
            (Name, Description, Price, Stock, CategoryId, Active, UserId, CreatedAt, UpdatedAt)
            VALUES
            (@Name, @Description, @Price, @Stock, @CategoryId, @Active, @UserId, @CreatedAt, @UpdatedAt);
            SELECT CAST(SCOPE_IDENTITY() AS INT);
            ";
        using var connection = _connection.CreateConnection();
        return await connection.ExecuteScalarAsync<int>(sql, product);
    }

    public async Task Delete(int id)
    {
        const string sql = @"
            UPDATE Products
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

    public async Task Update(Product product)
    {
        const string sql = @"
            UPDATE Products
            SET Name = @Name,
                Description = @Description,
                Price = @Price,
                Stock = @Stock,
                CategoryId = @CategoryId,
                UpdatedAt = @UpdatedAt,
                UserId = @UserId
            WHERE Id = @Id;";
        using var connection = _connection.CreateConnection();
        await connection.ExecuteAsync(sql, product);
    }
    #endregion

    public async Task<IEnumerable<Product>> GetAll() => 
        await _context.Products.AsNoTracking().Where(x => x.Active).ToListAsync();
    public async Task<Product?> GetById(int id) => 
       await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Active);
}
