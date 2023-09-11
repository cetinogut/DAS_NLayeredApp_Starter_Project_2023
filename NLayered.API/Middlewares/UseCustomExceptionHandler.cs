using Microsoft.AspNetCore.Diagnostics;
using NLayered.Core.DTOs;
using NLayered.Service.Exceptions;
using System.Text.Json;

namespace NLayered.API.Middlewares
{
    public static class UseCustomExceptionHandler
    {

        public static void UseCustomException(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(config =>
            {

                config.Run(async context =>
                {
                    context.Response.ContentType = "application/json";

                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();

                    var statusCode = exceptionFeature.Error switch
                    {
                        ClientSideException => 400,
                        NotFoundExcepiton => 404,
                        _ => 500
                    };
                    context.Response.StatusCode = statusCode;


                    var response = CustomResponseDto<NoContentDto>.Fail(statusCode, exceptionFeature.Error.Message);


                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));//middlewarelerde otomatik JSON dönüş olmaz, controllerlarda ise JSON dönüş vardır. Middlewarede kendimzi convert etmeliyiz.

                });

            });

        }
    }
}
