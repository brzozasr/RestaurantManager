using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantManager.Utilities;

namespace RestaurantManager.Persons
{
    public class Chef : Person
    {
        public Chef(string name) : base(name)
        {
        }

        public override void DoActivity(IEnumerable<Person> persons)
        {
            var waiters = persons.Where(w => w is Waiter);

            // Handed over the cooked order to the waiter
            foreach (var person in waiters)
            {
                if (person is Waiter waiter && Orders.Count > 0 && HasCookedOrder())
                {
                    var cookedOrder = Orders.Dequeue();
                    cookedOrder.OrderStatus = OrderStatus.HandedOverToWaiter;
                    waiter.Orders.Enqueue(cookedOrder);
                    return;
                }
            }

            // Cooking process
            foreach (var order in Orders)
            {
                if (order.TimeCooking > 0)
                {
                    order.TimeCooking -= 1;
                }
                else
                {
                    order.OrderStatus = OrderStatus.Cooked;
                }
            }
        }

        private bool HasCookedOrder()
        {
            var isCooed = Orders.Peek().OrderStatus == OrderStatus.Cooked;
            return isCooed;
        }
    }
}