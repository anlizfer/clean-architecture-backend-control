using CodeFirst.Domain.Enums;
using CodeFirst.Domain.Exceptions;
using CodeFirst.Domain.Helpers;
using CodeFirst.Domain.Wrappers;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Domain.Middlewares
{
    public class GlobalErrorException
    {
        private string _requestBody;
        private readonly DateTime _beginTime;
        private readonly Stopwatch _sw;
        private readonly RequestDelegate _next;
        public GlobalErrorException(RequestDelegate next)
        {
            _requestBody = string.Empty;
            _beginTime = DateTime.Now;
            _sw = Stopwatch.StartNew();
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                _requestBody = await GetDataRequest(context);
                await _next(context).ConfigureAwait(false);
            }
            catch (Exception error)
            {
                var response = context.Response;
                var responseModel = new Response<string>() { Succeeded = false, Message = error?.Message };

                await TypeOfErrorAsync(error, response, responseModel);
                await HandleExceptionAsync(_requestBody, context, error, response, responseModel, _sw, _beginTime);
            }
        }

        private static async Task HandleExceptionAsync(
            string _requestBody,
            HttpContext context,
            Exception ex,
            HttpResponse response,
            Response<string> responseModel,
            Stopwatch _sw,
            DateTime _beginTime
            )
        {
            //for client aplication
            var errorId = Activity.Current?.Id ?? context.TraceIdentifier;
            responseModel.CodeError = $"Código error: {@errorId}";
            responseModel.Message = $"Error: {responseModel.Message}";
            var result = JsonConvert.SerializeObject(responseModel, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            //Erros logs
            await LogHelper.LogError(
                 ex.Source,
                 _requestBody,
                 result,
                 _sw,
                 ex,
                 response.StatusCode,
                 _beginTime,
                 context
                 );
            if (response.StatusCode != 404 && response.StatusCode != 500) response.StatusCode = 500;
            await response.WriteAsync(result).ConfigureAwait(false);
        }

        private static async Task TypeOfErrorAsync(
            Exception error,
            HttpResponse response,
            Response<string> responseModel)
        {
            response.ContentType = "application/json";
            response.StatusCode = (int)GetErrorCode(error.GetType());

            switch (error)
            {
                case CoreException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case InfrastructureException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                case ValidationException e:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    responseModel.Errors = e.Errors;
                    break;

                case KeyNotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                default:
                    break;
            }
            await Task.FromResult(response);
        }

        private static HttpStatusCode GetErrorCode(Type exceptionType)
        {
            if (Enum.TryParse(exceptionType.Name, out TypeExceptions tryParseResult))
            {
                return tryParseResult switch
                {
                    TypeExceptions.NullReferenceException => HttpStatusCode.LengthRequired,
                    TypeExceptions.FileNotFoundException => HttpStatusCode.NotFound,
                    TypeExceptions.OverflowException => HttpStatusCode.RequestedRangeNotSatisfiable,
                    TypeExceptions.OutOfMemoryException => HttpStatusCode.ExpectationFailed,
                    TypeExceptions.InvalidCastException => HttpStatusCode.PreconditionFailed,
                    TypeExceptions.ObjectDisposedException => HttpStatusCode.Gone,
                    TypeExceptions.UnauthorizedAccessException => HttpStatusCode.Unauthorized,
                    TypeExceptions.NotImplementedException => HttpStatusCode.NotImplemented,
                    TypeExceptions.NotSupportedException => HttpStatusCode.NotAcceptable,
                    TypeExceptions.InvalidOperationException => HttpStatusCode.MethodNotAllowed,
                    TypeExceptions.TimeoutException => HttpStatusCode.RequestTimeout,
                    TypeExceptions.ArgumentException => HttpStatusCode.BadRequest,
                    TypeExceptions.StackOverflowException => HttpStatusCode.RequestedRangeNotSatisfiable,
                    TypeExceptions.FormatException => HttpStatusCode.UnsupportedMediaType,
                    TypeExceptions.IOException => HttpStatusCode.NotFound,
                    TypeExceptions.IndexOutOfRangeException => HttpStatusCode.ExpectationFailed,
                    _ => HttpStatusCode.InternalServerError,
                };
            }
            else
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        private static async Task<string> GetDataRequest(HttpContext context)
        {
            var request = context.Request;

            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer);
            var requestContent = Encoding.UTF8.GetString(buffer);
            request.Body.Position = 0;

            return string.IsNullOrEmpty(requestContent) ? string.Empty : requestContent;
        }
    }
}