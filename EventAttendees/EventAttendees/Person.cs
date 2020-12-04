using System;
using System.Collections.Generic;
using System.Text;

namespace EventAttendees
{
    public class Person
    {
        public Person(string firstName, string lastName, int _OIB, int phoneNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            OIB = _OIB;
            PhoneNumber = phoneNumber;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int OIB { get; set; }
        public int PhoneNumber { get; set; }

        public Person()
        {
            PersonInput();
        }
        public bool DoesHaveSameOIB(int newOIB)
        {
            return (OIB == newOIB);
        }
        public bool IsPersonAlreadyGoingToEvent(Dictionary<Event, List<Person>> eventDic, Event myEvent)
        {
            var peopleGoingToMyEvent = eventDic[myEvent];

            return peopleGoingToMyEvent.Contains(this);
        }
        public void PersonInput()
        {
            Console.WriteLine("Please enter first name of the person: ");
            var name = Console.ReadLine();
            while (string.IsNullOrEmpty(name))
            {
                Console.WriteLine("Please enter first name of the person: ");
                name = Console.ReadLine();
            }
            FirstName = name;

            Console.WriteLine("Please enter last name of the person: ");
            var surname = Console.ReadLine();
            while (string.IsNullOrEmpty(surname))
            {
                Console.WriteLine("Please enter last name of the person: ");
                surname = Console.ReadLine();
            }
            LastName = surname;

            Console.WriteLine("Please enter OIB of the person: ");
            var OIBSuccsess = int.TryParse(Console.ReadLine(), out int _OIB);
            while (!OIBSuccsess || DoesHaveSameOIB(_OIB))
            {
                Console.WriteLine("Please enter OIB of the person: ");
                OIBSuccsess = int.TryParse(Console.ReadLine(), out _OIB);
            }
            OIB = _OIB;

            Console.WriteLine("Please enter phone number of the person: ");
            var phoneNumberSuccsess = int.TryParse(Console.ReadLine(), out int phoneNumber);
            while (!phoneNumberSuccsess)
            {
                Console.WriteLine("Please enter OIB of the person: ");
                phoneNumberSuccsess = int.TryParse(Console.ReadLine(), out phoneNumber);
            }
            PhoneNumber = phoneNumber;

        }
        public void PersonDetails()
        {
            Console.WriteLine(FirstName + " " + LastName + " " + "OIB: " + OIB + " " + "phone number: " + PhoneNumber);
        }

    }
}
