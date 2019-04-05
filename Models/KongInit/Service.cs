using YamlDotNet.Serialization;

namespace kongconfigconverter
{
    public class Service
    {
        public string name { get; set; }
        public string url { get; set; }
        public long retries { get; set; }
        [YamlMember(Alias = "connect_timeout", ApplyNamingConventions = false)]
        public long connectTimeout { get; set; }
        [YamlMember(Alias = "write_timeout", ApplyNamingConventions = false)]
        public long writeTimeout { get; set; }
        [YamlMember(Alias = "read_timeout", ApplyNamingConventions = false)]
        public long readTimeout { get; set; }
    }
}