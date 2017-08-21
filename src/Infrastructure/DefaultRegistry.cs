using StructureMap;
using src.Services;
using UrlShortener.Services;

namespace src.Infrastructure
{
    public class DefaultRegistry : Registry
    {
        public DefaultRegistry()
        {
            For<IUrlLookup>().Use<UrlLookup>().Transient();
            For<IUrlShorten>().Use<UrlShorten>().Transient();
        }
    }
}