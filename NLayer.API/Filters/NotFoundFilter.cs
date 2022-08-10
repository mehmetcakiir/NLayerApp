using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Services;

namespace NLayer.API.Filters
{
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> service;

        public NotFoundFilter(IService<T> service)
        {
            this.service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Propertide ilk olan değer aşlınır.(id)
            var idValue = context.ActionArguments.Values.FirstOrDefault();

            if (idValue == null)
            {
                //İd değeri gelmiyorsa yoluna devam et
                await next.Invoke();
                return;
            }

            var id = (int)idValue;

            var enyEntity = await service.AnyAsycn(x => x.Id == id);


            //var ise yoluna devam etsin
            if (enyEntity)
            {
                await next.Invoke();
                return;

            } 
            context.Result = new NotFoundObjectResult(CustomResponseDto<BaseEntity>.Fail(404, $"{typeof(T).Name} {id} not fount"));

        }
    }
}
