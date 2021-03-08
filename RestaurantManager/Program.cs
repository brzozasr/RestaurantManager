using System;
using System.Collections.Generic;
using RestaurantManager.Orders;
using RestaurantManager.Persons;

namespace RestaurantManager
{
    class Program
    {
        static void Main(string[] args)
        {
            var restaurant = new Restaurant(GeneratePersons(30, 3, 2));

            

            while (true)
            {
                Console.Clear();
                restaurant.Display();
                
                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                if (keyInfo.Key != ConsoleKey.Escape)
                {
                    Console.Clear();
                    restaurant.Update();
                    restaurant.Display();
                }

                if (keyInfo.Key == ConsoleKey.Escape)
                {
                    break;
                }
            }
        }

        private static IEnumerable<Person> GeneratePersons(int guests, int waiters, int chefs)
        {
            List<Person> persons = new List<Person>();

            for (int i = 0; i < guests; i++)
            {
                var id = i.ToString().Length > 1 ? $"{i}" : $"{i} ";
                persons.Add(new Guest($"Guest{id}", new Order()));
            }

            for (int j = 0; j < waiters; j++)
            {
                var id = j.ToString().Length > 1 ? $"{j}" : $"{j} ";
                persons.Add(new Waiter($"Waiter{id}"));
            }

            for (int k = 0; k < chefs; k++)
            {
                var id = k.ToString().Length > 1 ? $"{k}" : $"{k} ";
                persons.Add(new Chef($"Chef{id}"));
            }

            return persons;
        }
    }
}