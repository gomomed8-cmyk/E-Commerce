using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using Stripe;
using Stripe.Tax;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Payments
{
    internal class StripePaymentGateway : IPaymentGateWay
    { 
        private readonly PaymentGatewaySettings _payment;
        private readonly PaymentIntentService _paymentIntentService;
        public StripePaymentGateway(IOptions<PaymentGatewaySettings> options)
        {
          _payment=options.Value ;
            var client = new StripeClient(_payment.SecretKey);
            _paymentIntentService = new PaymentIntentService(client);
        }

        public async Task<Result<PaymentIntentResult>> CreatePaymentIntentAsync(decimal amount, string currency, CancellationToken ct = default)
        {
            var options=new PaymentIntentCreateOptions()
            {
                Amount=(long)amount,
                Currency = currency,
                PaymentMethodTypes = ["card"]
            };
            var intent=await _paymentIntentService.CreateAsync(options,cancellationToken:ct);
            return new PaymentIntentResult(intent.Id,intent.ClientSecret);
        }

        public async Task<Result<PaymentIntentResult>> UpdatePaymentIntrntAsync(decimal amount, string paymentIntentId, CancellationToken ct = default)
        {
            var options = new PaymentIntentUpdateOptions()
            {
                Amount = (long)amount,
            };
            var intent= await _paymentIntentService.UpdateAsync(paymentIntentId,options,cancellationToken:ct) ;
            return new PaymentIntentResult(intent.Id, intent.ClientSecret);

        }
    }
}
