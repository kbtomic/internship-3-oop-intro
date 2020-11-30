using System;
using System.Collections.Generic;

namespace EventAttendees
{
    class Program
    {
        static void Main(string[] args)
        {
           var attendeesList = new List<Person>()
            {
                new Person("Rafael", "Nadal", 000, 099),
                new Person("Dominic", "Thiem", 001, 098),
                new Person("Serena", "Williams", 002, 097)

            };
            var myDictionary = new Dictionary<Event, List<Person>>();

            MainMenu(myDictionary);
        }
        static void MainMenu(Dictionary<Event, List<Person>> myDictionary)
        {
            var choice = 0;
            do
            {
                Console.WriteLine("Pick action: ");
                Console.WriteLine("1 - add new event");
                Console.WriteLine("2 - delete event");
                Console.WriteLine("3 - edit event");
                Console.WriteLine("4 - add event attendee");
                Console.WriteLine("5 - remove event attendee");
                Console.WriteLine("6 - print event details");
                Console.WriteLine("7 - exit menu");
               

                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break; 
                    case 7:
                        break;
                    default:
                        Console.WriteLine("Nepostojeci unos! Pokusaj ponovno!");
                        break;

                }
            } while (choice != 0);
        }
    }   
 }
