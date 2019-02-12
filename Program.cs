using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PartielJSONXML;

namespace PartielJSONXML
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var serializer = JsonSerializer.Create();
            List<Log> logs = new List<Log>();

            while (true)
            {
                Console.WriteLine(" 1 : Ajouter log JSON \n 2 : Sauvegarde JSON XML \n 3 : Afficher logs XML \n 4 : Quitter");
                string choix = Console.ReadLine();
                switch (choix)
                {
                    case "1":

                        Console.WriteLine("Message : ");
                        string msg = Console.ReadLine();
                        Console.WriteLine("\n Niveau : ");
                        string niveau = Console.ReadLine();

                        Log log = new Log(msg, DateTime.Now, niveau);
                        logs.Add(log);

                        if (File.Exists("JsonLogs.txt"))
                        {
                            using (StreamReader sr = new StreamReader("JsonLogs.txt"))
                            {
                                using (JsonReader reader = new JsonTextReader(sr))
                                {

                                    var log2 = serializer.Deserialize<List<Log>>(reader);
                                    foreach (Log item in log2)
                                    {
                                        logs.Add(item);
                                    }
                                }
                            }
                            using (StreamWriter sw = new StreamWriter("JsonLogs.txt"))
                            {
                                using (JsonWriter writer = new JsonTextWriter(sw))
                                {
                                    serializer.Serialize(writer, logs);
                                }
                            }
                        }
                        else
                        {
                            using (StreamWriter sw = new StreamWriter("JsonLogs.txt"))
                            {
                                using (JsonWriter writer = new JsonTextWriter(sw))
                                {
                                    serializer.Serialize(writer, logs);
                                }
                            }
                        }

                        break;

                    case "2":
                        var serializerXML = new XmlSerializer(typeof(List<Log>));
                        using (StreamReader sr = new StreamReader("JsonLogs.txt"))
                        {
                            using (JsonReader reader = new JsonTextReader(sr))
                            {
                                logs = serializer.Deserialize<List<Log>>(reader);

                            }
                        }
                        using (var sw = new StreamWriter("XMLlogs.xml"))
                        {
                            serializerXML.Serialize(sw, logs);
                        }
                        break;

                    case "3":
                        Console.WriteLine("Saisir jour mois et année ( format : aaaa-mm-jj)");
                        string date = Console.ReadLine();
                        XDocument xdocument = XDocument.Load("XMLlogs.xml");
                        IEnumerable<XElement> loggs = xdocument.Elements("ArrayOfLog").Descendants("Log").Where(x => x.Value.Contains(date));
                        foreach(var element in loggs)
                        {
                            Console.WriteLine(element.Value);
                        }
                              
                        break;

                    case "4":
                        System.Environment.Exit(-1);
                        break;
                }
            }
        }
    }
}

