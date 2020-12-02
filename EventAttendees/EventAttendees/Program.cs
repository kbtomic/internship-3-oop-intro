using System;
using System.Collections.Generic;

namespace EventAttendees
{
    class Program
    {
        static void Main(string[] args)
        {
            var date = new DateTime(2020, 12, 01);
            var coffeeEvent = new Event("Retro", (EventType)1, date.AddHours(20), date.AddHours(22));
            var lectureEvent = new Event("Sveucilisna", (EventType)2, date.AddHours(8), date.AddHours(10));
            var coffeeAttendeesList = new List<Person>()
            {
                new Person("Rafael", "Nadal", 000, 099),
                new Person("Dominic", "Thiem", 001, 098),
                new Person("Serena", "Williams", 002, 097)

            };
            var lectureAttendeesList = new List<Person>()
            {
                new Person("Toni", "Milun", 003, 099),
                new Person("Ana", "Anic", 004, 099)
            };
            var EventDic = new Dictionary<Event, List<Person>>() {
                { coffeeEvent, coffeeAttendeesList },
                { lectureEvent, lectureAttendeesList }
            };

            MainMenu(EventDic);
        }
        static void MainMenu(Dictionary<Event, List<Person>> eventDic)
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


                bool choiceSuccess = int.TryParse(Console.ReadLine(), out choice);
                if (choiceSuccess)
                {
                    switch (choice)
                    {
                        case 1:
                            AddNewEvent(eventDic);
                            break;
                        case 2:
                            DeleteEvent(eventDic);
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
                            Console.WriteLine("Exit from menu!");
                            break;
                        default:
                            Console.WriteLine("Wrong number! Please try again!");
                            break;
                    }
                }
                else
                    Console.WriteLine("Please insert number!");
            } while (choice != 7);
        }
        static void AddNewEvent(Dictionary<Event, List<Person>> eventDic)
        {
            var loopStopper = false;
            var eventName = "";
            while (!loopStopper)
            {
                Console.WriteLine("Please insert name of the event: ");
                var eventNameLoop = Console.ReadLine();
                while (string.IsNullOrEmpty(eventNameLoop))
                {
                    Console.WriteLine("Please insert name of the event which is not empty!");
                    eventNameLoop = Console.ReadLine();
                }
                foreach (var Event in eventDic.Keys)
                {
                    if (eventNameLoop == Event.Name)
                    {
                        Console.WriteLine("This name already exists!");
                        break;
                    }
                    else
                    {
                        loopStopper = true;
                        eventName = eventNameLoop;
                    }
                }
            }

            Console.WriteLine("Please insert type of event: Enter '1' for coffee, enter '2' for lecture, enter '3' for concert, enter '4' for study session!");
            var typeOfEventSuccess = int.TryParse(Console.ReadLine(), out int choice);
            while (!typeOfEventSuccess || choice >= 5 || choice <= 0)
            {
                Console.WriteLine("Please insert valid number in range 1-4!");
                typeOfEventSuccess = int.TryParse(Console.ReadLine(), out choice);
            }

            var startTime = new DateTime();
            loopStopper = false;
            while (!loopStopper)
            {
                Console.WriteLine("Please insert start time of event. Use format yyyy/mm/dd hh:mm:ss!");
                var startTimeSuccess = DateTime.TryParse(Console.ReadLine(), out DateTime startTimeLoop);
                while (!startTimeSuccess)
                {
                    Console.WriteLine("Please insert valid start time of event. Use format yyyy/mm/dd hh:mm:ss!");
                    startTimeSuccess = DateTime.TryParse(Console.ReadLine(), out startTimeLoop);
                }
                var uniqueEvent = 0;
                foreach (var Event in eventDic.Keys)
                {
                    if (DateTime.Compare(Event.StartTime, startTimeLoop) < 0 && DateTime.Compare(Event.EndTime, startTimeLoop) > 0)
                    {
                        Console.WriteLine("There can't be more than one event in the same time! At wished moment is event" + Event.Name + " already scheduled");
                        uniqueEvent = 1;
                        break;
                    }
                }
                if (uniqueEvent == 0)
                {
                    loopStopper = true;
                }
                startTime = startTimeLoop;
            }


            var endTime = new DateTime();
            loopStopper = false;
            while (!loopStopper)
            {
                Console.WriteLine("Please insert end time of event. Use format yyyy/mm/dd hh:mm:ss!");
                var endTimeSuccess = DateTime.TryParse(Console.ReadLine(), out DateTime endTimeLoop);
                while (!endTimeSuccess)
                {
                    Console.WriteLine("Please insert valid end time of event. Use format yyyy/mm/dd hh:mm:ss!");
                    endTimeSuccess = DateTime.TryParse(Console.ReadLine(), out endTimeLoop);
                }
                if (DateTime.Compare(endTimeLoop, startTime) < 0)
                    Console.WriteLine("Event can't end before it even started!");
                else
                {
                    var uniqueEvent = 0;
                    foreach (var Event in eventDic.Keys)
                    {
                        if (DateTime.Compare(Event.StartTime, endTimeLoop) < 0 && DateTime.Compare(Event.EndTime, endTimeLoop) > 0)
                        {
                            Console.WriteLine("There can't be more than one event in the same time! At wished moment is event" + Event.Name + " already scheduled");
                            uniqueEvent = 1;
                            break;
                        }
                        else if (DateTime.Compare(Event.EndTime, startTime) > 0 && DateTime.Compare(Event.StartTime, endTimeLoop) < 0)
                        {
                            Console.WriteLine("There can't be more than one event in the same time! You should end earlier because event " + Event.Name + " is going to start");
                            uniqueEvent = 1;
                            break;
                        }
                    }
                    if (uniqueEvent == 0)
                    {
                        loopStopper = true;
                    }
                }
                endTime = endTimeLoop;
            }
            Console.WriteLine("Are you sure that you want this event - name: " + eventName + " type of event: " + (EventType)choice + "start time: " + startTime + " end time: " + endTime);
            Console.WriteLine("Type yes if you are sure!");
            var confirmation = Console.ReadLine();
            if (confirmation.ToLower() == "yes")
            {
                var newEvent = new Event(eventName, (EventType)choice, startTime, endTime);
                eventDic.Add(newEvent, new List<Person>());
            }
            else
                Console.WriteLine("Event not added!");

        }
        static void DeleteEvent(Dictionary<Event, List<Person>> eventDic)
        {
            Console.WriteLine("List of events: ");
            foreach (var Event in eventDic.Keys)
                Console.WriteLine(Event.Name);

            var loopStopper = false;
            while (!loopStopper)
            {
                Console.WriteLine("Please enter name of the event that you want to delete: ");
                var nameOfDeletedEvent = Console.ReadLine();
                foreach (var Event in eventDic.Keys)
                {
                    if (nameOfDeletedEvent == Event.Name)
                    {
                        Console.WriteLine("Are you sure that you want to delete event: " + nameOfDeletedEvent);
                        Console.WriteLine("Type yes if you are sure!");
                        var confirmation = Console.ReadLine();
                        if (confirmation.ToLower() == "yes")
                        {
                            eventDic.Remove(Event);
                            Console.WriteLine("Deleted succcessfully");
                            loopStopper = true;
                            break;
                        }
                        else
                            Console.WriteLine("Event: " + nameOfDeletedEvent + " not deleted!");
                    }
                    else
                    {
                        Console.WriteLine("That name of the event is not on the list!");
                        break;
                    }
                }
            }
        }
    }
}
