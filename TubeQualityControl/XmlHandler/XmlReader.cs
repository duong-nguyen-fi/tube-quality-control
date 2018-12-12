using System;
using System.Xml;
using System.IO;
using TubeQualityControl.Entity;
using System.Collections.Generic;

namespace TubeQualityControl.XmlHandler
{
    class XmlReader
    {
        static string filePathName = MainFrm.CurrentDir + "/res/tube.xml".Replace("/", Path.DirectorySeparatorChar + "");

        public static string ReadXml(List<Part> parts)
        {
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(filePathName);
            XmlNodeList nodeList = xmldoc.GetElementsByTagName("part");
            string part = string.Empty;
            foreach (XmlNode node in nodeList)
            {
                if (node.InnerText.Contains("part"))
                {
                    List<MeasurePoint> points = new List<MeasurePoint>();
                    Part p = new Part(node.Attributes["name"].Value, points);
                    XmlNodeList nodelist = xmldoc.GetElementsByTagName("point");
                    foreach (XmlNode n in nodelist)
                    {
                        points.Add(new MeasurePoint(Convert.ToDouble(n["x"].InnerText), Convert.ToDouble(n["y"].InnerText), Convert.ToDouble(n["z"].InnerText)));
                    }
                    return p.ponitsToString();
                }
            }
            return "read successfully";
        }

    }
}
