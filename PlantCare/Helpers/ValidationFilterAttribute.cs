using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using PlantCare.Entities.Helpers;

namespace PlantCare.Helpers
{
    public class ValidationFilterAttribute : IActionFilter // validálja a bejövő adatokat, mielőtt azokat egy akció feldolgozná.
    {
        public void OnActionExecuting(ActionExecutingContext context) //akciók végrehajtása előtt fut le. Ezt a metódust használják a validációs ellenőrzések végrehajtására.
        {
            if (!context.ModelState.IsValid)
            {
                var error = new ErrorModel //hibaüzenet generálás 
                (
                    String.Join(',',
                    (context.ModelState.Values.SelectMany(t => t.Errors.Select(z => z.ErrorMessage))).ToArray()) //összefűzi a hibaüzeneteket
                );
                context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest; //400-as státuszkódot állít be a válaszban, ami azt jelzi, hogy a kérés helytelen.
                context.Result = new JsonResult(error); //A válaszban JSON formátumban visszaadja az összes hibát.
            }
        }
        public void OnActionExecuted(ActionExecutedContext context) { } //Ez a metódus az akció végrehajtása után fut le, de itt nem történik semmilyen művelet, mivel a filterünk nem igényel további műveleteket a válasz után.
    }
}
