using Models;
using Newtonsoft.Json;
using System.Xml;
using System.Xml.Schema;

namespace ConsoleApp
{
    public static class XmlParcer
    {
        public static Plane[] ParceXml(string path)
        {
            var planes = new List<Plane>();

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            var root = xmlDocument.DocumentElement;

            var childNodes = root!.SelectNodes("*")!;

            foreach (XmlNode node in childNodes)
            {
                var plane = new Plane
                {
                    Id = node.SelectSingleNode("@Id")?.Value ?? "",
                    Model = node.SelectSingleNode("Model")?.InnerText ?? "",
                    Origin = node.SelectSingleNode("Origin")?.InnerText ?? "",
                    Price = Convert.ToSingle(node.SelectSingleNode("Price")?.InnerText),
                };

                var planeCharNodes = node.SelectNodes("//Chars/*")!;

                plane.Chars = (planeCharNodes.Cast<XmlNode>()
                    .Select(planeCharNode => new PlaneChar
                    {
                        Type = Enum.Parse<PlaneType>(planeCharNode.SelectSingleNode("Type")?.InnerText ?? "Unknown"),
                        PlaceNumber = Convert.ToInt32(planeCharNode.SelectSingleNode("PlaceNumber")?.InnerText),
                        HasAmmunition = Convert.ToBoolean(planeCharNode.SelectSingleNode("HasAmmunition")?.InnerText),
                        RocketNumber = Convert.ToInt32(planeCharNode.SelectSingleNode("RocketNumber")?.InnerText),
                        HasRadar = Convert.ToBoolean(planeCharNode.SelectSingleNode("HasRadar")?.InnerText)
                    }).ToArray());

                var planeParameterNode = node.SelectSingleNode("Parameters");

                plane.Parameters = new PlaneParameters
                {
                    Height = Convert.ToInt32(planeParameterNode?.SelectSingleNode("Height")?.InnerText),
                    Width = Convert.ToInt32(planeParameterNode?.SelectSingleNode("Width")?.InnerText),
                    Length = Convert.ToInt32(planeParameterNode?.SelectSingleNode("Length")?.InnerText)
                };

                planes.Add(plane);
            }

            planes.Sort(new PlaneComparer());

            return planes.ToArray();
        }

        public static void XmlToJson(string path)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            var root = xmlDocument.DocumentElement;

            var childNodes = root!.SelectNodes("*")!;

            foreach (XmlNode node in childNodes)
                Console.WriteLine(JsonConvert.SerializeXmlNode(node, Newtonsoft.Json.Formatting.Indented, false));
        }

        public static void ValidateXml(string path)
        {
            var schema = new XmlSchemaSet();
            schema.Add("", "Plane.xsd");

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(path);
            xmlDocument.Schemas.Add(schema);
            xmlDocument.Validate(ValidationEventHandler!);
        }

        private static void ValidationEventHandler(object sender, ValidationEventArgs e)
        {
            if (e.Severity == XmlSeverityType.Error)
                Console.WriteLine(e.Message);
        }
    }
}
