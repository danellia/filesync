using System;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace FileSync
{
    public class LogXML
    {
        public XmlDocument log;
        FileStream fs;
        string logName = "log.xml";
        string currentTime;
        string timeTag;
        string outputEntry;
        List<string> output = new List<string>();
        XmlElement rootElement;
        XmlElement timeElement;
        XmlElement fileElement;
        XmlElement entryElement;
        XmlText entryText;

        public LogXML()
        {
            log = new XmlDocument();
            if (!File.Exists(logName))
            {
                fs = File.Create(logName);
                fs.Close();
            }

            currentTime = DateTime.Now.ToString();
            timeTag = currentTime.Replace(' ', '_').Replace(':', '.');

            log.Load(logName);
            rootElement = log.DocumentElement;
            timeElement = log.CreateElement("sync");
            timeElement.SetAttribute("time", timeTag);
            rootElement.AppendChild(timeElement);
            log.Save(logName);
        }
        XmlNode createElement(string element, string text)
        {
            entryElement = log.CreateElement(element);
            entryText = log.CreateTextNode(text);
            entryElement.AppendChild(entryText);

            return entryElement;
        }
        public void addFileEntry(string file, string state)
        {
            fileElement = log.CreateElement("file");
            fileElement.AppendChild(createElement("path", file));
            fileElement.AppendChild(createElement("state", state));
            timeElement.AppendChild(fileElement);

            log.Save(logName);
        }

        public void addFileEntry(string file, string state, string dir)
        {
            fileElement = log.CreateElement("file");
            fileElement.AppendChild(createElement("path", file));
            fileElement.AppendChild(createElement("state", state));
            fileElement.AppendChild(createElement("dir", dir));
            timeElement.AppendChild(fileElement);

            log.Save(logName);
        }

        public List<string> read()
        {
            log.Load(logName);
            XmlNodeList syncNodes = log.GetElementsByTagName("sync");
            foreach (XmlNode syncNode in syncNodes)
            {
                output.Add(syncNode.Attributes["time"].Value.ToString());
                foreach (XmlNode fileNode in syncNode.ChildNodes)
                {
                    outputEntry = "";
                    foreach (XmlNode node in fileNode.ChildNodes)
                    {
                        outputEntry += node.InnerText + " ";
                    }
                    output.Add(outputEntry);
                }
            }
            output.Add("Синхронизация завершена!");

            return output;
        }
    }
}
