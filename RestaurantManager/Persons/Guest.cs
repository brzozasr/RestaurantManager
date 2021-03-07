using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantManager.Orders;
using RestaurantManager.Utilities;

namespace RestaurantManager.Persons
{
    public class Guest : Person
    {
        public bool IsOrdered { get; private set; } = false;
        private Order _order;

        public Guest(string name, Order order) : base(name)
        {
            _order = order;
            _order.GuestOrderId = this.Id;
            _order.OrderStatus = OrderStatus.Accepted;
        }

        public override void DoActivity(IEnumerable<Person> persons)
        {
            var waitersAndGuests = persons.Where(p => p is Waiter || p is Guest).ToList();

            foreach (var waitersAndGuest in waitersAndGuests)
            {
                if (waitersAndGuest is Waiter {IsBusy: false} waiter && IsOrdered == false)
                {
                    waiter.Orders.Enqueue(_order);

                    IsOrdered = true;
                    return;
                }

                if (waitersAndGuest is Guest {IsOrdered: true} guest
                    && guest.Id != Id && guest.Orders.Count == 1 && Orders.Count == 1)
                {
                    var otherGuestOrder = guest.Orders.Peek();
                    var myOrder = Orders.Peek();

                    if (otherGuestOrder.IsSwitched == false && otherGuestOrder.OrderStatus == OrderStatus.Released && 
                         myOrder.IsSwitched == false && myOrder.OrderStatus == OrderStatus.Released)
                    {
                        otherGuestOrder = guest.Orders.Dequeue();
                        otherGuestOrder.IsSwitched = true;
                        otherGuestOrder.GuestOrderId = Id;

                        myOrder = Orders.Dequeue();
                        myOrder.IsSwitched = true;
                        myOrder.GuestOrderId = guest.Id;

                        guest.Orders.Enqueue(myOrder);
                        Orders.Enqueue(otherGuestOrder);

                        return;
                    }
                }
            }
        }
    }
}