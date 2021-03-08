using System;
using RestaurantManager.Utilities;

namespace RestaurantManager.Orders
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid GuestOrderId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public bool IsSwitched { get; set; } = false;
        public int TimeCooking { get; set; }

        public Order()
        {
            OrderId = Guid.NewGuid();
            OrderStatus = OrderStatus.Waiting;
            TimeCooking = 3;
        }
    }
}