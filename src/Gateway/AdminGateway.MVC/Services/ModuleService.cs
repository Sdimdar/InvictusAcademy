using AdminGateway.MVC.HttpClientExtensions;
using AdminGateway.MVC.Services.Interfaces;
using AdminGateway.MVC.ViewModels;
using Ardalis.Result;
using DataTransferLib.Models;
using ExtendedHttpClient;
using ServicesContracts.Courses.Responses;

namespace AdminGateway.MVC.Services;

public class ModuleService : IModuleService
{
    public ExtendedHttpClient<IModuleService> ExtendedHttpClient { get; set; }
    public ModuleService(ExtendedHttpClient<IModuleService> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }

    public async Task<DefaultResponseObject<ModuleInfoVm>> CreateNewModule(CreateModuleVM request,
        CancellationToken cancellationToken)
    {
        return await ExtendedHttpClient
            .PostAndReturnResponseAsync<CreateModuleVM, DefaultResponseObject<ModuleInfoVm>>(request,
                "/Modules/Create");
    }

   
}