using System.Text;
using AdminGateway.MVC.Services.Interfaces;
using DataTransferLib.Models;
using ExtendedHttpClient;
using Microsoft.AspNetCore.Mvc;
using ServicesContracts.Courses.Requests.Modules.Commands;
using ServicesContracts.Courses.Requests.Modules.Queries;
using ServicesContracts.Courses.Responses;

namespace AdminGateway.MVC.Services;

public class ModulesService : IModulesService
{ 
    public ExtendedHttpClient<IModulesService> ExtendedHttpClient { get; set; }
    
    public ModulesService(ExtendedHttpClient<IModulesService> extendedHttpClient)
    {
        ExtendedHttpClient = extendedHttpClient;
    }


    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> AddArticle(AddArticlesCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<AddArticlesCommand, DefaultResponseObject<ModuleInfoVm>>(request, $"/Modules/AddArticles");
    }

    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> Create(CreateModuleCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<CreateModuleCommand, DefaultResponseObject<ModuleInfoVm>>(request, $"/Modules/Create");
    }

    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> Delete(DeleteModuleCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<DeleteModuleCommand, DefaultResponseObject<ModuleInfoVm>>(request, $"/Modules/Delete");
    }

    public async Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> GetAll()
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<List<ModuleInfoVm>>>($"/Module/GetAll");
    }

    public async Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> GetFilterByString(GetModulesByFilterStringQuery request)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<GetModulesByFilterStringQuery, DefaultResponseObject<List<ModuleInfoVm>>>(request,$"/Module/GetByFilterString");
    }

    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> GetById(GetModuleByIdQuery request)
    {
        return await ExtendedHttpClient.GetAndReturnResponseAsync<GetModuleByIdQuery, DefaultResponseObject<ModuleInfoVm>>(request,$"/Module/GetById");
    }

    public async Task<ActionResult<DefaultResponseObject<List<ModuleInfoVm>>>> GetByListOfId(GetModulesByListOfIdQuery request)
    {
        StringBuilder stringBuilder = new StringBuilder();
        for (int i = 0; i < request.ModulesId.Count; i++)
        {
            if (i==request.ModulesId.Count-1)
            {
                stringBuilder.Append("ModulesId=");
                stringBuilder.Append(request.ModulesId[i]);
            }
            else
            {
                stringBuilder.Append("ModulesId=");
                stringBuilder.Append(request.ModulesId[i]);
                stringBuilder.Append("&");
            }
        }

        string requestString = stringBuilder.ToString();
        return await ExtendedHttpClient.GetAndReturnResponseAsync<DefaultResponseObject<List<ModuleInfoVm>>>($"/Modules/GetByListOfId?{requestString}");
    }

    public async Task<ActionResult<DefaultResponseObject<ModuleInfoVm>>> Update(UpdateModuleCommand request)
    {
        return await ExtendedHttpClient.PostAndReturnResponseAsync<UpdateModuleCommand, DefaultResponseObject<ModuleInfoVm>>(request, $"/Modules/Update");
    }
}