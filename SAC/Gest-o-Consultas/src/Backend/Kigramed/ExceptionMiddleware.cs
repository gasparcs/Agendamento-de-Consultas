using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace Kigramed
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError(exception, "Ocorreu uma exceção não tratada: {Message}", exception.Message);

            var response = context.Response;
            response.ContentType = "application/json";

            var errorResponse = new
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "Ocorreu um erro interno no servidor. Tente novamente mais tarde.",
                Details = exception.Message // Remover em produção para não expor detalhes
            };

            // Personalizar para tipos específicos de exceção, se necessário
            if (exception is ArgumentException)
            {
                response.StatusCode = (int)HttpStatusCode.BadRequest;
                errorResponse = new
                {
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Message = "Dados inválidos fornecidos.",
                    Details = exception.Message
                };
            }
            else if (exception is UnauthorizedAccessException)
            {
                response.StatusCode = (int)HttpStatusCode.Unauthorized;
                errorResponse = new
                {
                    StatusCode = (int)HttpStatusCode.Unauthorized,
                    Message = "Acesso não autorizado.",
                    Details = exception.Message
                };
            }
            // Adicionar mais tipos de exceção conforme necessário

            var result = JsonSerializer.Serialize(errorResponse);
            await response.WriteAsync(result);
        }
    }
}