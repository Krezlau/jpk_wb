using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace jpk_wb.Data;

public class DesignAppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionBuilder = new DbContextOptionsBuilder<AppDbContext>();
        
        optionBuilder.UseSqlServer("Server=tcp:sqlserver-pola.database.windows.net,1433;Initial Catalog=jpk_wb;Persist Security Info=False;User ID=krezlau;Password=Cebula123!@#;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        
        return new AppDbContext(optionBuilder.Options);
    }
}