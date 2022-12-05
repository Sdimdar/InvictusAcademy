using AdminGateway.MVC.ViewModels;
using DataTransferLib.Models;
using ExtendedHttpClient.Interfaces;
using ServicesContracts.Courses.Requests.Modules.Commands;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Responses;

namespace AdminGateway.MVC.Services.Interfaces;

public interface IModulesService : IUseExtendedHttpClient<IModulesService>
{
    Task<DefaultResponseObject<ModuleInfoVm>> AddArticle(AddArticlesCommand request);
    Task<DefaultResponseObject<ModuleInfoVm>> AddTest(AddTestCommand request);
    Task<DefaultResponseObject<ModuleInfoVm>> Create(CreateModuleCommand request);
    Task<DefaultResponseObject<ModuleInfoVm>> Delete(DeleteModuleCommand request);
    Task<DefaultResponseObject<List<ModuleInfoVm>>> GetAll();
    Task<DefaultResponseObject<int>> GetModulesCount();
    Task<DefaultResponseObject<List<ModuleInfoVm>>> GetFilterByString(GetModulesByFilterStringQuery request);
    Task<DefaultResponseObject<ModuleInfoVm>> GetById(ModuleByIdVm request);
    Task<DefaultResponseObject<List<ModuleInfoVm>>> GetByListOfId(GetModulesByListOfIdQuery request);
    Task<DefaultResponseObject<ModuleInfoVm>> Update(UpdateModuleCommand request);

}