using InvictusAcademyApp.Enums;

namespace InvictusAcademyApp.Models.RequestModels;

public class DefaultResponse
{
    public DefaultResponse()
    {
    }

    public DefaultResponse(ResponseStatusType responseStatus)
    {
        ResponseStatus = responseStatus;
    }

    public ResponseStatusType ResponseStatus { get; set; }
}