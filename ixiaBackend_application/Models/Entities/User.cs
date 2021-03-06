﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ixiaBackend_application.Models.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Address { get; set; }
        public string Token { get; set; }
        public string Provider { get; set; }
        public string ProfilePicture { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
    }
}
