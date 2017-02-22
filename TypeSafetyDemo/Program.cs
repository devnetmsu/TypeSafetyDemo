using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TypeSafetyDemo
{
    class Program
    {
        static string Status;
        static void Main(string[] args)
        {
            Person person = new Farmer();
            (person as Farmer).EnableFarming = false;
            person.Inventory.Add(new Apple());
            person.Inventory.Add(new Apple());
            person.Inventory.Add(new Apple());
            person.Inventory.Add(new Watermelon());

            var hungerTask = HungerLoop(person);
            var eatTask = EatLoop(person);

            // Show details
            while (person.IsAlive)
            {
                Thread.Sleep(250);
                Console.Clear();
                Console.WriteLine("Status: " + Status);
                Console.WriteLine("Belly: " + person.Belly.ToString());
                Console.WriteLine("Leftovers: " + person.Leftovers.Count.ToString());
            }
            Console.WriteLine("Person is dead :(");
        }

        static async Task EatLoop(Person person)
        {
            while (person.IsAlive)
            {
                // Wait a little bit
                await Task.Run(() => Thread.Sleep(1000));

                // Choose a consumable item
                Status = "Choosing food...";
                var consumable = await person.ChooseConsumable();

                // Eat
                Status = "Eating " + consumable.GetType().Name + "...";
                await person.EatAndStoreLeftovers(consumable);

                // Done
                Status = "Waiting.";
            }
        }

        static async Task HungerLoop(Person person)
        {
            while (person.IsAlive)
            {
                // Wait a little bit
                await Task.Run(() => Thread.Sleep(500));

                // Decrement belly
                person.IncrementBelly(-1);
            }            
        }
    }
}
