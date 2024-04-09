using jpk_wb.Data;

namespace jpk_wb.Services;

public interface ICompanyInfoService
{
    
}

public class CompanyInfoService : ICompanyInfoService
{
    private readonly AppDbContext _context;

    public CompanyInfoService(AppDbContext context)
    {
        _context = context;
    }
}