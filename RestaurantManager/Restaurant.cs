using System;
using System.Collections.Generic;
using System.Text;
using RestaurantManager.Persons;

namespace RestaurantManager
{
    public class Restaurant
    {
        public List<Person> Persons { get; } = new List<Person>();

        public Restaurant(IEnumerable<Person> persons)
        {
            Persons.AddRange(persons);
        }
        
        public void Update()
        {
            foreach (var person in Persons)
            {
                person.DoActivity(Persons);
            }
        }

        public void Display()
        {
            StringBuilder sb = new StringBuilder();

            int i = 1;
            foreach (var person in Persons)
            {
                if (person is Guest guest)
                {
                    sb.AppendLine($"Name: {guest.Name}, ID: {guest.Id}, order id: {guest.Order.OrderId} ordered: {guest.IsOrdered}, switch order: {guest.Order.IsSwitched}, order status: {guest.Order.OrderStatus.ToString()} ");
                }

                i++;
            }
            
            Console.Write(sb.ToString());
        }
    }
}