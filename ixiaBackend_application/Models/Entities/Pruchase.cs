using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Models.Entities
{
    public class Purchase
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime PurchaseTime { get; set; }
        public string Comments { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
