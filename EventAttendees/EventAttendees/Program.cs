﻿using System;
using System.Collections.Generic;
using System.Linq;

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
            var allAttendees = lectureAttendeesList.Concat(coffeeAttendeesList);
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
                            AddPersonOnEvent(eventDic);
                            break;
                        case 5:
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
            var typeOfEventSuccess = int.TryParse(Console.ReadLine(), out var choice);
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
                }
            }
        }
        static void EditEvent(Dictionary<Event, List<Person>> eventDic)
        {
            var loopStopper = false;
            var whatToEdit = 0;
            while (!loopStopper)
            {
                Console.WriteLine("Enter 1(edit event name), enter 2(edit event type), enter 3(edit event start time), enter 4(edit event end time)");
                var choiceSuccess = int.TryParse(Console.ReadLine(), out var choice);
                if (choiceSuccess && choice >= 1 && choice <= 4)
                {
                    whatToEdit = choice;
                    loopStopper = true;
                }
                else
                    Console.WriteLine("Please enter valid number!");

            }
            PrintEvents(eventDic);

            loopStopper = false;
            while (!loopStopper)
            {
                Console.WriteLine("Please enter name of the event that you want to edit: ");
                var nameOfEditedEvent = Console.ReadLine();
                foreach (var Event in eventDic.Keys)
                {
                    if (nameOfEditedEvent == Event.Name)
                    {
                        switch (whatToEdit)
                        {
                            case 1:
                                Console.WriteLine("Please enter new name: ");
                                var newName = Console.ReadLine();
                                while (Event.DoesHaveSameName(newName))
                                    newName = Console.ReadLine();
                                Event.Name = newName;
                                Console.WriteLine("New name is: " + newName);
                                loopStopper = true;
                                break;
                            case 2:
                                Console.WriteLine("Please enter new event type: 1(coffee), 2(lecture), 3(concert), 4(study session)");
                                var choiceSuccess = int.TryParse(Console.ReadLine(), out var choice);
                                while (!choiceSuccess)
                                {
                                    choiceSuccess = int.TryParse(Console.ReadLine(), out choice);
                                }
                                if (Event.TypeOfEvent == (EventType)choice)
                                    Console.WriteLine("You didn't change type!");
                                else
                                {
                                    Event.TypeOfEvent = (EventType)choice;
                                    Console.WriteLine("New event type is: " + (EventType)choice); //pojma neman zasto ode ispise za jedan broj veci choice
                   
                                }
                                loopStopper = true;
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
                                foreach (var ev in eventDic.Keys)
                                {
                                    if (ev != Event && ev.DoesOverlapEvent(startTime))
                                    {
                                        overlapping = 1;
                                        break;
                                    }
                                }
                                if(overlapping == 0)
                                {
                                    Event.StartTime = startTime;
                                    Console.WriteLine("Start time changed!");
                                }
                                else
                                {
                                    Console.WriteLine("Start time did not change!");
                                }
                                    loopStopper = true;
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
                                foreach (var ev in eventDic.Keys)
                                {
                                    if (ev != Event && ev.DoesOverlapEvent(endTime))
                                    {
                                        overlapping = 1;
                                        break;
                                    }
                                }
                                if (overlapping == 0 && endTime > Event.StartTime)
                                {
                                    Event.EndTime = endTime;
                                    Console.WriteLine("End time changed!");
                                }
                                else
                                {
                                    Console.WriteLine("End time did not change!");
                                }
                                loopStopper = true;
                                break;
                        }
                        break;
                    }
                }
            }
        }
        static void AddPersonOnEvent(Dictionary<Event, List<Person>> eventDic)
        {
            PrintEvents(eventDic);
            Console.WriteLine("Enter name of the event on which you want to add person: ");
            var eventName = Console.ReadLine();
            while (string.IsNullOrEmpty(eventName) || !DoesEventExist(eventDic, eventName))
            {
                Console.WriteLine("Enter name of the event on which you want to add person: ");
                eventName = Console.ReadLine();
            }
            var selectedEvent = SelectedEvent(eventDic, eventName);
            if (selectedEvent != null)
            {
                var newPerson = new Person();
                if (newPerson.IsPersonAlreadyGoingToEvent(eventDic, selectedEvent))
                    Console.WriteLine("Sorry, " + newPerson.FirstName + newPerson.LastName + " is already going to event " + selectedEvent.Name);
                else
                {
                    foreach (var Event in eventDic.Keys)
                    {
                        if (selectedEvent == Event)
                        {
                            eventDic[selectedEvent].Add(newPerson);
                            Console.WriteLine(newPerson.FirstName + "" + newPerson.LastName + " added to the event " + selectedEvent.Name);
                            break;
                        }
                    }
                }
            }

        }
        static void AllEventDetails(Dictionary<Event, List<Person>> eventDic)
        {
            PrintEvents(eventDic);
            Console.WriteLine("Enter name of the event: ");
            var eventName = Console.ReadLine();
            while (string.IsNullOrEmpty(eventName) && !DoesEventExist(eventDic, eventName))
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
                                PrintEventAttendees(eventDic,selectedEvent);
                                break;
                            case 3:
                                selectedEvent.PrintEventDetails(eventDic);
                                PrintEventAttendees(eventDic, selectedEvent);
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
        static void PrintEvents(Dictionary<Event, List<Person>> eventDic)
        {
            Console.WriteLine("List of events: ");
            foreach (var Event in eventDic.Keys)
                Console.WriteLine(Event.Name);
        } 
        static bool DoesEventExist(Dictionary<Event, List<Person>> eventDic, string name)
        {
            foreach (var Event in eventDic.Keys)
            {
                if(Event.Name == name)
                return true;
            }
            return false;
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
        static void PrintEventAttendees(Dictionary<Event, List<Person>> eventDic, Event eventName)
        {
            Console.WriteLine("List of event attendees for event " + eventName.Name);
            foreach(var person in eventDic[eventName])
                Console.WriteLine("[" + (eventDic[eventName].IndexOf(person) + 1) + "]" + " " + person.FirstName + " " + person.LastName + " " + person.PhoneNumber);
        }
    }
}
