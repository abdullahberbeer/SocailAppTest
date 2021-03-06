using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using ServerAppTwo.Data;

namespace ServerAppTwo.Helpers
{
    public class LastActiveActionFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext= await next();
            var id = int.Parse(resultContext.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var repository = (ISocialRepository)resultContext.HttpContext.RequestServices.GetService(typeof(ISocialRepository));
            var user = await repository.GetUser(id);
            user.LastActive=DateTime.Now;
            await repository.SaveChanges();
        }
    }
}