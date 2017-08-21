using System.Threading.Tasks;

namespace src.Services
{
    public interface IUrlLookup
    {
        Task<string> GetUrl(string id);
    }
}
