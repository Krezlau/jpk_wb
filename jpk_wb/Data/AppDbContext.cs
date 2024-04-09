using Microsoft.EntityFrameworkCore;

namespace jpk_wb.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<CompanyInfo> CompanyInfos { get; set; } = null!;
    
    public DbSet<BankStatement> BankStatements { get; set; } = null!;
    
    public DbSet<Transaction> Transactions { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CompanyInfo>().HasIndex(x => x.NIP).IsUnique();
        modelBuilder.Entity<CompanyInfo>().HasIndex(x => x.REGON).IsUnique();
        modelBuilder.Entity<CompanyInfo>().HasIndex(x => x.PelnaNazwa).IsUnique();
    }
}