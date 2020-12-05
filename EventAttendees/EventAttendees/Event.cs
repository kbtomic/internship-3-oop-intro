using System;
using System.Collections.Generic;
using System.Text;

namespace EventAttendees
{
    public enum EventType
    {
        Coffee,
        Lecture,
        Concert,
        StudySession
    }
    public class Event
    {
        public Event(string name, EventType typeOfEvent, DateTime startTime, DateTime endTime)
        {
            Name = name;
            TypeOfEvent = typeOfEvent;
            StartTime = startTime;
            EndTime = endTime;

        }
        public Event()
        {

        }

        public string Name { get; set; }
        public EventType TypeOfEvent { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool SuccessfullyEventNameInput(Dictionary<Event, List<Person>> eventDic)
        {
            Console.WriteLine("Please insert name of the event: ");
            var eventName = Console.ReadLine();
            while (string.IsNullOrEmpty(eventName))
            {
                Console.WriteLine("Please insert name of the event: ");
                eventName = Console.ReadLine();
            }
            Name = eventName;
            if (DoesHaveSameName(eventDic))
                return false;
            else
                return true;
        }
        public bool SuccessfullyEventTypeInput(Dictionary<Event, List<Person>> eventDic)
        {
            Console.WriteLine("Please insert type of event: Enter '0' for coffee, enter '1' for lecture, enter '2' for concert, enter '3' for study session!");
            var typeOfEventSuccess = int.TryParse(Console.ReadLine(), out var choice);
            while (!typeOfEventSuccess || choice > 3 || choice < 0)
            {
                Console.WriteLine("Please insert valid number in range 0-3!");
                typeOfEventSuccess = int.TryParse(Console.ReadLine(), out choice);
            }
            TypeOfEvent = (EventType)choice;
            return true;
        }
        public bool SuccessfullyStartAndEndTimeInput(Dictionary<Event, List<Person>> eventDic)
        {
            Console.WriteLine("Please insert start time of event. Use format yyyy/mm/dd hh:mm:ss!");
            var startTimeSuccess = DateTime.TryParse(Console.ReadLine(), out DateTime startTime);
            while (!startTimeSuccess)
            {
                Console.WriteLine("Please insert valid start time of event. Use format yyyy/mm/dd hh:mm:ss!");
                startTimeSuccess = DateTime.TryParse(Console.ReadLine(), out startTime);
            }
            StartTime = startTime;

            Console.WriteLine("Please insert end time of event. Use format yyyy/mm/dd hh:mm:ss!");
            var endTimeSuccess = DateTime.TryParse(Console.ReadLine(), out DateTime endTime);
            while (!endTimeSuccess)
            {
                Console.WriteLine("Please insert valid end time of event. Use format yyyy/mm/dd hh:mm:ss!");
                endTimeSuccess = DateTime.TryParse(Console.ReadLine(), out endTime);
            }
            EndTime = endTime;

            if (DateTime.Compare(EndTime, StartTime) < 0)
            {
                Console.WriteLine("Even't can't end before it even started!");
                return false;
            }
            foreach (var eventloop in eventDic.Keys)
            {
                if (DoesOverlapEvent(eventloop.EndTime))
                    return false;
                else if (DoesOverlapEvent(eventloop.StartTime))
                    return false;
            }
            return true;
        }
       
        public Event EventInput(Dictionary<Event, List<Person>> eventDic)
        {
            if(SuccessfullyEventNameInput(eventDic) && SuccessfullyEventTypeInput(eventDic) && SuccessfullyStartAndEndTimeInput(eventDic))
                return new Event(Name,TypeOfEvent,StartTime,EndTime);
            return null;
        }
        public bool DoesOverlapEvent(DateTime otherEventTime)
        {
            if (DateTime.Compare(StartTime, otherEventTime) < 0 && DateTime.Compare(EndTime, otherEventTime) > 0)
            {
                Console.WriteLine("There can't be more than one event in the same time!");
                return true;
            }
            else
                return false;
        }
        public bool DoesHaveSameName(Dictionary<Event, List<Person>> eventDic)
        {
            foreach(var eventloop in eventDic.Keys)
            {
                if (Name == eventloop.Name)
                {
                    Console.WriteLine("Requested name already exist!");
                    return true;
                }
            }
            return false;
        }
       
        public void PrintEventDetails(Dictionary<Event, List<Person>> eventDic)
        {
            Console.WriteLine("Name of the event is: " + Name);
            Console.WriteLine("Event type of the event is: " + TypeOfEvent);
            Console.WriteLine("Start time of the event is: " + StartTime);
            Console.WriteLine("End time of the event is: " + EndTime);
            Console.WriteLine("Event lasts: " + (EndTime - StartTime));
            Console.WriteLine("Number of event attendees is: " + eventDic[this].Count);
        }
        public void PrintEventAttendees(Dictionary<Event, List<Person>> eventDic)
        {
            Console.WriteLine("List of event attendees for event " + Name);
            foreach (var person in eventDic[this])
                Console.WriteLine("[" + (eventDic[this].IndexOf(person) + 1) + "]" + " " + person.FirstName + " " + person.LastName + " " + person.PhoneNumber);
        }
    }
}
