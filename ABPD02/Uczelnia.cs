using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace APBD02
{

    [XmlRootAttribute("uczelnia")]
    public class Uczelnia
    {
        [XmlAttribute(attributeName: "createdAt")]
        // public string createdAt = DateTime.Today.ToShortDateString();
        public string createdAt = DateTime.Today.ToString("dd.MM.yyyy");


        [XmlAttribute(attributeName: "author")]
        public string author = "Jakub Krysztofiak";

        [XmlArrayItem("student")]
        public List<Student> studenci;
        [XmlArray("activeStudies")]
        [XmlArrayItem("studies")]
        public List<ActiveStudies> activeStudies = new List<ActiveStudies>();
        // public Dictionary<string, int> activeStudies;

        public Uczelnia(HashSet<Student> hash)
        {
            studenci = hash.ToList();

            Dictionary<string, int> studiesCounter = new Dictionary<string, int>();

            foreach (var student in studenci)
            {
                if (studiesCounter.ContainsKey(student.studies.name))
                {
                    studiesCounter[student.studies.name] += 1;
                }
                else
                {
                    studiesCounter.Add(student.studies.name, 1);
                }
            }

            foreach (var pair in studiesCounter)
            {
                activeStudies.Add(new ActiveStudies(pair.Key, pair.Value));
            }

        }

        public Uczelnia()
        {

        }
    }
}