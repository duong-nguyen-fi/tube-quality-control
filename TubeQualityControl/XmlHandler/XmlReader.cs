using System;
using System.Xml;
using System.IO;
using TubeQualityControl.Entity;
using System.Xml.Serialization;


namespace TubeQualityControl.XmlHandler
{
    class XmlReader
    {

        public Part Deserialize<Part>(string input) where Part : class
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Part));

            using (StringReader sr = new StringReader(input))
            {
                return (Part)ser.Deserialize(sr);
            }
        }

        public string Serialize<Part>(Part ObjectToSerialize)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(ObjectToSerialize.GetType());

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, ObjectToSerialize);
                FileStream stream = new FileStream(@"U:\C#\tube.xml", FileMode.Open);
                Part p = (Part)xmlSerializer.Deserialize(stream);
                stream.Close();
                return textWriter.ToString();
            }
        }
    }
}
