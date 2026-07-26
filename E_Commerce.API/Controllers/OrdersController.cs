using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTOs.Oeders;
using E_Commerce.Domain.Entites.Orders;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace E_Commerce.API.Controllers
{
    public class OrdersController : ApiBaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto, CancellationToken ct)
        {
            return ToActionResult(await _orderService.CreateAsync(orderDto, GetEmailFormToken(), ct));
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetAllOrdersAsync(CancellationToken ct)
        {
            return ToActionResult(await _orderService.GetAllOrdersForUserAsync(GetEmailFormToken(), ct));
        }
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdAsync(Guid id,CancellationToken ct)
        {
           return ToActionResult(await _orderService.GetOrderByIdAndEmailForUserAsync(id, GetEmailFormToken(), ct));
        }
        [AllowAnonymous]
        [HttpGet("DeliveryMethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethodDto>>> GetAllDeliveryMethod(CancellationToken ct)
        {
           return ToActionResult(await _orderService.GetDeliveryMethodAsync(ct));
        }
    }
}
