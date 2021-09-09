
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Serilog.Events;
using System;
using System.Diagnostics;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SocialGames.TechnicalTest.ApiService
{
    public class LogRequestMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogRequestMiddleWare> _logger;

        public LogRequestMiddleWare(RequestDelegate next, ILogger<LogRequestMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch sw = null;
            try
            {
                sw = Stopwatch.StartNew();
                await _next(context);
                sw.Stop();
                int? statusCode = context.Response?.StatusCode;
                if (statusCode > 399)
                {
                    _logger.LogError(GetLogMessage(context, sw));
                }
                else
                {
                    _logger.LogInformation(GetLogMessage(context, sw, LogEventLevel.Debug));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, GetLogMessage(context, sw));
            }
        }

        private static string GetLogMessage(HttpContext context, Stopwatch sw, LogEventLevel logEvent = LogEventLevel.Error)
        {
            return $"{DateTime.UtcNow} {logEvent} {context?.Request?.GetDisplayUrl()} {sw.Elapsed.TotalMilliseconds}";
        }
    }
}
