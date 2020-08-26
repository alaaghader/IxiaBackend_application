using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Models.ModelsView
{
    public class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }

        public Error(string code = null, string description = null, string path = null)
        {
            Code = code;
            Description = description;
            Path = path;
        }

        public static Error WrongUsernameOrPassword()
            => new Error(nameof(WrongUsernameOrPassword), "Incorrect username or password");

        public static Error EmailAlreadyExists()
            => new Error(nameof(EmailAlreadyExists), "Email account already exists");

        public static Error EmailNotFound(string email, string field = "email")
            => new Error(nameof(EmailNotFound), $"No user with email '{email}' was found", field);
    }
}
