using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantManager.Orders;
using RestaurantManager.Utilities;

namespace RestaurantManager.Persons
{
    public class Guest : Person
    {
        public bool IsOrdered { get; set; }
        public Order Order { get; }

        public Guest(string name, Order order) : base(name)
        {
            Order = order;
            Order.GuestOrderId = this.Id;
        }

        public override void DoActivity(IEnumerable<Person> persons)
        {
            var guests = persons.Where(p => p is Guest).ToList();
            int intRandom = Utils.Random.Next(1, 201);

            foreach (var person in guests)
            {
                // Switch orders between guests
                if (person is Guest {IsOrdered: true} guest && intRandom == 1
                    && guest.Id != Id && guest.Orders.Count > 0 && Orders.Count > 0)
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