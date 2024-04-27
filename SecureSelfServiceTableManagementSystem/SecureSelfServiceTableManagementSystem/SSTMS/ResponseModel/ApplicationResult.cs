using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace SSTMS.ResponseModel
{
    public class ApplicationActionResult<T>
    {
        public ApplicationActionStatusCodes StatusCode { get; set; }
        public string StatusText { get; set; } = "";
        public string StatusMessage { get; set; } = "";
        public T ResultObject { get; set; } = default;
    }

    public enum ApplicationActionStatusCodes
    {
        Success = 200,
        BadRequest = 400,
        UnAuthorized_User = 401,
        UnAuthorized_Access = 403,
        Error = 500
    }

    //extensions
    public static class ApplicationActionResultExtension
    {
        public static string GetJSON<T>(this ApplicationActionResult<T> result)
        {
            return JsonConvert.SerializeObject(result);
        }
    }

    public static class ApplicationActionStatusCodesExtension
    {
        public static (int Code, string Text) GetStatusData(this ApplicationActionStatusCodes statusCode)
        {
            var text = Regex.Matches(statusCode.ToString(), @"([A-Z][a-z]+)").Cast<Match>().Select(m => m.Value);
            return (Code: (int)statusCode, Text: string.Join(" ", text));
        }
    }
}
