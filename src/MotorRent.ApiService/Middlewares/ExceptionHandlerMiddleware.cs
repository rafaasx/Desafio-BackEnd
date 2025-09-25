using MotorRent.ApiService.Common;
using MotorRent.Domain.Constants;
using System.Net;
using System.Text.Json;

namespace MotorRent.ApiService.Middlewares
{
    public class ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                logger.LogInformation("Processando requisição {TraceId}", context.TraceIdentifier);
                await next(context);
                logger.LogInformation("Processado requisição {TraceId}", context.TraceIdentifier);

            }
            catch (KeyNotFoundException ex)
            {
                logger.LogWarning(ex, Messages.InvalidData, context.Response.StatusCode, context.TraceIdentifier);
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                context.Response.ContentType = "application/json";
                var problem = new ApiResponse { Mensagem = Messages.InvalidData };
                await context.Response.WriteAsync(JsonSerializer.Serialize(problem));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, Messages.GenericError, context.Response.StatusCode, context.TraceIdentifier);

                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new ApiResponse { Mensagem = Messages.GenericError };

                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
