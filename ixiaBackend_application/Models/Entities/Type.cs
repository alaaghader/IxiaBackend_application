using System.Collections.Generic;

namespace ixiaBackend_application.Models.Entities
{
    public class Type
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual Sub_Category Sub_Category { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
