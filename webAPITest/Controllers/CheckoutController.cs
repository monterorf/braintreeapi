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
        private readonly IBraintreeConfiguration config = new BraintreeConfiguration();


        public ResponseService<string> GetToken()
        {
            try
            {
               // throw new Exception();
                var gateway = config.GetGateway();
                var clientToken = gateway.ClientToken.generate();

                return new ResponseService<string>
                {
                    Data = clientToken,
                    Code = (int)EnumResponseStatus.OK
                };
            }
            catch (Exception ex)
            {
                ex.Process();                
                return new ResponseService<string>(true, EnumResponseStatus.Error.GetMessage())
                {
                    Code = (int)EnumResponseStatus.Error
                };
            }
        }

        [System.Web.Http.HttpPost]
        public ResponseService<Transaction> CreatePayment(Models.Request req)
        {
            var gateway = config.GetGateway();

            if (req == null)
            {
                return new ResponseService<Transaction>
                {
                    Error = true,
                    Code = (int)EnumResponseStatus.Error,
                    Message = "Data wasn't provided to process your request"
                };
            }

            if (string.IsNullOrWhiteSpace(req.PaymentMethodNonce))
            {
                return new ResponseService<Transaction>
                {
                    Error = true,
                    Code = (int)EnumResponseStatus.Error,
                    Message = "PaymentMethodNonce is required to process your request"
                };
            }

            if (req.Amount<0)
            {
                return new ResponseService<Transaction>
                {
                    Error = true,
                    Code = (int)EnumResponseStatus.Error,
                    Message = "Amount must be greater than or equal to zero to process your request"
                };
            }

            var request = new TransactionRequest
            {
                Amount = req.Amount,
                PaymentMethodNonce = req.PaymentMethodNonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };
            Result<Transaction> result = gateway.Transaction.Sale(request);
            if (result.IsSuccess())
            {
                Transaction transaction = result.Target;
                return new ResponseService<Transaction>
                {
                    Data = transaction,
                    Code = (int)EnumResponseStatus.OK,
                    Message = "Ok"
                };
            }
            if (result.Transaction != null)
            {
                return new ResponseService<Transaction>
                {
                    Error = true,                    
                    Code = (int)EnumResponseStatus.Error,
                    Message = "No se armó"
                };
            }
            return new ResponseService<Transaction>
            {
                Error = true,
                Code = (int)EnumResponseStatus.Error,
                Message = result.Message
            };
            
        }
        //public ResponseService<Transaction> CreatePayment(Models.Request req)
        //{
        //    var gateway = config.GetGateway();
        //    string error;
        //    Decimal amount;
        //    amount = 65;
        //    string payment_method_nonce;
        //    payment_method_nonce = "tokencc_bf_rvg8rk_rnrz8p_4p87t3_n9ppg5_4c4";



        //    try
        //    {
        //        amount = Convert.ToDecimal(amount);
        //    }
        //    catch (FormatException e)
        //    {
        //        error = "Error: 81503: Amount is an invalid format.";
        //       // return error;
        //    }

        //    var nonce = payment_method_nonce;
        //    var request = new TransactionRequest

        //    {
        //        Amount = req.amount,
        //        PaymentMethodNonce = req.PaymentMethodNonce,
        //        Options = new TransactionOptionsRequest
        //        {
        //            SubmitForSettlement = true
        //        }
        //    };

        //    Result<Transaction> result = gateway.Transaction.Sale(request);
        //    if (result.IsSuccess())
        //    {
        //        Transaction transaction = result.Target;
        //        return new ResponseService<Transaction>
        //        {
        //            Data = transaction,
        //            Error = false,
        //            Code = (int)EnumResponseStatus.OK,
        //            Message = "Ok"
        //        };
        //        //return transaction.Id;

        //        //return RedirectToAction("Show", new { id = transaction.Id });
        //    }
        //    else if (result.Transaction != null)
        //    {
        //        //return "No se armó";
        //        //return RedirectToAction("Show", new { id = result.Transaction.Id });
        //    }
        //    else
        //    {
        //        /*         string errorMessages = "";
        //                 foreach (ValidationError error in result.Errors.DeepAll())
        //                 {
        //                     errorMessages += "Error: " + (int)error.Code + " - " + error.Message + "\n";
        //                 }
        //                 TempData["Flash"] = errorMessages;*/
        //        //return RedirectToAction("New");
        //        //return "no sé";
        //    }
        //}
    }
}
