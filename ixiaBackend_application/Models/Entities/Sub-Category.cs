using System.Collections.Generic;

namespace ixiaBackend_application.Models.Entities
{
    public class Sub_Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoUrl { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Type> Types { get; set; }
    }
}
