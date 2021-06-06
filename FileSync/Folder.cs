using System;
using System.IO;

namespace FileSync
{
    public class Folder
    {
        public string path;
        public string[] files;
        public string copyTo;

        public Folder(string dir)
        {
            path = dir;
            files = Directory.GetFiles(path);
        }
    }

    public class FileInformation
    {
        public FileInfo currentFile;
        public DateTimeOffset lastWriteTime;
        public string fileName;
        public string dirName;
        string fullPath;

        public FileInformation(string file)
        {
            currentFile = new FileInfo(file);
            lastWriteTime = currentFile.LastWriteTime;
            fileName = currentFile.Name;
            dirName = currentFile.DirectoryName;
            fullPath = Path.Combine(dirName, fileName);
        }

        public void copyTo(Folder dir, LogXML log, SyncEntryJSON syncJSON)
        {
            File.Copy(fullPath, dir.copyTo);
            log.addFileEntry(fullPath, "скопирован в", dir.copyTo);
            EntryJSON entry = new EntryJSON(fullPath, "скопирован в", dir.copyTo);
            syncJSON.addFileEntry(entry);
        }

        public void copyFrom(Folder dir, LogXML log, SyncEntryJSON syncJSON)
        {
            File.Copy(dir.copyTo, fullPath);
            log.addFileEntry(dir.copyTo, "скопирован в", fullPath);
            EntryJSON entry = new EntryJSON(dir.copyTo, "скопирован в", fullPath);
            syncJSON.addFileEntry(entry);
        }

        public void overwrite(Folder dir, LogXML logXML, SyncEntryJSON syncJSON, int mode)
        {
            switch (mode)
            {
                case 1:
                    File.Delete(dir.copyTo);
                    logXML.addFileEntry(dir.copyTo, "удален, т.к. есть более новая версия");
                    EntryJSON entry = new EntryJSON(dir.copyTo, "удален, т.к. есть более новая версия");
                    syncJSON.addFileEntry(entry);
                    copyTo(dir, logXML, syncJSON);
                    break;
                case 2:
                    File.Delete(fullPath);
                    logXML.addFileEntry(fullPath, "удален, т.к. есть более новая версия");
                    entry = new EntryJSON(fullPath, "удален, т.к. есть более новая версия");
                    syncJSON.addFileEntry(entry);
                    copyFrom(dir, logXML, syncJSON);
                    break;
            }
            
        }
    }
}
