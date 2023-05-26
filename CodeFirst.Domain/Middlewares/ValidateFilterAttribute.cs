using CodeFirst.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Domain.Middlewares
{
    public class ValidateFilterAttribute : IAsyncResultFilter
    {
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
                (string Key, IEnumerable<string>)[] listErrors = context.ModelState
                                                                    .Where(x => x.Value.Errors.Count > 0)
                                                                    .Select(x => (x.Key, x.Value.Errors
                                                                                                .Select(x => x.ErrorMessage))
                                                                           )
                                                                    .ToArray();

                List<string> ErrorsValidations = new();

                foreach ((string Key, IEnumerable<string>) listErrosFields in listErrors)
                {
                    foreach (string errorField in listErrosFields.Item2)
                    {
                        ErrorsValidations.Add(errorField);
                    };
                }
                if (ErrorsValidations.Count != 0)
                {
                    throw new ValidationException(ErrorsValidations);
                }
            }

            await next();
        }
    }
}