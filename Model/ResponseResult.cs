using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class ResponseResult
    {
        private readonly int[] StatusCodeValid = { 200, 201, 202, 300, 400, 404, 500 , 0001 , 10,11,12,13,14,15,16,20,21,22,23,30,31,32,33,40,41};
        private int _statusCode;
        private string? _message;

        public int StatusCode
        {
            get => _statusCode;
            set
            {
                if (!StatusCodeValid.Contains(value)) { throw new InvalidOperationException("Mã trạng thái không hợp lệ"); }
                _statusCode = value;
            }
        }
        public string? Message { get => _message; set => _message = value; }
    }
    public class ResponseResult<T> : ResponseResult 
    {
        private T? _data;
        public T? Data { get => _data; set => _data = value; }
    }
}
