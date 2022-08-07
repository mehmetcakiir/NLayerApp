using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;

namespace NLayer.API.Filters
{
    public class ValidateFilterAttribute : ActionFilterAttribute
    {
        //"Method çalıştığında" methodu override edilir.
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            //Uygulamada herhangi bir hata var ise (Tüm hatalar ModelState de toplanır.)
            if (!context.ModelState.IsValid)
            {
                //Uygulamadaki tüm hata mesajları errors a list şeklinde atanır.
                var errosrs = context.ModelState.Values.SelectMany(x => x.Errors).Select(z => z.ErrorMessage).ToList();


                // Clint kaynaklı hatalar için geri dönüş (Response oluşturulur)
                context.Result = new BadRequestObjectResult(CustomResponseDto<BaseDto>.Fail(400,errosrs));
            }
        }
    }
}
