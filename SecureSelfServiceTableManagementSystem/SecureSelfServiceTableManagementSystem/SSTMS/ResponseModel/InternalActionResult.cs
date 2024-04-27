namespace SSTMS.ResponseModel
{

    public class InternalActionResult<T> : ApplicationActionResult<T>
    {
        public bool StopProccess { get; set; }
        public static InternalActionResult<T> CreateSuccessResponse(T response)
        {
            return new InternalActionResult<T>()
            {
                StatusCode = ApplicationActionStatusCodes.Success,
                ResultObject = response
            };
        }
    }
    public class FactoryResponse<T>
    {
        public static InternalActionResult<T> CreateSuccess(T response)
        {
            return new InternalActionResult<T>()
            {
                StatusCode = ApplicationActionStatusCodes.Success,
                StopProccess = true,
                ResultObject = response
            };
        }

        public static InternalActionResult<T> CreateFail(Exception ex)
        {
            return new InternalActionResult<T>()
            {
                StatusCode = ApplicationActionStatusCodes.Error,
                StopProccess = true,
                StatusMessage = ex.StackTrace,
                StatusText = string.Concat(ex.Source, "__", ex.Message)
            };
        }

        public static InternalActionResult<T> CreateFail(T response)
        {
            return new InternalActionResult<T>()
            {
                StatusCode = ApplicationActionStatusCodes.Success,
                StopProccess = true,
                ResultObject = response
            };
        }
    }
}
