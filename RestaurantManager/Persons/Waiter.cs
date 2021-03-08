using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantManager.Orders;
using RestaurantManager.Utilities;

namespace RestaurantManager.Persons
{
    public class Waiter : Person
    {
        private bool IsBusy { get; set; } = false;
        
        public Waiter(string name) : base(name)
        {
        }

        public override void DoActivity(IEnumerable<Person> persons)
        {
            var guestsAndChefs = persons.Where(p => p is Chef || p is Guest);

            foreach (var person in guestsAndChefs)
            {
                // Take order from the guest
                if (person is Guest {IsOrdered: false} guest && IsBusy == false && guest.Order.OrderStatus == OrderStatus.Waiting)
                {
                    guest.Order.OrderStatus = OrderStatus.Accepted;
                    guest.IsOrdered = true;
                    Orders.Enqueue(guest.Order);

                    IsBusy = Orders.Count(o => o.OrderStatus == OrderStatus.Accepted) >= 3;

                    return;
                }

                // Handed over order to the chef
                if (person is Chef chef && chef.Orders.Count <= 2 && Orders.Count > 0 &&
                    Orders.Peek().OrderStatus == OrderStatus.Accepted)
                {
                    var waiterOrder = Orders.Dequeue();
                    waiterOrder.OrderStatus = OrderStatus.HandedOverToCooking;
                    chef.Orders.Enqueue(waiterOrder);
                    
                    IsBusy = Orders.Count(o => o.OrderStatus == OrderStatus.Accepted) >= 3;
                    return;
                }
                
                // Handed over the cooked order to the guest
                if (person is Guest {IsOrdered: true} guest1 && Orders.Count > 0 && 
                    Orders.Peek().OrderStatus == OrderStatus.HandedOverToWaiter && guest1.Id == Orders.Peek().GuestOrderId)
                {
                    var cookedOrder = Orders.Dequeue();
                    cookedOrder.OrderStatus = OrderStatus.Released;
                    guest1.Orders.Enqueue(cookedOrder);

                    IsBusy = Orders.Count(o => o.OrderStatus == OrderStatus.Accepted) >= 3;

                    return;
                }
            }
        }
    }
}