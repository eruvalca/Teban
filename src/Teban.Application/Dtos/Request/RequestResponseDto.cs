namespace Teban.Application.Dtos.Request
{
    public class RequestResponseDto<T>
    {
        internal RequestResponseDto(bool succeeded, IEnumerable<string> errors)
        {
            Succeeded = succeeded;
            Errors = errors.ToArray();
        }

        internal RequestResponseDto(bool succeeded, T data)
        {
            Succeeded = succeeded;
            Data = data;
        }

        public bool Succeeded { get; set; }
        public string[]? Errors { get; set; }
        public T? Data { get; set; }

        public static RequestResponseDto<T> Success(T data)
        {
            return new RequestResponseDto<T>(true, data);
        }

        public static RequestResponseDto<T> Failure(IEnumerable<string> errors)
        {
            return new RequestResponseDto<T>(false, errors);
        }

        public RequestResponseDto()
        {
        }
    }
}
