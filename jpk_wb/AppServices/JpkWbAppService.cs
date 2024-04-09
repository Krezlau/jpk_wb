namespace jpk_wb.AppServices;

public interface IJpkWbAppService
{
    Task Run(string file, string output);
}

public class JpkWbAppService : IJpkWbAppService
{
    public async Task Run(string file, string output)
    {
        Console.WriteLine("lmao");
    }
}