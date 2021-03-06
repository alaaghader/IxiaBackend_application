﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Models.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
        public double Lat { get; set; }
        public double Lon { get; set; }

        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Brand> Brands { get; set; }
    }
}
