using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Signawel.API.Extensions
{
    public static class StartupExtensions
    {
        public static void UseDeveloperExceptionJsonResponse(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(options =>
            {
                options.Run(
                    async context =>
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        context.Response.ContentType = "application/json";
                        var exception = context.Features.Get<IExceptionHandlerFeature>()?
                        .Error;
                        if (exception != null)
                        {
                            var json = JsonConvert.SerializeObject(exception);
                            await context.Response.WriteAsync(json).ConfigureAwait(false);
                        }
                    }
                );
            });
        }
    }
}