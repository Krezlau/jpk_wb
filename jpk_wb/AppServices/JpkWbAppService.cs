using jpk_wb.Data;
using jpk_wb.Services;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace jpk_wb.AppServices;

public interface IJpkWbAppService
{
    Task Run(string file, string output);
    Task AddData(string file);
    Task DeleteData();
    Task CreateXml(string output);
}

public class JpkWbAppService : IJpkWbAppService
{
    private readonly IBankStatementService _bankStatementService;
    private readonly ICompanyInfoService _companyInfoService;

    public JpkWbAppService(IBankStatementService bankStatementService,
        ICompanyInfoService companyInfoService)
    {
        _bankStatementService = bankStatementService;
        _companyInfoService = companyInfoService;
    }

    public async Task Run(string file, string output)
    {
        await DeleteData();
        await AddData(file);
        await CreateXml(output);
    }

    public async Task AddData(string file)
    {
        Console.WriteLine("Adding data from file: {0}", file);
        // read json from file
        DataDTO? data = null;
        try
        {
            var json = await File.ReadAllTextAsync(file);
            data = JsonConvert.DeserializeObject<DataDTO>(json);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error reading file: {0}", e.Message);
        }
        
        // validation
        if (data is null) return;
        
        // save to db 
        var companyInfo = data.InformacjePodmiotu.ToEntity();
        await _companyInfoService.AddCompanyInfo(companyInfo);
        
        var bankStatement = data.WyciagBankowy.ToEntity(companyInfo.Id);
        await _bankStatementService.AddBankStatement(bankStatement);
        
        Console.WriteLine("done");
    }

    public async Task DeleteData()
    {
        Console.WriteLine("Deleting data from the database");
        await _companyInfoService.DeleteCompanyInfo();
        await _bankStatementService.DeleteBankStatements();
        Console.WriteLine("done");
    }

    public async Task CreateXml(string output)
    {
        Console.WriteLine("CreateXml");
        var bankStatement = await _bankStatementService.GetBankStatement();
        if (bankStatement is null || bankStatement.Transakcje.IsNullOrEmpty() || bankStatement.InformacjePodmiotu is null)
        {
            Console.WriteLine("No data to create xml");
            return;
        }
        
        var jpk = XmlCreatorService.ConstructXml(bankStatement);
        
        // write to file
        try
        {
            await File.WriteAllTextAsync(output, jpk.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine("Error writing to file: {0}", e.Message);
        }
        
        Console.WriteLine("done");
    }
}