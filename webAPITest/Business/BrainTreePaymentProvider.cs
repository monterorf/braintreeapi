using Braintree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using webAPITest.Models;

namespace webAPITest.Business
{
    public class BrainTreePaymentProvider : IPaymentProvider
    {

        public ResponseService<T> GetToken<T>()
        {
            try
            {
                // throw new Exception();
                var gateway = new BraintreeConfiguration().GetGateway();
                var clientToken = gateway.ClientToken.generate();

                return new ResponseService<string>
                {
                    Data = clientToken,
                    Code = (int)EnumResponseStatus.OK
                } as ResponseService<T>;
            }
            catch (Exception ex)
            {
                ex.Process();
                return new ResponseService<string>(true, EnumResponseStatus.Error.GetMessage())
                {
                    Code = (int)EnumResponseStatus.Error
                } as ResponseService<T>;
            }
        }

        public ResponseService<T> MakePayment<T>(PaymentRequest req)
        {
            var gateway = new BraintreeConfiguration().GetGateway();

            if (req == null)
            {
                return new ResponseService<Transaction>
                {
                    Error = true,
                    Code = (int)EnumResponseStatus.Error,
                    Message = "Data wasn't provided to process your request"
                } as ResponseService<T>;
            }

            if (string.IsNullOrWhiteSpace(req.PaymentMethodNonce))
            {
                return new ResponseService<Transaction>
                {
                    Error = true,
                    Code = (int)EnumResponseStatus.Error,
                    Message = "PaymentMethodNonce is required to process your request"
                } as ResponseService<T>;
            }

            if (req.Amount < 0)
            {
                return new ResponseService<Transaction>
                {
                    Error = true,
                    Code = (int)EnumResponseStatus.Error,
                    Message = "Amount must be greater than or equal to zero to process your request"
                } as ResponseService<T>;
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
                } as ResponseService<T>;
            }
            if (result.Transaction != null)
            {
                return new ResponseService<Transaction>
                {
                    Error = true,
                    Code = (int)EnumResponseStatus.Error,
                    Message = "Something went wrong with your request"
                } as ResponseService<T>;
            }
            return new ResponseService<Transaction>
            {
                Error = true,
                Code = (int)EnumResponseStatus.Error,
                Message = result.Message
            } as ResponseService<T>;
        }
    }
}