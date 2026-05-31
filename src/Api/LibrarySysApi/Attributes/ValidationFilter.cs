using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibrarySysApi.Attributes
{
    public class ValidationFilter<TRequest> : IAsyncActionFilter
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationFilter(IValidator<TRequest> validator)
        {
            _validator = validator;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var request = context.ActionArguments.Values.OfType<TRequest>().FirstOrDefault();
            if (request != null)
            {
                var result = await _validator.ValidateAsync(request);
                if (!result.IsValid)
                {
                    var errors = result.Errors
                                        .GroupBy(e => e.PropertyName)
                                        .ToDictionary(
                                            g => g.Key,
                                            g => g.Select(e => e.ErrorMessage).ToArray()
                                        );

                    context.Result = new BadRequestObjectResult(new { errors });
                    return;
                }
            }
            await next();
        }
    }
}
