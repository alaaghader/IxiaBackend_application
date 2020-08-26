using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Purchase> Purchases { get; set; }
        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual Company Company { get; set; }
        public virtual Category Category { get; set; }
    }
}
