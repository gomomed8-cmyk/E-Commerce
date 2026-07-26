using E_Commerce.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Entites.Orders
{
    public class Order : BaseEntity<Guid>
    {
        private Order()
        {

        }

        public Order(string buyerEmail, OrderAddress shipToAddress, ICollection<OrderItem> items, decimal supTotal, DeliveryMethod deliveryMethod)
        {
            BuyerEmail = buyerEmail;
            ShipToAddress = shipToAddress;
            Items = items;
            SupTotal = supTotal;
            DeliveryMethod = deliveryMethod;
        }

        public string BuyerEmail { get; set; } = default!;
        public OrderAddress ShipToAddress { get; set; }=default!;
        public ICollection<OrderItem> Items { get; set; } = [];
        public decimal SupTotal { get; set; }
        public DeliveryMethod DeliveryMethod { get; set; }= default!;
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public int DeliveryMethodId { get; set; }  //FK
        public decimal GetTotal()=>SupTotal+(DeliveryMethod?.Cost ?? 0); 



    }
}
