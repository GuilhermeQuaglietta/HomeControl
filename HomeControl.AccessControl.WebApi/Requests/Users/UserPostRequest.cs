﻿namespace HomeControl.AccessControl.WebApi.Requests.Users
{
    public class UserPostRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}
