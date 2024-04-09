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
    public async Task Run(string file, string output)
    {
        await AddData(file);
        await CreateXml(output);
    }

    public async Task AddData(string file)
    {
        Console.WriteLine("AddData");
    }

    public async Task DeleteData()
    {
        Console.WriteLine("DeleteData");
    }

    public async Task CreateXml(string output)
    {
        Console.WriteLine("CreateXml");
    }
}