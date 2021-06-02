using System;
using System.IO;
using System.Windows.Forms;

namespace FileSync
{
    public partial class Form1 : Form
    {
        Folder dir1;
        Folder dir2;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    textBox1.Text = fbd.SelectedPath;
                    dir1 = new Folder(fbd.SelectedPath);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                if (fbd.ShowDialog() == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    textBox2.Text = fbd.SelectedPath;
                    dir2 = new Folder(fbd.SelectedPath);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            foreach (var file1 in dir1.files)
            {
                FileInformation currentFile1 = new FileInformation(file1);
                dir2.copyTo = Path.Combine(dir2.path, currentFile1.filename);
                if (!File.Exists(dir2.copyTo))
                {
                    currentFile1.copyTo(dir2);
                }
                else
                {
                    FileInformation currentFile2 = new FileInformation(dir2.copyTo);
                    if (currentFile1.lastWriteTime >= currentFile2.lastWriteTime)
                    {
                        File.Delete(dir2.copyTo);
                        currentFile1.copyTo(dir2);
                    }
                    else
                    {
                        File.Delete(file1);
                        currentFile1.copyFrom(dir2);
                    }
                }
                foreach (var file2 in dir2.files)
                {
                    FileInformation currentFile2 = new FileInformation(file2);
                    dir1.copyTo = Path.Combine(dir1.path, currentFile2.filename);
                    if (!File.Exists(dir1.copyTo))
                    {
                        currentFile2.copyTo(dir1);
                    }
                }
            }
        }
    }
}
