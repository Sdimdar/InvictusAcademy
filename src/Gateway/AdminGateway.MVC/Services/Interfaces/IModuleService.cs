using AdminGateway.MVC.ViewModels;
using Ardalis.Result;
using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Courses.Responses;
using ServicesContracts.Request.Requests.Commands;

namespace AdminGateway.MVC.Services.Interfaces;

public interface IModuleService:IUseExtendedHttpClient<IModuleService>
{
    Task<DefaultResponseObject<ModuleInfoVm>> CreateNewModule(CreateModuleVM request,
        CancellationToken cancellationToken);
}