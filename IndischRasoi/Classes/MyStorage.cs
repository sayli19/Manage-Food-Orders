using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IndischRasoi
{
    public class MyStorage
    {
        public static void WriteXml<T>(T data, string file)
        {
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(T));
                FileStream fs;
                fs = new FileStream(file, FileMode.Create);
                xs.Serialize(fs, data);
                fs.Close();
            }
            catch //(Exception)
            {
             
            }
        }

        public static T ReadXml<T>(string file)
        {
            try
            {
                using (StreamReader sr = new StreamReader(file))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(T));
                    return (T)xs.Deserialize(sr);
                }
            }
            catch //(Exception)
            {

                return default(T);
            }
        }

    }
}
