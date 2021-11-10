using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HeBianGu.Common.LocalConfig
{
    public class XmlProvider
    {
        //public void Save<T>(string filePath, T sourceObj, string xmlRootName = null)
        //{
        //    if (string.IsNullOrWhiteSpace(filePath)) return;

        //    string folder = Path.GetDirectoryName(filePath);

        //    if (!Directory.Exists(folder))
        //    {
        //        Directory.CreateDirectory(folder);
        //    }

        //    using (FileStream stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        using (StreamWriter writer = new StreamWriter(stream))
        //        {
        //            XmlSerializer xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName) ?
        //                new XmlSerializer(sourceObj.GetType()) :
        //                new XmlSerializer(sourceObj.GetType(), new XmlRootAttribute(xmlRootName));
        //            xmlSerializer.Serialize(writer, sourceObj);
        //        }
        //    }
        //}

        public void Save(string filePath, object sourceObj, string xmlRootName = null)
        {
            if (string.IsNullOrWhiteSpace(filePath)) return;

            string folder = Path.GetDirectoryName(filePath);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            using (FileStream stream = new FileStream(filePath, FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    XmlSerializer xmlSerializer = string.IsNullOrWhiteSpace(xmlRootName) ?
                        new XmlSerializer(sourceObj.GetType()) :
                        new XmlSerializer(sourceObj.GetType(), new XmlRootAttribute(xmlRootName));
                    xmlSerializer.Serialize(writer, sourceObj);
                }
            }
        }

        public T Load<T>(string filePath)
        {
            return (T)this.Load(filePath, typeof(T));
        }

        public object Load(string filePath, Type type)
        {
            object result = null;

            if (File.Exists(filePath))
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    XmlSerializer xmlSerializer = new XmlSerializer(type);
                    result = xmlSerializer.Deserialize(reader);
                }
            }

            return result;
        }
    }
}
