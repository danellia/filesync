using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;

namespace FileSync
{
    public class EntryJSON
    {
        string Path { get; set; }
        string State { get; set; }
        string Dir { get; set; }
        string entry { get; set; }

        public EntryJSON(string file, string state)
        {
            Path = file;
            State = state;
        }

        public EntryJSON(string file, string state, string dir)
        {
            Path = file;
            State = state;
            Dir = dir;
        }

        public override string ToString()
        {
            if (Dir != null)
            {
                entry = String.Format("{0} {1} {2}", Path, State, Dir);
            }
            else
            {
                entry = String.Format("{0} {1}", Path, State);
            }
            return entry;
        }
    }

    public class SyncEntryJSON
    {
        public string timeTag { get; set; }
        string currentTime { get; set; }
        public List<string> files { get; set; }

        public SyncEntryJSON()
        {
            currentTime = DateTime.Now.ToString();
            timeTag = currentTime.Replace(' ', '_').Replace(':', '.');
            files = new List<string>();
        }

        public void addFileEntry(EntryJSON entry)
        {
            files.Add(entry.ToString());
        }
    }
    public class LogJSON
    {
        string element;
        string logName = "log.json";
        string log;
        List<string> output = new List<string>();
        FileStream fs;

        public LogJSON()
        {
            if (!File.Exists(logName))
            {
                fs = File.Create(logName);
                fs.Close();
            }
        }
        public void serialize(SyncEntryJSON entry)
        {
            element = JsonConvert.SerializeObject(entry);
            File.WriteAllText(logName, element);
        }
        public List<string> read()
        {
            log = File.ReadAllText(logName);
            SyncEntryJSON syncEntry = JsonConvert.DeserializeObject<SyncEntryJSON>(log);
            output.Add(syncEntry.timeTag);
            foreach (string fileNode in syncEntry.files)
            {
                output.Add(fileNode.ToString());
            }
            output.Add("Синхронизация завершена!");

            return output;
        }
    }
}
