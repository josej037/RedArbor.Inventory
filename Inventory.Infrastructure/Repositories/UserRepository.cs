using Inventory.Application.Interfaces;
using Inventory.Domain.Entities;
using Inventory.Infrastructure.Connections;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Repositories;

internal class UserRepository : IUserRepository
{
    private readonly InventoryDbContext _context;
    private readonly IConnection _connection;
    public UserRepository(InventoryDbContext context, IConnection connection)
    {
        _context = context;
        _connection = connection;
    }
    public async Task<User?> Login(string username) =>
        await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Username == username && x.Active);
}
