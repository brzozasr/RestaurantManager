using System;
using System.Collections.Generic;
using System.Linq;
using RestaurantManager.Utilities;

namespace RestaurantManager.Persons
{
    public class Waiter : Person
    {
        public bool IsBusy => Orders.Count(o => o.OrderStatus == OrderStatus.Accepted) >= 3;
        
        public Waiter(string name) : base(name)
        {
        }

        public override void DoActivity(IEnumerable<Person> persons)
        {
            var guestsAndChefs = persons.Where(p => p is Chef || p is Guest);
        }
    }
}