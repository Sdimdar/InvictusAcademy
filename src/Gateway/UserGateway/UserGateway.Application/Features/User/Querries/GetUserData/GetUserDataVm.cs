﻿namespace UserGateway.Application.Features.User.Querries.GetUserData;

public class GetUserDataVm
{
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string? InstagramLink { get; set; }
    public string Citizenship { get; set; }
    public string RegistrationDate { get; set; }
    public string City { get; set; }
}
