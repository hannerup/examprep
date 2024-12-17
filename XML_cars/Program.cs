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
            List<Car> cars = ReadCars("cars.xml");

            foreach (Car car in cars)
                Console.WriteLine(car);

            WriteCars(cars, "../../../cars2.xml");

        }
        private static List<Car> ReadCars(string path)
        {
            List<Car> cars = new List<Car>();

            XmlReaderSettings settings = new XmlReaderSettings
            {
                IgnoreComments = true,
                IgnoreWhitespace = true

            };

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
            string carname = reader.GetAttribute("name");
            int cylinders = 0;
            string country = string.Empty;

            reader.ReadStartElement("car");

            while (reader.IsStartElement())
            {
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
            reader.ReadEndElement("car");
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
