using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Baskets;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce.API.Controllers
{

    public class PaymentsController : ApiBaseController
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [Authorize]
        [HttpPost("{basketId}")]
        public async Task<ActionResult<BasketDto>> CreateOrUpdatePaymentIntent(string basketId, CancellationToken ct)
        {
            var result =await _paymentService.CreateOrUpdatePaymentIntentAsync(basketId, ct);
            return ToActionResult(result);
        }
    }
}
