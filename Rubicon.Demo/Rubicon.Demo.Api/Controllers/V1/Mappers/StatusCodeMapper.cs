using Microsoft.AspNetCore.Mvc;

namespace Rubicon.Demo.Api.Controllers.V1.Mappers
{
    public static class StatusCodeMapper
    {
        public static IActionResult Map(Domain.Enums.ResultStatus status, string message)
        {
            switch (status)
            {
                case Domain.Enums.ResultStatus.Success:
                    return new OkResult();
                case Domain.Enums.ResultStatus.Error:
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                case Domain.Enums.ResultStatus.UnAuthorized:
                    return new UnauthorizedResult();
                case Domain.Enums.ResultStatus.NotFound:
                    return new NotFoundResult();
                case Domain.Enums.ResultStatus.ValidationFailed:
                    return new BadRequestObjectResult(message);
                default:
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
