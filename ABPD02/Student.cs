using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace APBD02
{

    public class Student
    {
        [XmlAttribute(attributeName: "indexNumber")]
        public string indexNumber { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }
        // public string birthdate { get; set; }
        public string birthdate { get; set; }
        public string email { get; set; }
        public string mothersName { get; set; }
        public string fathersName { get; set; }
        public Studies studies { get; set; }




        public Student(string info)
        {
            var infoSep = info.Split(",");

            fname = infoSep[0];
            lname = infoSep[1];
            studies = new Studies(infoSep[2], infoSep[3]);
            indexNumber = "s" + infoSep[4];
            // birthdate = infoSep[5];
            birthdate = DateTime.Parse(infoSep[5]).ToString("dd.MM.yyyy");
            email = infoSep[6];
            mothersName = infoSep[7];
            fathersName = infoSep[8];
        }

        public Student()
        {

        }

        public override string ToString()
        {
            return $"{fname} {lname} {indexNumber}";
        }

    }
}
