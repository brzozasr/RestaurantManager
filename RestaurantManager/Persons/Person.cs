using System;
using System.Collections.Generic;
using RestaurantManager.Orders;

namespace RestaurantManager.Persons
{
    public abstract class Person
    {
        public Queue<Order> Orders { get; set; }
        public string Name { get; }
        public Guid Id { get; }

        public Person(string name)
        {
            Name = name;
            Id = Guid.NewGuid();
        }

        public abstract void DoActivity(IEnumerable<Person> persons);
    }
}