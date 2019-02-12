using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartielJSONXML
{
    [Serializable()]
    public class Log
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string Niveau { get; set; }

        public Log(string message, DateTime date, string niveau)
        {
            Message = message;
            Date = date;
            Niveau = niveau;
        }
        public Log()
        { }
        public override string ToString()
        {
            return " Message : " + Message + "\n Date : " + Date + "\n Niveau : " + Niveau;
        }
    }
}

