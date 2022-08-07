using Microsoft.AspNetCore.Diagnostics;
using NLayer.Core.DTOs;
using NLayer.Service.Exceptions;
using System.Text.Json;

namespace NLayer.API.Middlewares
{
    public static class UseCustomExeptionHandler
    {
        //IApplicationBuilder interface için bir UseCustomExeption adına middleware yazılır.
        public static void UseCustomExeption(this IApplicationBuilder app)
        {
            //Uygulamadaki tüm hataları yakalayan UseExceptionHandler Middleware ının içerisine girilir.
            app.UseExceptionHandler(config =>
            {
                //Run sonlandırıcı bir middleware dir. (Sonlandırıcı middleware yazılır.)
                config.Run(async context =>
                {
                    context.Response.ContentType = "aplication/json";


                    //Uygulamadaki tüm hatalar exceptionFeature a atanır.
                    var exceptionFeature = context.Features.Get<IExceptionHandlerFeature>();


                    //Hata hem uygulamadan hem de client tan kaynaklı olabilir

                    var statusCode = exceptionFeature.Error switch
                    {
                        //Hata Client kaynaklı ise 400
                        ClientSideException => 400,

                        //Değil ise 500 ata
                        _ => 500

                    };

                    context.Response.StatusCode = statusCode;

                    var response = CustomResponseDto<BaseDto>.Fail(statusCode, exceptionFeature.Error.Message);

                    //Json formatına çevrilir
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));

                });
            });
        }
    }
}
