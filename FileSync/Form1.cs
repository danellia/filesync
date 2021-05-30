using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace FileSync
{
    public partial class Form1 : Form
    {
        string dir1;
        string dir2;
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
                    dir1 = fbd.SelectedPath;
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
                    dir2 = fbd.SelectedPath;
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var files1 = Directory.GetFiles(dir1);
            var files2 = Directory.GetFiles(dir2);

            foreach (var file1 in files1)
            {
                FileInfo currentFile1 = new FileInfo(file1);
                DateTimeOffset lastWriteTime1 = currentFile1.LastWriteTime;
                string filename1 = currentFile1.Name;
                foreach (var file2 in files2)
                {
                    FileInfo currentFile2 = new FileInfo(file2);
                    DateTimeOffset lastWriteTime2 = currentFile2.LastWriteTime;
                    string filename2 = currentFile2.Name;
                    if (!File.Exists(dir2 + filename1))
                    {
                        File.Copy(file1, file2);
                    } else
                    {
                        if (lastWriteTime1 >= lastWriteTime2)
                        {
                            File.Delete(file2);
                            File.Copy(file1, file2);
                        }
                        else
                        {
                            File.Delete(file1);
                            File.Copy(file2, file1);
                        }
                    }
                    if (!File.Exists(dir1 + filename2))
                    {
                        File.Copy(file2, file1);
                    }
                }
            }
        }
    }
}
