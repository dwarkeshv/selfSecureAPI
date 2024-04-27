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
}
