using System;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using datamodel.Models;
using dataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using src.Infrastructure.Configuration;
using src.Services;

namespace UrlShortener.Services
{
    public class UrlShorten : IUrlShorten
    {
        private const int RETRIES = 10;

        private readonly DataContext _db;
        private readonly UrlSettings _urlSettings;

        public class Result
        {
            public Guid Id { get; set; }
            public string ShortId { get; set; }
        }

        public UrlShorten(DataContext db, IOptionsSnapshot<UrlSettings> urlSettings)
        {
            _db = db;

            if (urlSettings.Value == null)
            {
                throw new Exception($"Url settings have not been set");
            }

            _urlSettings = urlSettings.Value;
        }

        public async Task<Result> ShortenUrl(string url)
        {
            url = System.Net.WebUtility.UrlDecode(url);
            url = SetUrl(url);

            bool isValid = IsUrlValid(url);

            if (!isValid)
            {
                throw new SecurityException("Url is not valid");
            }

            Result result = await GetUrl(url).ConfigureAwait(false);
            if (result == null)
            {
                result = await InsertUrl(url).ConfigureAwait(false);
            }

            return result;
        }

        private bool IsUrlValid(string url)
        {
            string[] domains = _urlSettings.Domains.Split(',');
            foreach (string domain in domains)
            {
                string domainUrl = domain.Trim();
                if (!domainUrl.StartsWith("http"))
                {
                    domainUrl = "https?://" + domain;
                }

                Regex regex = new Regex(domainUrl + ".*");
                Match match = regex.Match(url);

                if (match.Success)
                {
                    return true;
                }
            }


            return false;
        }

        private async Task<Result> InsertUrl(string url)
        {
            string shortId = await TryGetShortId().ConfigureAwait(false);

            Url newUrl = new Url
            {
                ShortId = shortId,
                RedirectUrl = url
            };

            await _db.Urls.AddAsync(newUrl).ConfigureAwait(false);
            await _db.SaveChangesAsync().ConfigureAwait(false);

            Result result = new Result
            {
                Id = newUrl.Id,
                ShortId = newUrl.ShortId
            };

            return result;
        }

        private async Task<Result> GetUrl(string url)
        {
            var query = from urls in _db.Urls
                        where urls.RedirectUrl == url
                        select new Result
                        {
                            Id = urls.Id,
                            ShortId = urls.ShortId
                        };

            Result result = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            return result;
        }

        private string SetUrl(string url)
        {
            if (!url.StartsWith("http"))
            {
                url = "http://" + url;
            }

            Uri result = new Uri(url);

            return result.AbsoluteUri;
        }

        private async Task<string> TryGetShortId()
        {
            for (int i = 0; i < RETRIES; i++)
            {
                string shortId = Generate();

                bool exists = await _db.Urls.AnyAsync(u => u.ShortId == shortId).ConfigureAwait(false);

                if (!exists)
                {
                    return shortId;
                }
            }

            throw new Exception("Failed to generate a unique short id");
        }

        private string Generate()
        {
            StringBuilder result = new StringBuilder();

            Random random = new Random(Guid.NewGuid().GetHashCode());

            int length = _urlSettings.ShortIdLength;
            while (length-- > 0)
            {
                result.Append(_urlSettings.ShortIdCharacters[random.Next(_urlSettings.ShortIdCharacters.Length)]);
            }

            return result.ToString();
        }
    }
}