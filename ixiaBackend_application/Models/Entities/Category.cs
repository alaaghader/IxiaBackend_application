using System.Collections.Generic;

namespace ixiaBackend_application.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }

        public virtual ICollection<Sub_Category> Sub_Categories { get; set; }
    }
}
