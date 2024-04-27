using Newtonsoft.Json;

namespace SSTMS.ResponseModel
{
    public class ResponseModel<T>
    {
        public ResponseModel()
        {
            IsSuccess = true;
            Message = "";
        }
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonIgnore]
        public Exception Exception { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
        public T Data { get; set; }
        [JsonProperty("isPagination", NullValueHandling = NullValueHandling.Ignore)]
        public bool? isPagination { get; set; }

        //public PaginationDetail PaginationDetails { get; set; }
    }
}
