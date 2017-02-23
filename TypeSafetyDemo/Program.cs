using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TypeSafetyDemo
{
    class Program
    {
        static void PrintList(IEnumerable theList)
        {
            Console.WriteLine($"The list contains:");
            foreach (var item in theList)
            {
                Console.WriteLine(item.GetType().Name);
            }
        }
        
        static void Main(string[] args)
        {
            //// Method shadowing
            //var aVegan = new Vegan();
            //aVegan.Inventory.Add(new Apple());
            //aVegan.Inventory.Add(new Bacon()); // Compiler Error
            //(aVegan as Person).Inventory.Add(new Bacon());
            //PrintList((aVegan as Person).Inventory);
            //PrintList(aVegan.Inventory);

            // Type Casting vs Type Conversion
            var theSeed = new Seed<Apple>(10);
            
            ISeed aSeed = (ISeed)theSeed; // aSeed references theSeed, and will accept any ISeed
            ISeed anotherSeed = theSeed as ISeed; // anotherSeed references theSeed.  This is faster than the above line because this one is only for casting, not conversion
            PoisonAppleSeed poisonedSeed = (PoisonAppleSeed)theSeed; // Conversion (calls the explicit operator in PoisonAppleSeed.cs)
            Apple nullApple = aSeed as Apple; // nullApple is null because Seed<Apple> is not in Apple's inheritance chain
            Apple errorApple = (Apple)aSeed; // Will throw an invalid cast exception


            RunEatingSimulation();

            Console.ReadLine();
        }


        static string Status;
        static void RunEatingSimulation()
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
