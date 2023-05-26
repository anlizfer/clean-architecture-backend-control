using CodeFirst.Domain.Models;
using Microsoft.AspNetCore.Http;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;

namespace CodeFirst.Domain.Helpers
{
    public static class LogHelper
    {
        public static async Task LogError(
            string product,
            string _requestBody,
            string _response,
            Stopwatch _sw,
            Exception ex,
            int StatusCode,
            DateTime _beginTime,
            HttpContext context
            )
        {

            var detail = GetLogDetail(product, _requestBody, _response, context, null, ex, StatusCode, _sw, _beginTime).Result;

            await WritingLogs(detail);
        }

        public static async Task<LogDetail> GetLogDetail(
            string product,
            string _requestBody,
            string _response,
            HttpContext context,
            Dictionary<string, object> AditionalInfo,
            Exception ex,
            int StatusCode,
            Stopwatch _sw,
            DateTime _beginTime
            )
        {
            var detail = new LogDetail()
            {
                CorrelationalId = Activity.Current?.Id ?? context.TraceIdentifier,
                Product = product,
                StatusCode = StatusCode,
                ActivityName = $"{context.Request.Path} - {context.Request.Method}",
                Location = ex.Source,
                Ip = UtilitiesHelper.GetClientIPAddress(context),
                HostName = Environment.MachineName,
                Exception = ex,
                StackTrace = ex.StackTrace,
                InnerException = ex.InnerException?.Message.Replace("'", "") ?? string.Empty,
                RequestBody = _requestBody,
                Response = _response,
                AditionalInfo = AditionalInfo ?? new Dictionary<string, object>(),
                UserId = UtilitiesHelper.GetClientId(context),
                UserName = UtilitiesHelper.GetClientName(context)
            };
            _sw.Stop();
            detail.ElapsedMilliSeconds = _sw.ElapsedMilliseconds;
            detail.AditionalInfo = new Dictionary<string, object>()
              {
                {"Started", _beginTime.ToString(CultureInfo.InvariantCulture) }
              };
            return await Task.FromResult(detail);
        }

        public static async Task WritingLogs(LogDetail detail)
        {
            await Task.Run(() =>
            {
                Log.ForContext("CorrelationalId", detail.CorrelationalId, destructureObjects: true)
                   .ForContext("RequestBody", detail.RequestBody, destructureObjects: true)
                   .ForContext("Response", detail.Response, destructureObjects: true)
                   .ForContext("StatusCode", detail.StatusCode, destructureObjects: true)
                   .ForContext("StackTrace", detail.StackTrace, destructureObjects: true)
                   .ForContext("InnerException", detail.InnerException, destructureObjects: true)
                   .ForContext("Product", detail.Product, destructureObjects: true)
                   .ForContext("ActivityName", detail.ActivityName, destructureObjects: true)
                   .ForContext("Location", detail.Location, destructureObjects: true)
                   .ForContext("HostName", detail.HostName, destructureObjects: true)
                   .ForContext("UserId", detail.UserId, destructureObjects: true)
                   .ForContext("UserName", detail.UserName, destructureObjects: true)
                   .ForContext("IP", detail.Ip, destructureObjects: true)
                   .ForContext("ElapsedMilliSeconds", detail.ElapsedMilliSeconds, destructureObjects: true)
                   .ForContext("AditionalInfo", detail.AditionalInfo, destructureObjects: true)
                   .Error(detail.Exception, $"{detail.Exception.Message} {detail.CorrelationalId}", detail.CorrelationalId);

            });
        }
    }
}