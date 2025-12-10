using Domain.Errors;
using Microsoft.AspNetCore.Mvc;

namespace Order.API.Extensions
{
    public static class ResultExtensions
    {
        public static IResult ToProplemDetails(this Error error)
        {
            var proplem = new ProblemDetails
            {
                Status = error.Code switch
                {
                    "NotFound" => StatusCodes.Status404NotFound,
                    "Presistance.Failure" => StatusCodes.Status500InternalServerError,
                    _ => StatusCodes.Status500InternalServerError
                },
                Detail = error.Message
            };

            return Results.Problem(proplem);
        }
    }
}
