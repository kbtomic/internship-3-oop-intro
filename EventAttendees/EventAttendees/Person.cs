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
    }
}
