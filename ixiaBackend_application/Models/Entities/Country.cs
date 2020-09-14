using System.Collections.Generic;

namespace ixiaBackend_application.Models.Entities
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Price> Prices { get; set; }
    }
}
