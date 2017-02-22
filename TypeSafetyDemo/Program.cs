using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafetyDemo
{
    class Program
    {
        static string Status;
        static void Main(string[] args)
        {
            // Set things up
            Person person = new Farmer();
            person.Inventory.Add(new Apple());
            person.Inventory.Add(new Apple());
            person.Inventory.Add(new PoisonApple());
            person.Inventory.Add(new Watermelon());

            // Start the tasks
            var theTasks = new List<Task>();

            // Start ShowStatus(person) and others without waiting for completion,
            // then hold on to the task so we can wait later
            theTasks.Add(ShowStatus(person)); 
            theTasks.Add(HungerLoop(person));
            theTasks.Add(EatLoop(person));

            // Wait for the person to die (gruesome, I know)
            Task.WaitAll(theTasks.ToArray());            
        }

        static async Task ShowStatus(Person person)
        {
            // Show details
            while (person.IsAlive)
            {
                await Task.Delay(250);
                Console.Clear();
                Console.WriteLine("Status: " + Status);
                Console.WriteLine("Belly: " + person.Belly.ToString());
                Console.WriteLine("Inventory: " + person.Inventory.Count.ToString());
                Console.WriteLine("Leftovers: " + person.Leftovers.Count.ToString());
            }
            Console.WriteLine("Person is dead :(");
        }

        static async Task EatLoop(Person person)
        {
            while (person.IsAlive)
            {
                // Wait a little bit
                await Task.Delay(1000);

                // Choose a consumable item
                Status = "Choosing food...";
                var consumable = await person.ChooseConsumable();

                if (consumable == null)
                {
                    Status = "Out of food";
                }
                else
                {
                    // Eat
                    Status = "Eating " + consumable.GetType().Name + "...";
                    await person.EatAndStoreLeftovers(consumable);

                    // Done
                    Status = "Waiting.";
                }                
            }
        }

        static async Task HungerLoop(Person person)
        {
            while (person.IsAlive)
            {
                // Wait a little bit
                await Task.Delay(500);

                // Decrement belly
                person.IncrementBelly(-1);
            }            
        }
    }
}
