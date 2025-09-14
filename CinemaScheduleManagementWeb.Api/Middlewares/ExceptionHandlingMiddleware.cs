using Microsoft.AspNetCore.Http;

namespace CinemaScheduleManagementWeb.Api.Middlewares
{
    /// <summary>
    /// Класс перехвата и обработки исключений.
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="next">Запрос.</param>
        /// <param name="logger">Логгер.</param>
        public ExceptionHandlingMiddleware(RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        /// <summary>
        /// Метод перехвата запроса.
        /// </summary>
        /// <param name="context">Контекст запроса.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }

            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Метод обрабатывает исключение и формирует HTTP ответ.
        /// </summary>
        /// <param name="context">Контекст запроса.</param>
        /// <param name="exception">Исключение.</param>
        /// <returns>Обработанное исключение.</returns>
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            switch (exception)
            {
                case InvalidOperationException:
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    break;
                default:
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            return context.Response.WriteAsJsonAsync(new
            {
                StatusCode = context.Response.StatusCode,
                Details = exception.Message
            });
        }
    }
}
