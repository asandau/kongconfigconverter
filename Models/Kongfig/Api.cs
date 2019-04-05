using System.Collections.Generic;

namespace kongconfigconverter
{
    public class Api
    {
        public string name { get; set; }
        public string ensure { get; set; }
        public List<OldPlugin> plugins { get; set; } 
        public Attributes attributes { get; set; }
    }
}