using Microsoft.EntityFrameworkCore;

namespace Quotes.Provider.Db;

public class QuotesContext(DbContextOptions<QuotesContext> options): DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}