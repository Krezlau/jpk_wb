using jpk_wb.Data;
using Microsoft.EntityFrameworkCore;

namespace jpk_wb.Services;

public interface IBankStatementService
{
    Task<List<BankStatement>> GetBankStatements();
}

public class BankStatementService : IBankStatementService
{
    private readonly AppDbContext _context;

    public BankStatementService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<BankStatement>> GetBankStatements()
    {
        return await _context.BankStatements.ToListAsync();
    }
}