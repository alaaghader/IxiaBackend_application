using System;

namespace ixiaBackend_application.Models.ModelsView
{
    public class PurchaseView
    {
        public UserView User { get; set; }
        public PriceView Price { get; set; }
        public DateTime PurchaseTime { get; set; }
        public string Comments { get; set; }
    }
}
