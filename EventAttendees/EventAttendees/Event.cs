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

        public bool DoesOverlapEvent(Event otherEvent)
        {
            if (DateTime.Compare(StartTime, otherEvent.StartTime) < 0 && DateTime.Compare(EndTime, otherEvent.StartTime) > 0)
            {
                Console.WriteLine("There can't be more than one event in the same time! At wished moment is event" + Name + " already scheduled");
                return true;
            }
            else if (DateTime.Compare(StartTime, otherEvent.EndTime) < 0 && DateTime.Compare(EndTime, otherEvent.EndTime) > 0)
            {
                Console.WriteLine("There can't be more than one event in the same time! At wished moment is event" + Name + " already scheduled");
                return true;
            }
            else if (DateTime.Compare(EndTime, otherEvent.StartTime) > 0 && DateTime.Compare(StartTime, otherEvent.EndTime) < 0)
            {
                Console.WriteLine("There can't be more than one event in the same time! You should end earlier because event " + Name + " is going to start");
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
