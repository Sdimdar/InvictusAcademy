﻿namespace Identity.Application.Features.Users.Commands.GetUserData;

public class UserDataVm
{
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string? InstagramLink { get; set; }
    public string Citizenship { get; set; }

}