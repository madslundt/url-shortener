using System.Threading.Tasks;
using UrlShortener.Services;
using static UrlShortener.Services.UrlShorten;

namespace src.Services
{
    public interface IUrlShorten
    {
        Task<Result> ShortenUrl(UrlShorten.Request request);
    }
}
