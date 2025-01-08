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
            // Kalder metoden ReadCars for at læse bilerne fra "cars.xml"
            List<Car> cars = ReadCars("C:\\Users\\kehan\\source\\repos\\xmlbil\\cars.xml");

            // Går gennem listen af biler og udskriver hver bils oplysninger til konsollen
            foreach (Car car in cars)
                Console.WriteLine(car);

            // Kalder WriteCars for at skrive bilerne til en ny XML-fil ("cars2.xml")
            WriteCars(cars, "../../../cars2.xml");
        }

        // Metode til at læse bilerne fra en XML-fil
        private static List<Car> ReadCars(string path)
        {
            // Opretter en tom liste, som skal indeholde bilobjekter
            List<Car> cars = new List<Car>();

            // Initialiserer variabler til at beregne det samlede antal cylindre og antallet af biler
            int sum = 0;   // Total antal cylindre
            int antal = 0; // Antal biler

            // Konfigurerer XMLReader til at ignorere kommentarer og mellemrum i XML-filen
            XmlReaderSettings settings = new XmlReaderSettings
            {
                IgnoreComments = true,  // Ignorer kommentarer
                IgnoreWhitespace = true // Ignorer mellemrum og linjeskift
            };

            // Opretter en XMLReader til at læse XML-filen med de definerede indstillinger
            using (XmlReader reader = XmlReader.Create(path, settings))
            {
                // Flytter læseren til starten af XML-indholdet
                reader.MoveToContent();

                // Bekræfter, at vi starter med elementet <cars>
                reader.ReadStartElement("cars");

                // Læser alle <car>-elementer i XML-filen
                while (reader.IsStartElement("car"))
                {
                    // Kalder ReadCar-metoden for at læse en bils data
                    // Opdaterer også summen af cylindre og antallet af biler
                    Car car = ReadCar(reader, ref sum, ref antal);

                    // Tilføjer bilen til listen
                    cars.Add(car);
                }

                // Afslutter læsningen af <cars>-elementet
                reader.ReadEndElement("cars");
            }

            // Beregner og udskriver gennemsnittet af cylindre pr. bil, hvis der er biler i listen
            if (antal > 0)
            {
                double average = (double)sum / antal;
                Console.WriteLine("Gennemsnitlig antal cylindre: " + average.ToString("F2"));
            }
            else
            {
                // Hvis der ikke er nogen biler i XML-filen, udskrives en besked
                Console.WriteLine("Ingen cylinderdata fundet.");
            }

            // Returnerer listen af biler
            return cars;
        }

        // Metode til at læse data for en enkelt bil fra XML
        private static Car ReadCar(XmlReader reader, ref int sum, ref int antal)
        {
            // Variabler til at holde bilens data
            string carname = reader.GetAttribute("name") ?? string.Empty; // Læser bilens navn (attribut)
            int cylinders = 0;                                           // Antal cylindre
            string country = string.Empty;                              // Bilens oprindelsesland

            // Begynder at læse indholdet af <car>-elementet
            reader.ReadStartElement("car");

            // Læser alle child-elementer i <car>
            while (reader.IsStartElement())
            {
                // Switch statement bruges til at behandle hvert child-element
                switch (reader.LocalName)
                {
                    case "cylinders":
                        // Læser antal cylindre og opdaterer summen og antallet af biler
                        cylinders = reader.ReadElementContentAsInt();
                        sum += cylinders; // Lægger til den samlede sum af cylindre
                        antal++;          // Øger antallet af biler
                        break;

                    case "country":
                        // Læser bilens oprindelsesland
                        country = reader.ReadElementContentAsString();
                        break;

                    default:
                        // Hvis der findes et uventet element, springes det over
                        reader.Skip();
                        break;
                }
            }

            // Afslutter læsningen af <car>-elementet
            reader.ReadEndElement("car");

            // Returnerer en ny bil med de læste data
            return new Car
            {
                name = carname,
                cylinders = cylinders,
                country = country
            };
        }

        // Metode til at skrive bilerne til en ny XML-fil
        private static void WriteCars(List<Car> cars, string path)
        {
            // Konfigurerer XMLWriter til at formatere XML med indrykning
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,       // Slår indrykning til for læsbarhed
                IndentChars = "  "   // Brug to mellemrum til indrykning
            };

            // Opretter en XMLWriter til at skrive til filen
            using (XmlWriter writer = XmlWriter.Create(path, settings))
            {
                // Starter dokumentet
                writer.WriteStartDocument();

                // Starter <cars>-elementet
                writer.WriteStartElement("cars");

                // Går gennem listen af biler og skriver hver bil til XML
                foreach (Car car in cars)
                {
                    WriteCars(writer, car); // Kalder metoden til at skrive en enkelt bil
                }

                // Afslutter <cars>-elementet
                writer.WriteEndElement();

                // Afslutter dokumentet
                writer.WriteEndDocument();
            }
        }

        // Metode til at skrive data for en enkelt bil til XML
        private static void WriteCars(XmlWriter writer, Car car)
        {
            // Starter <car>-elementet
            writer.WriteStartElement("car");

            // Tilføjer "name"-attributten
            writer.WriteAttributeString("name", car.name);

            // Tilføjer <cylinders>-elementet
            writer.WriteElementString("cylinders", car.cylinders.ToString());

            // Tilføjer <country>-elementet
            writer.WriteElementString("country", car.country);

            // Afslutter <car>-elementet
            writer.WriteEndElement();
        }
    }
}
