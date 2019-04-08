using System.Collections.Generic;

namespace kongconfigconverter
{
    public class ConfigConverter
    {
        public KongInitFormat convert(KongfigFormat kongfigFormat) {
            var kongInitFormat = new KongInitFormat();
            var services = new List<Service>();
            var routes = new List<Route>();
            var plugins = new List<Plugin>();

            foreach (var api in kongfigFormat.apis) {
                var service = addToServices(services, toService(api));
                var route = toRoute(api, service.name);
                routes.Add(route);
                plugins = mergePlugins(plugins, toPlugins(api));
            }

            kongInitFormat.plugins = plugins;
            kongInitFormat.services = services;
            kongInitFormat.routes = routes;

            return kongInitFormat;
        }

        private List<Plugin> mergePlugins(List<Plugin> plugins, List<Plugin> newPlugins) {
            foreach(var newPlugin in newPlugins) {
                var existingPlugin = existsAlready(plugins, newPlugin);
                if(existingPlugin == null) {
                    plugins.Add(newPlugin);
                } else {
                    var targetName = newPlugin.target.Split("[")[1].Split("]")[0];
                    existingPlugin.target = addTarget(existingPlugin.target, targetName);
                }
            }
            return plugins;
        }

        private string addTarget(string target, string targetName)
        {
            return $"{target.Split("]")[0]}, {targetName}]";
        }

        private Plugin existsAlready(List<Plugin> plugins, Plugin plugin) {
            foreach (var existingPlugin in plugins) {
                if(existingPlugin.name == plugin.name) {
                    return existingPlugin;
                }
            }
            return null;
        }

        private Service addToServices(List<Service> services, Service service) {
            var existSameUrl = false;
            foreach (var existingService in services) {
                if(same(existingService, service)) {
                    return existingService;
                } else if(sameName(existingService, service)) {
                    existSameUrl = true;
                }
            }
            if (existSameUrl) {
                service.name = service.name + "-1";
            }
            services.Add(service);
            return service;
        }

        private bool same(Service service1, Service service2) {
            return service1.retries == service2.retries
                && service1.url == service2.url
                && service1.connectTimeout == service2.connectTimeout
                && service1.readTimeout == service2.readTimeout
                && service1.writeTimeout == service2.writeTimeout;
        }

        private bool sameName(Service service1, Service service2) {
            return service1.name == service2.name;
        }

        private Service toService(Api api) {
            var service = new Service();
            var uriParts = api.attributes.upstreamUrl.Split("/");
            service.name = uriParts[uriParts.Length - 1];
            service.url = api.attributes.upstreamUrl;
            service.retries = api.attributes.retries;
            service.connectTimeout = api.attributes.upstreamConnectTimeout;
            service.readTimeout = api.attributes.upstreamReadTimeout;
            service.writeTimeout = api.attributes.upstreamSendTimeout;
            return service;
        }

        private Route toRoute(Api api, string serviceName) {
            var route = new Route();
            route.name = api.name;
            route.applyTo = serviceName;
            var config = new RouteConfig();
            config.stripPath = api.attributes.stripUri;
            config.preserveHosts = api.attributes.preserveHost;
            config.paths = api.attributes.uris;
            config.methods = api.attributes.methods;
            var protocols = new List<string>();
            protocols.Add("https");
            if(!api.attributes.httpsOnly) {
                protocols.Add("http");
            }
            config.protocols = protocols;
            route.config = config;
            return route;
        }

        private List<Plugin> toPlugins(Api api) {
            var plugins = new List<Plugin>();
            if(api.plugins != null) {
                foreach (var oldPlugin in api.plugins) {
                    plugins.Add(toPlugin(oldPlugin, api.name));
                }
            }
            return plugins;
        }

        private Plugin toPlugin(OldPlugin oldPlugin, string routeName) {
            var plugin = new Plugin();
            plugin.name = oldPlugin.name;
            plugin.enabled = oldPlugin.attributes.enabled;
            plugin.target = $"r[{routeName}]";
            plugin.config = oldPlugin.attributes.config;
            return plugin;
        }    }
}