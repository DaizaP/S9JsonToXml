using System.Text.Json;
using System.Xml.Linq;

namespace S9JsonToXml
{
    internal class Program
    {
        static void Main()
        {
            string json = File.ReadAllText(@"C:\Users\Дмитрий\Desktop\Обучение\CSharp\ApplicationCSharp\S9JsonToXml\JsonIn.json");
            JsonDocument document = JsonDocument.Parse(json);
            XElement xElement = new XElement("Root");
            ConvertJsonToXml(document.RootElement, xElement);
            XDocument xmlDocument = new XDocument(xElement);
            xmlDocument.Save(@"C:\Users\Дмитрий\Desktop\Обучение\CSharp\ApplicationCSharp\S9JsonToXml\XmlOut.xml");
        }
        static void ConvertJsonToXml(JsonElement jsonElement, XElement xmlElement)
        {
            foreach (JsonProperty property in jsonElement.EnumerateObject())
            {
                if (property.Value.ValueKind == JsonValueKind.Object)
                {
                    if (xmlElement.Name == "Root")
                    {
                        xmlElement.Name = property.Name;
                        ConvertJsonToXml(property.Value, xmlElement);
                    }
                    else
                    {
                        XElement childXmlElement = new XElement(property.Name);
                        xmlElement.Add(childXmlElement);
                        ConvertJsonToXml(property.Value, childXmlElement);
                    }

                }
                else if (property.Value.ValueKind == JsonValueKind.Array)
                {
                    foreach (JsonElement arrayElement in property.Value.EnumerateArray())
                    {
                        XElement childXmlElement = new XElement(property.Name);
                        xmlElement.Add(childXmlElement);
                        ConvertJsonToXml(arrayElement, childXmlElement);
                    }
                }
                else
                {
                    XElement childXmlElement = new XElement(property.Name, property.Value);
                    xmlElement.Add(childXmlElement);
                }
            }
        }
    }
}