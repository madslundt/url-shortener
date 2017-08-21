using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using src.Services;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    public class UrlController : Controller
    {
        private readonly IUrlLookup _urlLookup;
        private readonly IUrlShorten _urlShorten;

        public UrlController(IUrlLookup urlLookup, IUrlShorten urlShorten)
        {
            _urlLookup = urlLookup;
            _urlShorten = urlShorten;
        }

        [HttpPost, Route("/")]
        public async Task<IActionResult> PostUrlAsync([FromQuery] string url)
        {
            UrlShorten.Result result = await _urlShorten.ShortenUrl(url).ConfigureAwait(false);

            return Ok(result);
        }

        [HttpGet, Route("/{shortId}")]
        public async Task<IActionResult> GetUrlAsync(string shortId)
        {
            string url = await _urlLookup.GetUrl(shortId).ConfigureAwait(false);

            return Redirect(url);
        }
    }
}
