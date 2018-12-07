using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TubeQualityControl.Entity;

namespace TubeQualityControl.XmlHandler
{
    public class XmlWriter
    {

        static string filePathName = @"D:\tube.xml";

        public static string WriteXml(List<Part> parts)
        {
             
            //Here we use the XmlTextWriter to open a new XML file
            // FileStream filestream = new FileStream(filePathName, FileMode.Append);
            XmlTextWriter xmlTextWriter = new XmlTextWriter(filePathName, System.Text.Encoding.UTF8);


            xmlTextWriter.Formatting = Formatting.Indented;
            //Here we write XML declaration
            xmlTextWriter.WriteStartDocument();
            //Here we write the root elemnt
            xmlTextWriter.WriteStartElement("root");

            //Here we end the root element
            xmlTextWriter.WriteEndElement();
            //Here we end the document element
            xmlTextWriter.WriteEndDocument();
            //Here we flush and close the stream

            

            //xmlTextWriter.Flush();
            xmlTextWriter.Close();

            foreach (var part in parts)
            {
                AppendElement(part);
            }

            
            
            FileInfo fileInfo = new FileInfo(filePathName);

            //Here we return the file path and its length. 
            return Path.GetFullPath(filePathName) + " was written succefully. File size: " + fileInfo.Length + ".";
        }


        //This method returns file information
        public string GetFileInfo(string filePathName)
        {
            //Here we create a FileInfo object
            FileInfo fileInfo = new FileInfo(filePathName);


            //Here we return the file path and its length. 
            return Path.GetFullPath(filePathName) + " was written succefully. File size: " + fileInfo.Length + ".";
        }


        //This method appends new elements to the XML tree.
        public static string AppendElement(Part part)
        {
            //Reading and adding new data
            XmlTextReader xmlTextReader = new XmlTextReader(filePathName);
            //Does not return any whitespace node
            xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
            //Here we load the file into memory
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlTextReader);
            xmlTextReader.Close();
            XmlElement newTubeElement = xmlDocument.CreateElement("part");
            newTubeElement.SetAttribute("name",part.Name);
           // XmlElement newDataElement = xmlDocument.CreateElement("point");
            //newTubeElement.AppendChild(newDataElement);

            /*  int counter;
              for (counter = 0; counter <part.MeasurePoints.Count; counter++)
              {
                  XmlElement newXElement = xmlDocument.CreateElement("X");
                  newXElement.InnerText = part.MeasurePoints[counter].X.ToString();
                  newDataElement.AppendChild(newXElement);
                  XmlElement newYElement = xmlDocument.CreateElement("Y");
                  newYElement.InnerText = part.MeasurePoints[counter].Y.ToString();
                  newDataElement.AppendChild(newYElement);
                  XmlElement newZElement = xmlDocument.CreateElement("Z");
                  newZElement.InnerText = part.MeasurePoints[counter].Z.ToString();
                  newDataElement.AppendChild(newZElement);
              }*/

            XmlNode rootNode = xmlDocument.DocumentElement;
            // xmlDocument.GetElementById("employees").AppendChild(newEmpElement);

            //Here we would add the new child to the end of the root node
            foreach (var p in part.MeasurePoints) // p is point
            {
                //AppendElement(point);
                XmlElement newDataElement = xmlDocument.CreateElement("point");
                XmlElement newXElement = xmlDocument.CreateElement("X");
                newXElement.InnerText = p.X.ToString();
                newDataElement.AppendChild(newXElement);
                XmlElement newYElement = xmlDocument.CreateElement("Y");
                newYElement.InnerText = p.Y.ToString();
                newDataElement.AppendChild(newYElement);
                XmlElement newZElement = xmlDocument.CreateElement("Z");
                newZElement.InnerText = p.Z.ToString();
                newDataElement.AppendChild(newZElement);
                newTubeElement.AppendChild(newDataElement);
            }

            rootNode.AppendChild(newTubeElement);

            xmlDocument.Save(filePathName);
            return "Add child element successfully";
          // return GetFileInfo(filePathName);
        }



        public static string AppendElement(MeasurePoint p)
        {
            //Reading and adding new data
            XmlTextReader xmlTextReader = new XmlTextReader(filePathName);
            //Does not return any whitespace node
            xmlTextReader.WhitespaceHandling = WhitespaceHandling.None;
            //Here we load the file into memory
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(xmlTextReader);
            xmlTextReader.Close();
            XmlElement newDataElement = xmlDocument.CreateElement("point");
            XmlElement newXElement = xmlDocument.CreateElement("X");
            newXElement.InnerText = p.X.ToString();
            newDataElement.AppendChild(newXElement);
            XmlElement newYElement = xmlDocument.CreateElement("Y");
            newYElement.InnerText = p.Y.ToString();
            newDataElement.AppendChild(newYElement);
            XmlElement newZElement = xmlDocument.CreateElement("Z");
            newZElement.InnerText = p.Z.ToString();
            newDataElement.AppendChild(newZElement);
             

            XmlNode rootNode = xmlDocument.DocumentElement;
            // xmlDocument.GetElementById("employees").AppendChild(newEmpElement);

            //Here we would add the new child to the end of the root node
            rootNode.AppendChild(newDataElement);
            xmlDocument.Save(filePathName);
            return "Add child eöement successfully";
            // return GetFileInfo(filePathName);
        }

        private static void AddChildren(XmlNode xmlNode, int level)
        {
            XmlNode childXmlNode;
            //Here we speciy padding; th enumber of spaces based on the level in the XML tree
            string pad = new string(' ', level * 2);

            Console.WriteLine(pad + xmlNode.Name + "(" + xmlNode.NodeType.ToString() + ": " + xmlNode.Value + ")");
            //Here we extract possible attributes
            if (xmlNode.NodeType == XmlNodeType.Element)
            {

                XmlNamedNodeMap mapAttributes = xmlNode.Attributes;
                for (int i = 0; i < mapAttributes.Count; i++)
                {
                    Console.WriteLine(pad + " " + mapAttributes.Item(i).Name + "=" + mapAttributes.Item(i).Value);
                }

            }
            //Here we call recursively o all children of the current node
            if (xmlNode.HasChildNodes)
            {
                childXmlNode = xmlNode.FirstChild;
                while (childXmlNode != null)
                {
                    AddChildren(childXmlNode, level + 1);
                    childXmlNode = childXmlNode.NextSibling;
                }
            }
        }
    }
}
