using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace kongconfigconverter
{
    public class Attributes
    {
        public object hosts { get; set; }
        public List<string> uris { get; set; }
        public List<string> methods { get; set; }
        [YamlMember(Alias = "strip_uri", ApplyNamingConventions = false)]
        public bool stripUri { get; set; }
        [YamlMember(Alias = "preserve_host", ApplyNamingConventions = false)]
        public bool preserveHost { get; set; }
        [YamlMember(Alias = "upstream_url", ApplyNamingConventions = false)]
        public string upstreamUrl { get; set; }
        public long retries { get; set; }
        [YamlMember(Alias = "upstream_connect_timeout", ApplyNamingConventions = false)]
        public long upstreamConnectTimeout { get; set; }
        [YamlMember(Alias = "upstream_read_timeout", ApplyNamingConventions = false)]
        public long upstreamReadTimeout { get; set; }
        [YamlMember(Alias = "upstream_send_timeout", ApplyNamingConventions = false)]
        public long upstreamSendTimeout { get; set; }
        [YamlMember(Alias = "https_only", ApplyNamingConventions = false)]
        public bool httpsOnly { get; set; }
        [YamlMember(Alias = "http_if_terminated", ApplyNamingConventions = false)]
        public bool httpIfTerminated { get; set; }
    }
}