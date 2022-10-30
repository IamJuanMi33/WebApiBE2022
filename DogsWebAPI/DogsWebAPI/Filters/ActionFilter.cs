using Microsoft.AspNetCore.Mvc.Filters;

namespace DogsWebAPI.Filters
{
    public class ActionFilter : IActionFilter
    {
        private readonly ILogger<ActionFilter> log;

        public ActionFilter(ILogger<ActionFilter> log)
        {
            this.log = log;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            log.LogInformation("Antes de ejecutar la acción");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            log.LogInformation("Después de ejecutar la acción");
        }
    }
}
