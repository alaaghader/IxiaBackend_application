using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Models.ModelsView
{
    public class PurchaseView
    {
        public UserView User { get; set; }
        public ProductView Product { get; set; }
        public DateTime PurchaseTime { get; set; }
        public string Comments { get; set; }
    }
}
