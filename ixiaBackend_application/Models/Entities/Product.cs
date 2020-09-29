using System;
using System.Collections.Generic;
using System.Linq;
namespace ixiaBackend_application.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Price> Prices { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual Company Company { get; set; }
        public virtual Type Type { get; set; }
    }
}
