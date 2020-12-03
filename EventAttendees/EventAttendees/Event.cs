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
            {
                Console.WriteLine("This name already exists!");
                return true;
            }
            else
                return false;
        }
    }
}
