using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace kongconfigconverter
{
    public class RouteConfig
    {
        public List<string> paths { get; set; }
        [YamlMember(Alias = "strip_path", ApplyNamingConventions = false)]        public bool stripPath { get; set; }
        public List<string> protocols { get; set; }
        public List<string> methods { get; set; }
        public List<string> hosts { get; set; }
        [YamlMember(Alias = "preserve_host", ApplyNamingConventions = false)] 
        public bool preserveHosts { get; set; }
    }
}