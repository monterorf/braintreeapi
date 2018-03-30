using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using webAPITest.Business;

namespace webAPITest.Controllers
{
    public class TokenController : ApiController
    {
        public IBraintreeConfiguration config = new BraintreeConfiguration();
        public string Get()
        {            
            var gateway = config.GetGateway();
            var clientToken = gateway.ClientToken.generate();
            return  clientToken;
        }
    }
}
