using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Path to the XML file
        string xmlFilePath = "C:\\Users\\kehan\\source\\repos\\xmltest3\\textfile.xml";

        // Load the XML file
        XDocument xmlDoc = XDocument.Load(xmlFilePath);

        // Get the content of the single element
        string textContent = xmlDoc.Root?.Value;

        if (textContent == null)
        {
            Console.WriteLine("The XML file does not contain any text.");
            return;
        }

        // Initialize a dictionary to count the letters
        Dictionary<char, int> letterCounts = new Dictionary<char, int>();

        // Iterate through each character in the text
        foreach (char c in textContent)
        {
            // Convert character to lowercase to make the count case-insensitive
            char lowerChar = char.ToLower(c);

            // Check if the character is a letter
            if (char.IsLetter(lowerChar))
            {
                if (letterCounts.ContainsKey(lowerChar))
                {
                    letterCounts[lowerChar]++;
                }
                else
                {
                    letterCounts[lowerChar] = 1;
                }
            }
        }

        // Print the letter counts
        Console.WriteLine("Letter Frequencies:");
        foreach (var entry in letterCounts.OrderBy(k => k.Key))
        {
            Console.WriteLine($"{entry.Key}: {entry.Value}");
        }
    }
}