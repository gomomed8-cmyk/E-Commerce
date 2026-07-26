using E_Commerce.Application.DTOs.Authentication;
using E_Commerce.Domain.Entites.Orders;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Application.DTOs.Oeders
{
    public class OrderToReturnDto
    {
        public Guid Id { get; set; }
        public string BuyerEmail { get; set; } = default!;
        public DateTimeOffset OrderDate { get; set; }
        public AddressDto ShipToAddress { get; set; }
        public string DeliveryMethod { get; set; }= default!;
        public string Status { get; set; } = default!;
        public decimal SupTotal { get; set; }
        public decimal DeliveryMethodCost { get; set; }
        public decimal Total { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}
