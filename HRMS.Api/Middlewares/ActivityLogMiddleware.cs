using HRMS.Core.Models.ActivityLog;
using HRMS.Core.Utilities.Auth;
using HRMS.Services.Interfaces;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Buffers;
using System.IO.Pipelines;
using System.Text;
using System.Globalization;

namespace HRMS.Api.Middlewares
{
    /// <summary>
    /// 
    /// </summary>
    public class ActivityLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ActivityLogMiddleware> _logger;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        public ActivityLogMiddleware(RequestDelegate next, ILogger<ActivityLogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        /// <param name="activityLogService"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext httpContext, IActivityLogService activityLogService)
        {
            if (httpContext != null)
            {
                try
                {
                    var jsonData = await GetInfo(httpContext);
                    if (jsonData != null)
                    {
                        await activityLogService.AddActivityLogs(jsonData);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Activity Log Middleware Invoke");
                }
            }
            await _next(httpContext);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private async Task<ActivityLogModel?> GetInfo(HttpContext context)
        {
            var request = context?.Request;
            if (request?.Method == "GET")
            {
                return null;
            }

            if (request?.ContentLength > 0)
            {
                request.EnableBuffering();
                request.Body.Position = 0;
                List<string> tmp = await GetListOfStringFromPipe(request.BodyReader, request?.ContentType);
                request.Body.Position = 0;

                string strInfoBody = "";
                if (request?.Path.Value.Contains("/api/auth/signin") == true)
                {
                    strInfoBody = "{\"value\": \"signin\"}";
                }
                else
                {
                    strInfoBody = string.Join("", tmp.ToArray());
                }
                string ipAddress = context?.Connection?.RemoteIpAddress?.ToString() ?? string.Empty;

                var t = JsonConvert.SerializeObject(strInfoBody);
                strInfoBody = JsonConvert.DeserializeObject<string>(t);

                return new ActivityLogModel
                {
                    Id = Guid.NewGuid(),
                    EventLocation = request.Path,
                    EventType = request.Method,
                    ActivityJson = strInfoBody?.Trim(','),
                    IPAddress = ipAddress.Contains("::1") || ipAddress.Contains(":::1") || ipAddress.Contains("0.0.0.1") ? "127.0.0.0" : ipAddress,
                };
            }
            return new ActivityLogModel();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        private async Task<List<string>> GetListOfStringFromPipe(PipeReader reader, string contentType)
        {
            List<string> results = new List<string>();

            while (true)
            {
                ReadResult readResult = await reader.ReadAsync();
                var buffer = readResult.Buffer;

                SequencePosition? position = null;

                do
                {
                    // Look for a EOL in the buffer
                    position = buffer.PositionOf((byte)'\n');

                    if (position != null)
                    {
                        var readOnlySequence = buffer.Slice(0, position.Value);
                        AddStringToList(results, in readOnlySequence, contentType);

                        // Skip the line + the \n character (basically position)
                        buffer = buffer.Slice(buffer.GetPosition(1, position.Value));
                    }
                }
                while (position != null);


                if (readResult.IsCompleted && buffer.Length > 0)
                {
                    AddStringToList(results, in buffer, contentType);
                }

                reader.AdvanceTo(buffer.Start, buffer.End);

                // At this point, buffer will be updated to point one byte after the last
                // \n character.
                if (readResult.IsCompleted)
                {
                    break;
                }
            }

            if (results.Count > 1 && results?.Last().Contains("}") == false)
            {
                results.Add("}");
            }
            return results;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="results"></param>
        /// <param name="readOnlySequence"></param>
        /// <param name="contentType"></param>
        private static void AddStringToList(List<string> results, in ReadOnlySequence<byte> readOnlySequence, string contentType)
        {
            // Separate method because Span/ReadOnlySpan cannot be used in async methods
            ReadOnlySpan<byte> span = readOnlySequence.IsSingleSegment ? readOnlySequence.First.Span : readOnlySequence.ToArray().AsSpan();
            var content = Encoding.UTF8.GetString(span)?.Trim() ?? string.Empty;
            var t = content.Any(x => x > 255);
            if (!t && !string.IsNullOrEmpty(content))
            {
                if (contentType.Contains("multipart/form-data"))
                {
                    var jsonValue = new string[2];
                    if (content.Contains("WebKitFormBoundary") && content.Contains("Content-Disposition:") && content.Contains("form-data;") && content.Contains("name="))
                    {
                        var value = content.Split("Content-Disposition: form-data; name=")?[1];
                        if (value != null)
                        {
                            jsonValue = value.TrimStart('"').Split("\"");
                        }
                    }
                    else if (content.Contains("Content-Disposition:") && content.Contains("form-data;") && content.Contains("name="))
                    {
                        if (content != null)
                        {
                            jsonValue = content.Replace("Content-Disposition: form-data;", "").Trim().TrimStart('"').Split("=");
                        }
                    }
                    else if (content.Contains("Content-Type:"))
                    {
                        jsonValue = content.Split(":")?.Count() > 0 ? content.Split(":") : null;
                    }
                    else if (content?.Contains(":") == false || (content?.Contains(":") == true && IsValidDateTimeTest(content)))
                    {
                        jsonValue = new string[] { "value", content };
                    }

                    if (jsonValue?.Length == 2 && jsonValue[0] != null && jsonValue[1] != null)
                    {
                        var data = new Dictionary<string, string>() { { jsonValue[0].Trim(';').Trim(), jsonValue[1].Trim().Trim('"') } };
                        content = JsonConvert.SerializeObject(data);
                        content = $"{content.Trim('{').Trim('}')},";

                        if (results == null || (results != null && results.Count == 0))
                        {
                            results?.Add("{");
                        }

                        if (results?.Count > 0 && content?.Contains("------WebKitFormBoundary") == false && content?.Contains(":") == true)
                        {
                            results?.Add(content);
                        }
                    }
                }
                else
                {
                    results?.Add(content);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private static bool IsValidDateTimeTest(string dateTime)
        {
            try
            {
                Convert.ToDateTime(dateTime);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
