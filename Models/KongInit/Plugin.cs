using System.Collections.Generic;

namespace kongconfigconverter
{
    public class Plugin
    {
        public string name { get; set; }
        public bool enabled { get; set; }
        public string target { get; set; }
        public Dictionary<string, object> config { get; set; }
    }
}