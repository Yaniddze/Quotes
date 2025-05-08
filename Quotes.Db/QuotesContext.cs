using Microsoft.EntityFrameworkCore;

namespace Quotes.Db;

public class QuotesContext(DbContextOptions<QuotesContext> options): DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}