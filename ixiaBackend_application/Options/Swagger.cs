using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ixiaBackend_application.Options
{
    public class Swagger
    {
        public string RouteTemplate { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public string UiEndpoint { get; set; }
    }
}
