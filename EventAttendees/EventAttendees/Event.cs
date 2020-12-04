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
        public string Name { get; set; }
        public EventType TypeOfEvent { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool DoesOverlapEvent(DateTime otherEventTime)
        {
            if (DateTime.Compare(StartTime, otherEventTime) < 0 && DateTime.Compare(EndTime, otherEventTime) > 0)
            {
                Console.WriteLine("There can't be more than one event in the same time! At wished moment is event " + Name + " already scheduled");
                return true;
            }
            else
                return false;
        }
        public bool DoesHaveSameName(string name)
        {
            if (name == Name)
                Console.WriteLine("This name already exists!");

                return (name == Name);
        }
       
        public void PrintEventDetails(Dictionary<Event, List<Person>> eventDic)
        {
            Console.WriteLine("Name of the event is: " + Name);
            Console.WriteLine("Event type of the event is: " + TypeOfEvent);
            Console.WriteLine("Start time of the event is: " + StartTime);
            Console.WriteLine("End time of the event is: " + EndTime);
            Console.WriteLine("Event lasts: " + (EndTime - StartTime));
            Console.WriteLine("Number of event attendees is: " + eventDic.Values.Count);
        }
    }
}
