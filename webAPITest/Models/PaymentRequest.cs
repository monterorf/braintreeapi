using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace webAPITest.Models
{
    public class PaymentRequest
    {
        public decimal Amount { get; set; }
        public string PaymentMethodNonce { get; set; }
        public bool SubmitForSettlement { get; set; }
    }
}