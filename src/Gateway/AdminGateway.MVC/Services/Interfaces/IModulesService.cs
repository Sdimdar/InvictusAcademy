using AdminGateway.MVC.ViewModels;
using Ardalis.Result;
using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Modules.Commands;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Responses;

namespace AdminGateway.MVC.Services.Interfaces;

public interface IModulesService: IUseExtendedHttpClient<IModulesService>
{
    Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> AddArticle(AddArticlesCommand request);
    Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> Create(CreateModuleCommand request);
    Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> Delete(DeleteModuleCommand request);
    Task<DefaultResponseObject<List<ModuleInfoVm>>> GetAll();
    Task<DefaultResponseObject<int>> GetModulesCount();
    Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> GetFilterByString(GetModulesByFilterStringQuery request);
    Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> GetById(ModuleByIdVm request);
    Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> GetByListOfId(GetModulesByListOfIdQuery request);
    Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> Update(UpdateModuleCommand request);

}