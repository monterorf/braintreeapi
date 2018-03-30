using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPITest.Models
{
    public class ResponseService<T>
    {
        public T Data { get; set; }
        public bool Error { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }


        public ResponseService()
            : this(false) { }

        public ResponseService(bool error)
            : this(error, null) { }

        public ResponseService(bool error, string mesage)
        {
            Error = error;
            Code = (int)((Error) ? EnumResponseStatus.Error : EnumResponseStatus.OK);
            Message = mesage;
        }
    }
}