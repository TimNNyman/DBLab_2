using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DBLab
{
    public class Phone
    {
        public string StudentID { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneType { get; set; }

        public Phone(string number, string type, string studentId)
        {
            PhoneNumber = number;
            PhoneType = type;
            StudentID = studentId;
        }

        public override string ToString()
        {
            return PhoneNumber + " - " + PhoneType;
        }
    }
}
