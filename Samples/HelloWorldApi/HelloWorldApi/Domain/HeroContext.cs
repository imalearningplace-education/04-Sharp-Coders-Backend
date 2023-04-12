using HelloWorldApi.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace HelloWorldApi.Domain;

public class HeroContext :  DbContext {
    public DbSet<Hero> Heroes { get; set; }

    public HeroContext(DbContextOptions options) 
        : base(options) {
    }
    
}
