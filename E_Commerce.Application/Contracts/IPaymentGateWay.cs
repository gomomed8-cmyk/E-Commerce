using E_Commerce.Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Contracts
{
    public interface IPaymentGateWay
    {
        Task<Result<PaymentIntentResult>> CreatePaymentIntentAsync(decimal amount,string currency,CancellationToken ct = default);
        Task<Result<PaymentIntentResult>> UpdatePaymentIntrntAsync(decimal amount,string paymentIntentId,CancellationToken ct = default);
    }
}
