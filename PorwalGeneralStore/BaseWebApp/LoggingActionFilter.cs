using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BaseWebApp
{
    public class LoggingActionFilter : ActionFilterAttribute
    {
        ILogger _logger;
        bool _isLogRequest = true;
        public LoggingActionFilter(bool isLogRequest = true)
        {
            _isLogRequest = isLogRequest;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (_isLogRequest)
            {
                // do something before the action executes
                _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' executing");
            }
            else
            {
                _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' executing");
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (_isLogRequest)
            {
                // do something before the action executes
                _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' executed");
            }
            else
            {
                _logger.LogInformation($"Action '{context.ActionDescriptor.DisplayName}' executed");
            }
        }
    }
}
