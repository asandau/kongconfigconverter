using System.Collections.Generic;

namespace kongconfigconverter
{
    public class PluginAttributes
    {
        public bool enabled { get; set; }
        public Dictionary<string, object> config { get; set; }
    }
}