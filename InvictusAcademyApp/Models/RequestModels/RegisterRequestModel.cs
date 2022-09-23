﻿namespace InvictusAcademyApp.Models.RequestModels;

public class RegisterRequestModel
{
    public string Email { get; set; }
    public string Password { get; set; }
    // public string PasswordConfirm { get; set; }
    public string PhoneNumber { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string? InstagramLink { get; set; }
    public string Citizenship { get; set; }
}