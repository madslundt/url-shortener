using System.Threading.Tasks;
using dataModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using datamodel.Models;

namespace src.Services
{
    public class UrlLookup : IUrlLookup
    {
        private readonly DataContext _db;

        private class Url
        {
            public Guid Id { get; set; }
            public string RedirectUrl { get; set; }
        }

        public UrlLookup(DataContext db)
        {
            _db = db;
        }

        public async Task<string> GetUrl(string id)
        {
            bool isGuid = Guid.TryParse(id, out Guid guid);

            IQueryable<Url> query;
            if (isGuid)
            {
                query = from url in _db.Urls
                            where url.Id == guid
                            select new Url
                            {
                                Id = url.Id,
                                RedirectUrl = url.RedirectUrl
                            };
            }
            else
            {
                query = from url in _db.Urls
                            where url.ShortId == id
                            select new Url
                            {
                                Id = url.Id,
                                RedirectUrl = url.RedirectUrl
                            };
            }

            Url result = await query.FirstOrDefaultAsync().ConfigureAwait(false);

            if (result == null)
            {
                throw new ArgumentNullException($"Url with id {id} could not be found");
            }
            else
            {
                await ReportUrlHistory(result.Id).ConfigureAwait(false);

                return result.RedirectUrl;
            }
        }

        private async Task ReportUrlHistory(Guid guid)
        {
            UrlHistory history = new UrlHistory
            {
                UrlId = guid
            };

            await _db.UrlHistories.AddAsync(history).ConfigureAwait(false);

            await _db.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}