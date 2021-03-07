using System;
using System.Collections.Generic;
using System.Linq;

namespace RestaurantManager.Persons
{
    public class Chef : Person
    {
        public bool IsCooking { get; set; } = false;
        
        public Chef(string name) : base(name)
        {
        }

        public override void DoActivity(IEnumerable<Person> persons)
        {
            var waiters = persons.Where(w => w is Waiter);
        }
    }
}