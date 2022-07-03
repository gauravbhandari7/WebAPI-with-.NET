using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Wrappers
{
    public class Response<T>
    {
        public bool Succeeded { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public Response(T data, string message)
        {
            Succeeded = true;
            Message = message;
            Data = data;
        }
    }
}
