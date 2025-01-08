using System.Xml;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using xmltest2;

namespace Xmltest
{
    public class Program
    {
        private static void Main(string[] args)
        {
            //metode der tager cars og læser dem fra xml filen hvis er det korrekte sted
            List<Car> cars = ReadCars("cars.xml");

            foreach (Car car in cars)
                Console.WriteLine(car);
            //laver en ny xml fil
            WriteCars(cars, "../../../cars2.xml");

        }
        // private metode der læser "cars" elementer og skriver dem ud i consol format
        private static List<Car> ReadCars(string path)
        {

            // instantiere en liste af klassen Car
           
            List<Car> cars = new List<Car>();

            // Opsætter hvordan XML readeren skal læse filen
            XmlReaderSettings settings = new XmlReaderSettings
            {
                IgnoreComments = true,
                IgnoreWhitespace = true

            };

            // dispose af xml filen, når vi er færdige med at bruge filen (undgår at korrupte data i filen)
            using (XmlReader reader = XmlReader.Create(path, settings))
            {
                reader.MoveToContent();
                reader.ReadStartElement("cars");
                {
                    while (reader.IsStartElement("car"))
                        cars.Add(ReadCar(reader));
                }
                reader.ReadEndElement("cars");
            }
            return cars;
        }
        
        private static Car ReadCar(XmlReader reader)
        {
            // Reader leder efter XML attribut "name"
            string carname = reader.GetAttribute("name");
            int cylinders = 0;
            string country = string.Empty;

            reader.ReadStartElement("car");

            while (reader.IsStartElement())
            {
                // Vi bruger en switch i stedet for if, da switches er mere kompakte, og performer hurtigere 
                
                switch (reader.LocalName)
                {
                    case "name":
                        carname = reader.ReadElementContentAsString();
                        break;
                    case "cylinders":
                        cylinders = reader.ReadElementContentAsInt();
                        break;
                    case "country":
                        country = reader.ReadElementContentAsString();
                        break;
                    default:
                        Console.WriteLine("unexpected element: " + reader.LocalName);
                        reader.Skip();
                        break;
                }
            }
            // afslutter læsningen af car elementet
            reader.ReadEndElement("car");

            // laver
            return new Car
            {
                name = carname,
                cylinders = cylinders,
                country = country
            };
        }

        private static void WriteCars(List<Car> cars, string path)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "  "
            };
            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                writer.WriteStartDocument();
                {
                    writer.WriteStartElement("cars");
                    {
                        foreach (Car car in cars)
                            WriteCars(writer, car);
                    }
                    writer.WriteEndElement("car");
                }
                writer.WriteEndDocument();
            }
        }




        private static void WriteCars(XmlWriter writer, Car car)
        {
            writer.WriteStartElement("car");
            {
                writer.WriteAttributeString("name", car.name);
                //writer.WriteStartAttribute("", "name", car.name);
                //writer.WriteStartAttribute("name", car.name);
                //writer.WriteElementString("carname", car.CarName);
                writer.WriteElementString ("cylinders", car.cylinders.ToString());
                writer.WriteElementString("country", car.country);
            }
            writer.WriteEndElement("car");
        }

    }
}
