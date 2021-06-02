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
        public string filename;
        public string dirname;

        public FileInformation(string file)
        {
            currentFile = new FileInfo(file);
            lastWriteTime = currentFile.LastWriteTime;
            filename = currentFile.Name;
            dirname = currentFile.DirectoryName;
        }

        public void copyTo(Folder dir)
        {
            File.Copy(Path.Combine(dirname, filename), dir.copyTo);
        }

        public void copyFrom(Folder dir)
        {
            File.Copy(dir.copyTo, Path.Combine(dirname, filename));
        }
    }
}
