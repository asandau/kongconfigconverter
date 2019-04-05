using YamlDotNet.Serialization;

namespace kongconfigconverter
{
    public class Route
    {
        public string name { get; set; }
        public RouteConfig config { get; set; }
        [YamlMember(Alias = "apply_to", ApplyNamingConventions = false)]
        public string applyTo { get; set; }
    }
}