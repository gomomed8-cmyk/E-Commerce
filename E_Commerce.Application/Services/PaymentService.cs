using AutoMapper;
using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Baskets;
using E_Commerce.Application.Specifications;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entites.Orders;
using E_Commerce.Domain.Entites.Products;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.Services
{
    internal class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentGateWay _paymentGateWay;
        private readonly IMapper _mapper;
        private readonly PaymentGatewaySettings _paymentGatewaySettings;

        public PaymentService(IBasketRepository basketRepository,
            IUnitOfWork unitOfWork,IPaymentGateWay paymentGateWay
            ,IOptions<PaymentGatewaySettings> options,IMapper mapper)
        {
            _basketRepository = basketRepository;
            _unitOfWork = unitOfWork;
            _paymentGateWay = paymentGateWay;
            _mapper = mapper;
            _paymentGatewaySettings = options.Value;
        }

        public async Task<Result<BasketDto>> CreateOrUpdatePaymentIntentAsync(string basketId, CancellationToken ct = default)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId, ct);
            if (basket == null)
                return Error.NotFound("Basket Is Not Found", $"Basket With Id {basketId} Is Not Found");
            if (basket.Items.Count == 0)
                return Error.Validation("Basket Is Empty", $"Can Not Create PaymentIntent With Empty {basketId}");

            if (!basket.DeliveryMethodId.HasValue)
                return Error.Validation("Delivery Method Id Is Required");

            var deliveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value, ct);
            if (deliveryMethod == null)
                return Error.NotFound("Delivery Method Not Found");

            basket.ShippingPrice = deliveryMethod.Cost;

            var productIds = basket.Items.Select(x => x.Id).ToHashSet();
            var products = (await _unitOfWork.GetRepository<Product, int>()
                .GetAllAsync(new ProductWithIdSpecification(productIds), ct)).ToDictionary(x => x.Id);
            foreach (var item in basket.Items)
            {
                if (!products.TryGetValue(item.Id, out var product))
                    return Error.NotFound("Product Not Found", $"Product With Id {item.Id} Is Not Found");

                item.Price = product.Price;
            }


            var supTotal = basket.Items.Sum(i => i.Price * i.Quantity);
            var amount = (long)((supTotal + deliveryMethod.Cost) * 100);


            if(string.IsNullOrEmpty(basket.PaymentIntentId))
            {
             var result= await _paymentGateWay.CreatePaymentIntentAsync(amount,_paymentGatewaySettings.DefaultCurrency, ct);
                basket.PaymentIntentId = result.data.PaymentIntentId;
                basket.ClientSecret = result.data.ClientSecret;
            }
            else
            {
                await _paymentGateWay.UpdatePaymentIntrntAsync(amount,basket.PaymentIntentId, ct);
            }

            await _basketRepository.CreateOrUpdateBasketAsync(basket,ct:ct);

            return  _mapper.Map<BasketDto>(basket);
        }
    }
}
