using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace xmltest2
{
    internal static class ExtensionMethods
    {

        internal static void ReadEndElement(this XmlReader reader, string tagName)
        {
            if (reader.NodeType == XmlNodeType.EndElement &&
                reader.LocalName == tagName)
                reader.ReadEndElement();
            else
                throw new XmlException($"EndElement '{tagName}' was not found.");
        }

        internal static void WriteEndElement(this XmlWriter writer, string _)
        {
            writer.WriteEndElement();
        }
    }
}
