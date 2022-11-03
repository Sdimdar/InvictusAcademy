using AdminGateway.MVC.ViewModels;
using Ardalis.Result;
using DataTransferLib.Models;
using ServicesContracts.Request.Requests.Commands;

namespace AdminGateway.MVC.Services.Interfaces;

public interface IModuleService
{
    Task<DefaultResponseObject<bool>> CreateNewModule(CreateModuleVM request, CancellationToken cancellationToken);
}