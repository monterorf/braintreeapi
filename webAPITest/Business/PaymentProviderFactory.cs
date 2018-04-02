using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPITest.Business
{
    public static class PaymentProviderFactory
    {
        public static IPaymentProvider Create()
        {
            return new BrainTreePaymentProvider();
        }

    }
}