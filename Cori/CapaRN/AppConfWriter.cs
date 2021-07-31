using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using System.Xml;



namespace CapaRN
{
    public class AppConfWriter
    {

        private AppConfWriter() { }

        public static string ReadSetting(string key)
        {
            return ConfigurationSettings.AppSettings[key];
        }

        public static void WriteSetting(string key, string value)
        {
            // load config document for current assembly
            List<XmlDocument> docs = loadConfigDocument();
            string[] files = getConfigFilePath();
            int i = 0;

            foreach (XmlDocument doc in docs)
            {
                // retrieve appSettings node
                XmlNode node = doc.SelectSingleNode("//appSettings");

                if (node == null)
                    throw new InvalidOperationException("appSettings section not found in config file.");

                try
                {
                    // select the 'add' element that contains the key
                    XmlElement elem = (XmlElement)node.SelectSingleNode(string.Format("//add[@key='{0}']", key));

                    if (elem != null)
                    {
                        // add value for key
                        elem.SetAttribute("value", value);
                    }
                    else
                    {
                        // key was not found so create the 'add' element 
                        // and set it's key/value attributes 
                        elem = doc.CreateElement("add");
                        elem.SetAttribute("key", key);
                        elem.SetAttribute("value", value);
                        node.AppendChild(elem);
                    }
                    doc.Save(files[i++]);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private static List<XmlDocument> loadConfigDocument()
        {
            List<XmlDocument> docs = new List<XmlDocument>();
            try
            {
                string[] files = getConfigFilePath();
                foreach (string s in files)
                {
                    XmlDocument docu = new XmlDocument();
                    docu.Load(s);
                    docs.Add(docu);
                }
                return docs;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static string[] getConfigFilePath()
        {
            return Directory.GetFiles(Application.StartupPath, "*.config");
        }

    }
}
