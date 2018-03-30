using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPITest
{
    public enum EnumResponseStatus
    {
        OK = 200,
        MultipleChoices = 300,
        Unauthorized = 401,
        TemporaryRedirect = 307,
        Error = 500
    }


    public enum EnumResponseExceptions
    {
        OK = 200,
        MultipleChoices = 300,
        Unauthorized = 401,
        TemporaryRedirect = 307,
        Error = 500
    }
}