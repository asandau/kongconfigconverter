using System.Collections.Generic;

namespace kongconfigconverter
{
    public class KongInitFormat
    {
        public List<Service> services { get; set; }
        public List<Route> routes { get; set; }
        public List<Plugin> plugins { get; set; }
    }
}