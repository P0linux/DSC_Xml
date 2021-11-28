// See https://aka.ms/new-console-template for more information
using ConsoleApp;

Console.WriteLine("Hello, World!");

var planes = XmlParcer.ParceXml("ValidCollectionXml.xml");

foreach (var plane in planes)
{
    Console.WriteLine(plane.Id);
}

Console.WriteLine("\nXML validation result:");
XmlParcer.ValidateXml("InvalidXml.xml");

Console.WriteLine("\nXML -> JSON:");
XmlParcer.XmlToJson("ValidCollectionXml.xml");
