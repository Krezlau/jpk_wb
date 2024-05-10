using System.ComponentModel.DataAnnotations;
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
        try
        {
            await AddData(file);
            await CreateXml(output);
        }
        catch (Exception e)
        {
            Console.WriteLine("Aborting...");
        }
    }

    public async Task AddData(string file)
    {
        await DeleteData();
        
        Console.WriteLine("Adding data from file: {0}...", file);
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
            throw new Exception("Error reading file.");
        }

        ValidateAndThrow(data);
        
        // save to db 
        var companyInfo = data.InformacjePodmiotu.ToEntity();
        await _companyInfoService.AddCompanyInfo(companyInfo);
        
        var bankStatement = data.WyciagBankowy.ToEntity(companyInfo.Id);
        await _bankStatementService.AddBankStatement(bankStatement);
        
        Console.WriteLine("Done.");
    }

    public async Task DeleteData()
    {
        Console.WriteLine("Deleting data from the database...");
        await _companyInfoService.DeleteCompanyInfo();
        await _bankStatementService.DeleteBankStatements();
        Console.WriteLine("Done.");
    }

    public async Task CreateXml(string output)
    {
        Console.WriteLine("Creating the xml...");
        var bankStatement = await _bankStatementService.GetBankStatement();
        if (bankStatement is null || bankStatement.Transakcje.IsNullOrEmpty() || bankStatement.InformacjePodmiotu is null)
        {
            Console.WriteLine("No data to create xml.");
            throw new Exception("No data to create xml.");
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
            throw new Exception("Error writing to file.");
        }
        
        Console.WriteLine("Done.");
    }

    private static void ValidateAndThrow(DataDTO? data)
    {
        if (data is null)
        {
            Console.WriteLine("No data to add.");
            throw new Exception("No data to add.");
        }
        
        var context = new ValidationContext(data, null, null);
        var context2 = new ValidationContext(data.InformacjePodmiotu, null, null);
        var context3 = new ValidationContext(data.WyciagBankowy, null, null);
        var results = new List<ValidationResult>();
        
        bool dataValid = Validator.TryValidateObject(data, context, results, true);
        bool informacjePodmiotuValid = Validator.TryValidateObject(data.InformacjePodmiotu, context2, results, true);
        bool wyciagBankowyValid = Validator.TryValidateObject(data.WyciagBankowy, context3, results, true);
        bool transakcjeValid = true;
        foreach (var transaction in data.WyciagBankowy.Transakcje)
        {
            var context4 = new ValidationContext(transaction, null, null);
            transakcjeValid &= Validator.TryValidateObject(transaction, context4, results, true);
        }
            
        if (!dataValid || !informacjePodmiotuValid || !wyciagBankowyValid || !transakcjeValid)
        {
            Console.WriteLine("Validation failed:");
            foreach (var result in results)
            {
                Console.WriteLine(result.ErrorMessage);
            }
            
            throw new Exception("Validation failed");
        }
    }
}