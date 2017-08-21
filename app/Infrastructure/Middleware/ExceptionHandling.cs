namespace src.Infrastructure.Middleware
{
    using System;
    using System.IdentityModel.Tokens;
    using System.Security;
    using System.Threading.Tasks;
    using datamodel.Models;
    using dataModel;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Newtonsoft.Json;
    using src.Infrastructure.Configuration;

    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly DataContext _db;
        private readonly UrlSettings _urlSettings;

        public ExceptionHandlingMiddleware(RequestDelegate next, DataContext db, IOptionsSnapshot<UrlSettings> urlSettings)
        {
            _next = next;
            _db = db;

            if (urlSettings.Value == null)
            {
                throw new Exception($"Url settings have not been set");
            }

            _urlSettings = urlSettings.Value;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context).ConfigureAwait(false);
            }
            catch (Exception ex) when (ex is ArgumentNullException)
            {
                //Not found
                if (string.IsNullOrWhiteSpace(_urlSettings.RedirectUrl))
                {
                    context.Response.StatusCode = 404;
                    await ReportUrlError(ex.Message, context.Response.StatusCode).ConfigureAwait(false);
                    await context.Response.WriteAsync(ex.Message).ConfigureAwait(false);
                }
                else
                {
                    await ReportUrlError(ex.Message, 302).ConfigureAwait(false);
                    context.Response.Redirect(_urlSettings.RedirectUrl);
                }

                return;
            }
            catch (Exception ex) when (ex is SecurityException)
            {
                //Forbidden
                context.Response.StatusCode = 403;
                await ReportUrlError(ex.Message, context.Response.StatusCode).ConfigureAwait(false);

                await context.Response.WriteAsync(ex.Message).ConfigureAwait(false);

                return;
            }
            catch (Exception ex)
            {
                //Internal server error
                context.Response.StatusCode = 500;
                await ReportUrlError(ex.Message, context.Response.StatusCode).ConfigureAwait(false);

                await context.Response.WriteAsync(ex.Message).ConfigureAwait(false);

                return;
            }
        }

        private async Task ReportUrlError(string errorMessage, int statusCode)
        {
            UrlError error = new UrlError
            {
                ErrorMessage = errorMessage,
                StatusCode = statusCode
            };

            await _db.UrlErrors.AddAsync(error).ConfigureAwait(false);

            await _db.SaveChangesAsync().ConfigureAwait(false);
        }
    }
}