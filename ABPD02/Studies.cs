using System.Runtime.Serialization;

namespace APBD02
{

    public class Studies
    {
        public string name { get; set; }
        public string mode { get; set; }

        public Studies(string studies, string type)
        {
            this.name = studies;
            this.mode = type;
        }

        public Studies()
        {
        }

    }
}