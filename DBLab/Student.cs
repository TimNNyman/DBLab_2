using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBLab
{
    public class Student
    {
        private string studentID;
        private string firstName;
        private string lastName;
        private string gender;
        private string streetAdress;
        private string city;
        private string country;
        private string zipCode;

        public string StudentID
        {
            get => studentID;
            set => studentID = value == string.Empty ? null : value;
        }
        public string FirstName
        {
            get => firstName;
            set => firstName = value == string.Empty ? null : value;
        }
        public string LastName
        {
            get => lastName;
            set => lastName = value == string.Empty ? null : value;
        }
        public string Gender
        {
            get => gender;
            set => gender = value == string.Empty ? null : value;
        }
        public string StreetAdress
        {
            get => streetAdress;
            set => streetAdress = value == string.Empty ? null : value;
        }
        public string City
        {
            get => city;
            set => city = value == string.Empty ? null : value;
        }
        public string Country
        {
            get => country;
            set => country = value == string.Empty ? null : value;
        }
        public string ZipCode
        {
            get => zipCode;
            set => zipCode = value == string.Empty ? null : value;
        }
        public string BirthDate { get; set; }
        public string StudentType { get; set; }
    }
}
