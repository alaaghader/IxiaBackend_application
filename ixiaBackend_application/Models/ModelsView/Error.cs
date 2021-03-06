﻿using System;
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

        public static Error ProductAlreadyOrdered()
            => new Error(nameof(ProductAlreadyOrdered), "You have already ordered this product before");

        public static Error WrongUsernameOrPassword()
            => new Error(nameof(WrongUsernameOrPassword), "Incorrect username or password");

        public static Error CountryAlreadyExists()
            => new Error(nameof(CountryAlreadyExists), "Country already exsits");

        public static Error PriceAlreadyExists()
            => new Error(nameof(PriceAlreadyExists), "Price already exsits");

        public static Error CurrencyAlreadyExists()
            => new Error(nameof(CurrencyAlreadyExists), "Currency already exsits");

        public static Error EmailAlreadyExists(string provider)
            => new Error(nameof(EmailAlreadyExists), $"Email account already exists, try to login with your " +
                $"'{provider}' account");

        public static Error EmailNotFound(string email, string field = "email")
            => new Error(nameof(EmailNotFound), $"No user with email '{email}' was found", field);
    }
}
