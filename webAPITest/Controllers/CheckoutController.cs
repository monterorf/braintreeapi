using Braintree;
using System;
using System.Web.Http;
using System.Web.Mvc;
using webAPITest.Business;
using System.Web.Http.Cors;
using webAPITest.Models;
using System.Collections.Generic;

namespace webAPITest.Controllers
{
    [EnableCors(origins: "http://testbraintree.us-west-2.elasticbeanstalk.com", headers: "*", methods: "*")]
    public class CheckoutController : ApiController 
    {
        //private readonly IBraintreeConfiguration config = new BraintreeConfiguration();


        [System.Web.Http.HttpGet]
        public ResponseService<string> GetToken()
        {
            IPaymentProvider provider = PaymentProviderFactory.Create();
            return provider.GetToken<string>();
        }

        [System.Web.Http.HttpPost]
        public ResponseService<Transaction> CreatePayment(Models.PaymentRequest req)
        {
            IPaymentProvider provider = PaymentProviderFactory.Create();
            return provider.MakePayment<Transaction>(req);
        }
        
    }
}
