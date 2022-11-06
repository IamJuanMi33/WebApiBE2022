using Microsoft.AspNetCore.Mvc.Filters;

namespace DogsWebAPISeg.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly ILogger<ExceptionFilter> log;

        public ExceptionFilter(ILogger<ExceptionFilter> log)
        {
            this.log = log;
        }

        public override void OnException(ExceptionContext context)
        {
            log.LogError(context.Exception, context.Exception.Message);

            base.OnException(context);
        }
    }
}
