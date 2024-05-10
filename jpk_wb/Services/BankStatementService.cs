using jpk_wb.Data;
using Microsoft.EntityFrameworkCore;

namespace jpk_wb.Services;

public interface IBankStatementService
{
    Task<BankStatement?> GetBankStatement();
    Task<List<BankStatement>> GetBankStatements();
    Task AddBankStatement(BankStatement bankStatement);
    Task DeleteBankStatements();
}

public class BankStatementService : IBankStatementService
{
    private readonly AppDbContext _context;

    public BankStatementService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<BankStatement?> GetBankStatement()
    {
        return await _context.BankStatements
            .Include(x => x.InformacjePodmiotu)
            .Include(x => x.Transakcje)
            .FirstOrDefaultAsync();
    }

    public async Task<List<BankStatement>> GetBankStatements()
    {
        return await _context.BankStatements.ToListAsync();
    }

    public async Task AddBankStatement(BankStatement bankStatement)
    {
        await _context.BankStatements.AddAsync(bankStatement);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteBankStatements()
    {
        var bankStatements = await _context.BankStatements.ToListAsync();
        _context.BankStatements.RemoveRange(bankStatements);
        await _context.SaveChangesAsync();
    }
}