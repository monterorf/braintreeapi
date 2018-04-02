using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webAPITest.Models;

namespace webAPITest.Business
{
    public interface IPaymentProvider
    {
        ResponseService<T> MakePayment<T>(PaymentRequest request);
        ResponseService<T> GetToken<T>();

    }
}
