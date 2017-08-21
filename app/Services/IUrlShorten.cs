using System.Threading.Tasks;
using static UrlShortener.Services.UrlShorten;

namespace src.Services
{
    public interface IUrlShorten
    {
        Task<Result> ShortenUrl(string url);
    }
}
