using System.Xml.Serialization;

namespace APBD02
{
    public class ActiveStudies
    {

        [XmlAttribute]
        public string name { get; set; }
        [XmlAttribute]
        public int numberOfStudents { get; set; }

        public ActiveStudies(string studiesName, int numberOfStudents)
        {
            name = studiesName;
            this.numberOfStudents = numberOfStudents;
        }

        public ActiveStudies()
        {

        }

    }
}