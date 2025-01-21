using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PlantCare.Entities.Helpers;

namespace PlantCare.Helpers
{
    public class ExceptionFilter : IExceptionFilter  //egy globális kivételkezelő filter. Amikor bármilyen kivétel történik az alkalmazásban, a filter biztosítja, hogy az alábbiak történjenek:
    {
        public void OnException(ExceptionContext context)
        {
            var error = new ErrorModel(context.Exception.Message); //A kivétel üzenete egy ErrorModel objektumba kerül.

            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;  //A válasz státuszkódja 500 (Internal Server Error) lesz.
            context.Result = new JsonResult(error);  //A válasz tartalma egy JSON objektum, amely tartalmazza a hibaüzenetet.
        }
    }
}




