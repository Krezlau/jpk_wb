using jpk_wb.Data;
using Microsoft.EntityFrameworkCore;

namespace jpk_wb.Services;

public interface ICompanyInfoService
{
    Task AddCompanyInfo(CompanyInfo companyInfo);
    Task DeleteCompanyInfo();
}

public class CompanyInfoService : ICompanyInfoService
{
    private readonly AppDbContext _context;

    public CompanyInfoService(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddCompanyInfo(CompanyInfo companyInfo)
    {
        await _context.CompanyInfos.AddAsync(companyInfo);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteCompanyInfo()
    {
        var companyInfos = await _context.CompanyInfos.ToListAsync();
        _context.CompanyInfos.RemoveRange(companyInfos);
        await _context.SaveChangesAsync();
    }
}