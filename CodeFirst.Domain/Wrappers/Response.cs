using System.Collections.Generic;

namespace CodeFirst.Domain.Wrappers
{
    public class Response<T>
    {
        public Response()
        {
        }

        public Response(T data)
        {
            Succeeded = true;
            CodeError = string.Empty;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }

        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public List<string> Errors { get; set; }
        public string CodeError { get; set; }
        public string Message { get; set; }
    }
}