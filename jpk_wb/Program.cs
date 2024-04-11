// See https://aka.ms/new-console-template for more information
using System.CommandLine;
using jpk_wb.AppServices;
using jpk_wb.Data;
using jpk_wb.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = new ServiceCollection();

services.AddScoped<IJpkWbAppService, JpkWbAppService>();

services.AddScoped<IBankStatementService, BankStatementService>();
services.AddScoped<ICompanyInfoService, CompanyInfoService>();

services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(
        "Server=tcp:sqlserver-pola.database.windows.net,1433;Initial Catalog=jpk_wb;Persist Security Info=False;User ID=krezlau;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
});

var serviceProvider = services.BuildServiceProvider();

var rootCommand = new RootCommand("JPK_WB parser");

var fileOption = new Option<string>("--file",
                                     description: "File to parse");
fileOption.AddAlias("-f");
fileOption.IsRequired = true;
rootCommand.AddOption(fileOption);
 
var outputOption = new Option<string>("--output",
                                      description: "Output file",
                                      getDefaultValue: () => "jpk_wb.xml");
outputOption.AddAlias("-o");
rootCommand.AddOption(outputOption);

var companyOption = new Option<string>("--company",
                                       description: "Company name to use");
companyOption.AddAlias("-c");
companyOption.IsRequired = false;
rootCommand.AddOption(companyOption);

var addCommand = new Command("add", "Add data");
rootCommand.AddCommand(addCommand);
addCommand.AddAlias("a");
addCommand.AddOption(fileOption);

var deleteCommand = new Command("delete", "Delete data");
rootCommand.AddCommand(deleteCommand);
deleteCommand.AddAlias("d");

var createXmlCommand = new Command("create-xml", "Create XML file");
rootCommand.AddCommand(createXmlCommand);
createXmlCommand.AddAlias("c");
createXmlCommand.AddOption(outputOption);
createXmlCommand.AddOption(companyOption);

addCommand.SetHandler(
    async (fileOptionValue) =>
    {
        await serviceProvider.GetService<IJpkWbAppService>()!.AddData(fileOptionValue);
    },
    fileOption);

deleteCommand.SetHandler(
    async () =>
    {
        await serviceProvider.GetService<IJpkWbAppService>()!.DeleteData();
    });

createXmlCommand.SetHandler(
    async (outputOptionValue) =>
    {
        await serviceProvider.GetService<IJpkWbAppService>()!.CreateXml(outputOptionValue);
    },
    outputOption);

rootCommand.SetHandler(
    async (fileOptionValue, outputOptionValue) =>
    {
        await serviceProvider.GetService<IJpkWbAppService>()!.Run(fileOptionValue, outputOptionValue);
    },
    fileOption, outputOption);
    
rootCommand.InvokeAsync(args).Wait();