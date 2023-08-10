using AutoWrapper.Wrappers;
using log4net;
using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;
//using LogzLoggerProvider;

namespace DaprMicroserviceTemplate.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ExceptionMiddleware));
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            var requestContent = string.Empty;
            var request = httpContext?.Request;
            try
            {
                if ((request.Method == HttpMethods.Post || request.Method == HttpMethods.Put) && request.ContentLength > 0)
                {
                    request.EnableBuffering();
                    var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                    await request.Body.ReadAsync(buffer, 0, buffer.Length);
                    //get body string here...
                    requestContent = Encoding.UTF8.GetString(buffer);
                    request.Body.Position = 0;  //rewinding the stream to 0
                }
                var requestedData = PrepareRequestedData(httpContext, requestContent);

                //Please uncomment below line and allow for global start logs
                //   _logger.ServiceStarted(list: requestedData);

                await _next(httpContext);
            }
            catch (Exception ex)
            {
                var requestedData = PrepareRequestedData(httpContext, requestContent);
                await HandleExceptionAsync(httpContext, ex, requestedData);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception, object[] requestedData)
        {
            var path = context.Request?.Path;
            int statusCode;

            //Please uncomment below line and allow for global exception logs
            //_logger.ServiceFaulted(exception, path, requestedData);

            switch (true)
            {
                case bool _ when exception is ApiException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(exception.GetBaseException().Message);
        }

        private object[] PrepareRequestedData(HttpContext context, string requestBodyJson)
        {
            var request = context?.Request;
            var requestData = new object[]{
                request?.Method,
                requestBodyJson,
                request?.QueryString};
            return requestData;
        }

    }
}
