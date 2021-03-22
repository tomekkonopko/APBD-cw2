using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using Formatting = System.Xml.Formatting;

namespace APBD02
{
    class Program
    {
        static void Main(string[] args)
        {

            var hash = new HashSet<Student>(new Comparator());
            var log = new StringBuilder("");


            var pathCSV = @"C:\Users\Tomek\Desktop\APBD-cw2\ABPD02\data.csv";
            var pathRes = @"C:\Users\Tomek\Desktop\APBD-cw2\ABPD02\result.json";
            var pathLog = @"C:\Users\Tomek\Desktop\APBD-cw2\ABPD02\log.txt";
            var type = "json";

            // if (args.Length == 1)
            //{
                //pathCSV = args[0];
                //pathRes = args[1];
                //type = args[2].ToLower();
            //}

            //if (!File.Exists(pathLog))
            //{
            //    File.Create(pathLog).Dispose();
            //}


            try
            {
                var lines = File.ReadLines(pathCSV);

                foreach (var line in lines)
                {
                    var info = line.Split(",");

                    if (info.Length == 9)
                    {
                        var student = new Student(line);
                        Boolean ok = true;
                        foreach (var item in info)
                        {
                            if (String.IsNullOrEmpty(item))
                            {
                                ok = false;
                                break;
                            }
                        }
                        if (ok)
                        {
                            if (!hash.Add(student))
                            {
                                log.Append(line + Environment.NewLine);
                                // Console.WriteLine("Student NIE dodany");
                            }
                            else
                            {
                                // Console.WriteLine("Student dodany");
                            }
                        }
                    }
                    else
                    {
                        log.Append(line + Environment.NewLine);
                        // Console.WriteLine("Student NIE dodany");
                    }

                }
            }
            catch (FileNotFoundException fe)
            {
                Console.WriteLine($"Plik {pathCSV} nie istnieje");
                log.Append($"Plik {pathCSV} nie istnieje");
                System.IO.File.WriteAllText(pathLog, log.ToString());
                return;
            }
            catch (DirectoryNotFoundException de)
            {
                Console.WriteLine("Podana ścieżka do pliku .csv jest niepoprawna");
                log.Append("Podana ścieżka do pliku .csv jest niepoprawna");
                System.IO.File.WriteAllText(pathLog, log.ToString());
                return;
            }



            Uczelnia uczelnia = new Uczelnia(hash);

            try
            {
                switch (type)
                {
                    case "xml":
                        SaveXml(uczelnia, pathRes);
                        break;
                    case "json":
                        SaveJson(uczelnia, pathRes);
                        break;
                    default:
                        log.Append("Niepoprawny typ zapisu!");
                        Console.WriteLine("Niepoprawny typ zapisu!");
                        break;
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine($"Plik {pathRes} nie istnieje");
                log.Append($"Plik {pathRes} nie istnieje");
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Podana ścieżka do pliku wyściowego jest niepoprawna");
                log.Append("Podana ścieżka do pliku wyściowego jest niepoprawna");
            }

            // Console.WriteLine(log);
            System.IO.File.WriteAllText(pathLog, log.ToString());

        }



        public static void SaveJson(Uczelnia uczelnia, string path)
        {

            string output = JsonConvert.SerializeObject(new { uczelnia = uczelnia }, Newtonsoft.Json.Formatting.Indented);

            System.IO.File.WriteAllText(path, output);
        }

        public static void SaveXml(Uczelnia uczelnia, string path)
        {
            XmlSerializer xs = new XmlSerializer(typeof(Uczelnia));
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();

            ns.Add("", "");
            var xml = "";
            using (var sww = new StringWriter())
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.OmitXmlDeclaration = true;

                using (XmlWriter writer = XmlWriter.Create(sww, settings))
                {
                    xs.Serialize(writer, uczelnia, ns);
                    xml = sww.ToString();
                }
            }

            System.IO.File.WriteAllText(path, xml.ToString());

        }
    }
}
