using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.ModelsInput
{
    public class ExternalLoginInput
    {
        public string UserProvider { get; set; }
        public string ReturnUrl { get; set; }
    }
}
