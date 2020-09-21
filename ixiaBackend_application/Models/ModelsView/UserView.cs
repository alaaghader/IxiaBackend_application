using Microsoft.AspNetCore.Mvc;
using System;

namespace ixiaBackend_application.Models.ModelsView
{
    public class UserView
    {
        public String Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Token { get; set; }
        public string Provider { get; set; }
        public string ProfilePicture { get; set; }
    }
}
