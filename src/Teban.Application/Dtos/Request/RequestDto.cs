namespace Teban.Application.Dtos.Request
{
    public class RequestDto<T>
    {
        internal RequestDto(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        internal RequestDto(bool succeeded, T data)
        {
            Succeeded = succeeded;
            Data = data;
        }

        public bool Succeeded { get; set; }
        public string[]? Errors { get; set; }
        public T? Data { get; set; }

        public static RequestDto<T> Success(T data)
        {
            return new RequestDto<T>(true, data);
        }

        public static RequestDto<T> Failure(IEnumerable<string> errors)
        {
            return new RequestDto<T>(false, errors);
        }

        public RequestDto()
        {
        }
    }
}
