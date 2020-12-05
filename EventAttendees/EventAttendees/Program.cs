using System;
using System.Collections.Generic;
using System.Linq;

namespace EventAttendees
{
    class Program
    {
        static void Main(string[] args)
        {
            var date = new DateTime(2020, 12, 01);
            var coffeeEvent = new Event("Retro", EventType.Coffee, date.AddHours(20), date.AddHours(22));
            var lectureEvent = new Event("Sveucilisna", EventType.Lecture, date.AddHours(8), date.AddHours(10));
            var allAttendees = new List<Person>()
            {
                new Person("Rafael", "Nadal", 123, "099456789"),
                new Person("Dominic", "Thiem", 1234, "098923456"),
                new Person("Serena", "Williams", 12345, "097123456"),
                new Person("Toni", "Milun", 123456, "099123451"),
                new Person("Ana", "Anic", 1234567, "0999876543")
            };
            var coffeeAttendeesList = new List<Person>();
            coffeeAttendeesList.Add(allAttendees[0]);
            coffeeAttendeesList.Add(allAttendees[1]);
            coffeeAttendeesList.Add(allAttendees[2]);

            var lectureAttendeesList = new List<Person>();
            lectureAttendeesList.Add(allAttendees[3]);
            lectureAttendeesList.Add(allAttendees[4]);

            var eventDic = new Dictionary<Event, List<Person>>() {
                { coffeeEvent, coffeeAttendeesList },
                { lectureEvent, lectureAttendeesList }
            };

            MainMenu(eventDic, allAttendees);
        }
        static void MainMenu(Dictionary<Event, List<Person>> eventDic, List<Person> allAttendees)
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


                var choiceSuccess = int.TryParse(Console.ReadLine(), out choice);
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
                            EditEvent(eventDic);
                            break;
                        case 4:
                            AddPersonOnEvent(eventDic,allAttendees);
                            break;
                        case 5:
                            RemoveEventAttendee(eventDic, allAttendees);
                            break;
                        case 6:
                            AllEventDetails(eventDic);
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
            var newEvent = new Event();
            if (newEvent.EventInput(eventDic) != null)
            {
                if (Confirmation())
                {
                    Console.WriteLine("Event added!");
                    eventDic.Add(newEvent, new List<Person>());
                }
                else
                    Console.WriteLine("Event not added!");
            }
            else
                Console.WriteLine("Event not added!");
        }
        static void DeleteEvent(Dictionary<Event, List<Person>> eventDic)
        {
            if (!IsDicEmpty(eventDic))
            {
                PrintEvents(eventDic);
                Console.WriteLine("Please enter name of the event that you want to delete: ");
                var nameOfDeletedEvent = Console.ReadLine();
                var deletedEvent = SelectedEvent(eventDic, nameOfDeletedEvent);
                if (deletedEvent != null)
                {
                    if (Confirmation())
                    {
                        eventDic.Remove(deletedEvent);
                        Console.WriteLine(deletedEvent.Name + " deleted successfully!");
                    }
                    else
                        Console.WriteLine(deletedEvent.Name + " not deleted successfully!");
                }
            }
        }
        static void EditEvent(Dictionary<Event, List<Person>> eventDic)
        {
            if (!IsDicEmpty(eventDic))
            {
                Console.WriteLine("Enter 1(edit event name), enter 2(edit event type), enter 3(edit event start time), enter 4(edit event end time)");
                var whatToEditSuccess = int.TryParse(Console.ReadLine(), out var whatToEdit);
                while (!whatToEditSuccess || whatToEdit < 1 || whatToEdit > 4)
                {
                    Console.WriteLine("Enter 1(edit event name), enter 2(edit event type), enter 3(edit event start time), enter 4(edit event end time)");
                    whatToEditSuccess = int.TryParse(Console.ReadLine(), out whatToEdit);
                }

                PrintEvents(eventDic);
                Console.WriteLine("Please enter name of the event that you want to edit: ");
                var nameOfEditedEvent = Console.ReadLine();
                var editedEvent = SelectedEvent(eventDic, nameOfEditedEvent);
                if (editedEvent != null)
                {
                    switch (whatToEdit)
                    {
                        case 1:
                            Console.WriteLine("Please enter new name: ");
                            var newName = Console.ReadLine();
                            while (editedEvent.DoesHaveSameName(eventDic))
                            {
                                Console.WriteLine("Please enter new name: ");
                                newName = Console.ReadLine();
                            }
                            if (Confirmation())
                            {
                                editedEvent.Name = newName;
                                Console.WriteLine("New name is: " + newName);
                            }
                            else
                                Console.WriteLine("Name not changed!");
                            break;
                        case 2:
                            Console.WriteLine("Please enter new event type: 0(coffee), 1(lecture), 2(concert), 3(study session)");
                            var choiceSuccess = int.TryParse(Console.ReadLine(), out var choice);
                            while (!choiceSuccess)
                            {
                                choiceSuccess = int.TryParse(Console.ReadLine(), out choice);
                            }
                            if (editedEvent.TypeOfEvent == (EventType)choice)
                                Console.WriteLine("You didn't change type!");
                            else
                            {
                                if (Confirmation())
                                {
                                    editedEvent.TypeOfEvent = (EventType)choice;
                                    Console.WriteLine("New event type is: " + (EventType)choice);
                                }
                                else
                                    Console.WriteLine("Event type not changed!");
                            }
                            break;
                        case 3:
                            Console.WriteLine("Please insert new start time of event. Use format yyyy/mm/dd hh:mm:ss!");
                            var startTimeSuccess = DateTime.TryParse(Console.ReadLine(), out DateTime startTime);
                            while (!startTimeSuccess)
                            {
                                Console.WriteLine("Please insert valid start time of event. Use format yyyy/mm/dd hh:mm:ss!");
                                startTimeSuccess = DateTime.TryParse(Console.ReadLine(), out startTime);
                            }
                            var overlapping = 0;
                            foreach (var eventloop in eventDic.Keys)
                            {
                                if (eventloop != editedEvent && eventloop.DoesOverlapEvent(startTime))
                                {
                                    overlapping = 1;
                                    break;
                                }
                            }
                            if (overlapping == 0)
                            {
                                if (Confirmation())
                                {
                                    editedEvent.StartTime = startTime;
                                    Console.WriteLine("Start time changed!");
                                }
                                else
                                    Console.WriteLine("Start time not changed!");
                            }
                            else
                            {
                                Console.WriteLine("Start time not changed!");
                            }
                            break;
                        case 4:
                            Console.WriteLine("Please insert new end time of event. Use format yyyy/mm/dd hh:mm:ss!");
                            var endTimeSuccess = DateTime.TryParse(Console.ReadLine(), out DateTime endTime);
                            while (!endTimeSuccess)
                            {
                                Console.WriteLine("Please insert valid end time of event. Use format yyyy/mm/dd hh:mm:ss!");
                                endTimeSuccess = DateTime.TryParse(Console.ReadLine(), out startTime);
                            }
                            overlapping = 0;
                            foreach (var eventloop in eventDic.Keys)
                            {
                                if (eventloop != editedEvent && eventloop.DoesOverlapEvent(endTime))
                                {
                                    overlapping = 1;
                                    break;
                                }
                            }
                            if (overlapping == 0 && endTime > editedEvent.StartTime)
                            {
                                if (Confirmation())
                                {
                                    editedEvent.EndTime = endTime;
                                    Console.WriteLine("End time changed!");
                                }
                                else
                                    Console.WriteLine("End time not changed!");
                            }
                            else
                            {
                                Console.WriteLine("End time not changed!");
                            }
                            break;
                    }
                }

            }
        }
                
  
        static void AddPersonOnEvent(Dictionary<Event, List<Person>> eventDic, List<Person> allAttendees)
        {
            if (!IsDicEmpty(eventDic))
            {
                PrintEvents(eventDic);
                Console.WriteLine("Enter name of the event on which you want to add person: ");
                var eventName = Console.ReadLine();
                while (string.IsNullOrEmpty(eventName))
                {
                    Console.WriteLine("Enter name of the event on which you want to add person: ");
                    eventName = Console.ReadLine();
                }
                var selectedEvent = SelectedEvent(eventDic, eventName);
                if (selectedEvent != null)
                {
                    Console.WriteLine("Enter 1 to add new person that is going on event, enter 2 to add one of the existing people");
                    var choiceSuccess = int.TryParse(Console.ReadLine(), out int choice);
                    while (!choiceSuccess || choice > 2 || choice < 1)
                    {
                        Console.WriteLine("Enter 1 to add new person that is going on event, enter 2 to add one of the existing people");
                        choiceSuccess = int.TryParse(Console.ReadLine(), out choice);
                    }
                    if (choice == 1)
                        AddNewPersonToBeAttendee(eventDic, allAttendees, selectedEvent);
                    else
                        AddExistingPersonToBeAttendee(eventDic, allAttendees, selectedEvent);
                }
            }

        }
        static void RemoveEventAttendee(Dictionary<Event, List<Person>> eventDic, List<Person> allAttendees)
        {
            if (!IsDicEmpty(eventDic))
            {
                PrintEvents(eventDic);
                Console.WriteLine("Enter name of the event from which you want to remove attendee: ");
                var eventName = Console.ReadLine();
                while (string.IsNullOrEmpty(eventName))
                {
                    Console.WriteLine("Enter name of the event from which you want to remove attendee: ");
                    eventName = Console.ReadLine();
                }
                var selectedEvent = SelectedEvent(eventDic, eventName);
                if (selectedEvent != null)
                {
                    PrintEventAttendeesWithOIB(eventDic, selectedEvent);
                    Console.WriteLine("Enter OIB of the person you want to remove: ");
                    var OIBOfNewAttendeeSuccess = int.TryParse(Console.ReadLine(), out int OIBOfNewAttendee);
                    while (!OIBOfNewAttendeeSuccess)
                    {
                        Console.WriteLine("Enter OIB of the person you want to become attendee: ");
                        OIBOfNewAttendeeSuccess = int.TryParse(Console.ReadLine(), out OIBOfNewAttendee);
                    }
                    Person removingAttendee = null;
                    foreach (var person in allAttendees)
                    {
                        if (OIBOfNewAttendee == person.OIB)
                            removingAttendee = person;
                    }
                    if (removingAttendee != null)
                    {
                        if (Confirmation())
                        {
                            eventDic[selectedEvent].Remove(removingAttendee);
                            Console.Write("Removing: ");
                            removingAttendee.PersonDetails();
                        }
                        else
                            Console.WriteLine("Person not removed!");
                    }
                    else
                        Console.WriteLine("Person with requested OIB does not exist!");
                }
            }
        }
        static void AllEventDetails(Dictionary<Event, List<Person>> eventDic)
        {
            if (!IsDicEmpty(eventDic))
            {
                PrintEvents(eventDic);
                Console.WriteLine("Enter name of the event: ");
                var eventName = Console.ReadLine();
                while (string.IsNullOrEmpty(eventName))
                {
                    Console.WriteLine("Enter name of the event: ");
                    eventName = Console.ReadLine();
                }
                var selectedEvent = SelectedEvent(eventDic, eventName);
                if (selectedEvent != null)
                {
                    var choice = 0;
                    do
                    {
                        Console.WriteLine("Enter 1 for event details, enter 2 for event attendees, enter 3 for combination, enter 4 for exit this menu");
                        var choiceSuccess = int.TryParse(Console.ReadLine(), out choice);
                        if (choiceSuccess)
                        {
                            switch (choice)
                            {
                                case 1:
                                    selectedEvent.PrintEventDetails(eventDic);
                                    break;
                                case 2:
                                    selectedEvent.PrintEventAttendees(eventDic);
                                    break;
                                case 3:
                                    selectedEvent.PrintEventDetails(eventDic);
                                    selectedEvent.PrintEventAttendees(eventDic);
                                    break;
                                case 4:
                                    Console.WriteLine("Switching to main menu!");
                                    break;
                                default:
                                    Console.WriteLine("Wrong number! Please try again!");
                                    break;
                            }
                        }
                        else
                            Console.WriteLine("Please insert number!");
                    } while (choice != 4);
                }
            }

        }
        static void AddNewPersonToBeAttendee(Dictionary<Event, List<Person>> eventDic, List<Person> allAttendees, Event eventGoingTo)
        {
            var newPerson = new Person();
            if (Confirmation())
            {
                eventDic[eventGoingTo].Add(newPerson);
                allAttendees.Add(newPerson);
                Console.Write("Adding to event: ");
                newPerson.PersonDetails();
            }
            else
                Console.WriteLine("Person not added!");
        }
        static void AddExistingPersonToBeAttendee(Dictionary<Event, List<Person>> eventDic, List<Person> allAttendees, Event eventGoingTo)
        {
            PrintExistingAttendees(allAttendees);
            Console.WriteLine("Enter OIB of the person you want to become attendee: ");
            var OIBOfNewAttendeeSuccess = int.TryParse(Console.ReadLine(), out int OIBOfNewAttendee);
            while(!OIBOfNewAttendeeSuccess)
            {
                Console.WriteLine("Enter OIB of the person you want to become attendee: ");
                OIBOfNewAttendeeSuccess = int.TryParse(Console.ReadLine(), out OIBOfNewAttendee);
            }
            Person existingPerson = null;
            foreach(var person in allAttendees)
            {
                if (OIBOfNewAttendee == person.OIB)
                    existingPerson = person;
            }
            if (existingPerson != null)
            {
                if (existingPerson.IsPersonAlreadyGoingToEvent(eventDic, eventGoingTo))
                {
                    existingPerson.PersonDetails();
                    Console.WriteLine("is already going to event " + eventGoingTo.Name);
                }
                else
                {
                    if (Confirmation())
                    {
                        eventDic[eventGoingTo].Add(existingPerson);
                        Console.WriteLine("Adding to event: ");
                        existingPerson.PersonDetails();
                    }
                    else
                        Console.WriteLine("Person not added!");
                }
            }
            else
                Console.WriteLine("Person with requested OIB does not exist!");
        }
        static void PrintEvents(Dictionary<Event, List<Person>> eventDic)
        {
            Console.WriteLine("List of events: ");
            foreach (var Event in eventDic.Keys)
                Console.WriteLine(Event.Name);
        } 

        static Event SelectedEvent(Dictionary<Event, List<Person>> eventDic, string name)
        {
            foreach (var Event in eventDic.Keys)
            {
                if (Event.Name == name)
                    return Event;
            }
            Console.WriteLine("That event does not exist!");
            return null;
        }
        static void PrintEventAttendeesWithOIB(Dictionary<Event, List<Person>> eventDic, Event eventName)
        {
            Console.WriteLine("List of event attendees for event " + eventName.Name);
            foreach (var person in eventDic[eventName])
                person.PersonDetails();
        }
        static void PrintExistingAttendees(List<Person> allAttendees)
        {
            Console.WriteLine("List of existing attendees: ");
            foreach (var person in allAttendees)
                person.PersonDetails();
        }
        static bool Confirmation()
        {
            Console.WriteLine("Please type yes to confirm all changes!");
            var confirmation = Console.ReadLine();
            if (confirmation.ToLower() == "yes")
                return true;
            else
                return false;
        }
        static bool IsDicEmpty(Dictionary<Event, List<Person>> eventDic)
        {
            if(eventDic.Count == 0)
            {
                Console.WriteLine("There is no scheduled events!");
                return true;
            }
            return false;
        }
    }
}
