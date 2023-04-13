using Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class UserCrudContext : DbContext {

    public DbSet<User> Users { get; set; }

    public UserCrudContext(DbContextOptions options) 
        : base(options) {
    }

}
