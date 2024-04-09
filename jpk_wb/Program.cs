// See https://aka.ms/new-console-template for more information
using System.CommandLine;
using jpk_wb.AppServices;
using Microsoft.Extensions.DependencyInjection;

ServiceCollection services = new ServiceCollection();

services.AddTransient<IJpkWbAppService, JpkWbAppService>();

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


rootCommand.SetHandler(
    async (fileOptionValue, outputOptionValue) =>
    {
        await serviceProvider.GetService<IJpkWbAppService>()!.Run(fileOptionValue, outputOptionValue);
    },
    fileOption, outputOption);
    
rootCommand.InvokeAsync(args).Wait();