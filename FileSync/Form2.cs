using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace FileSync
{
    public partial class Form2 : Form
    {
        LogXML logXML;
        LogJSON logJSON;
        List<string> output;

        public Form2()
        {
            logXML = new LogXML();
            logJSON = new LogJSON();
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            clearListBox();
            output = logXML.read();
            foreach (string entry in output)
            {
                listBox1.Items.Add(entry);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            clearListBox();
            output = logJSON.read();
            foreach (string entry in output)
            {
                listBox1.Items.Add(entry);
            }
        }

        void clearListBox()
        {
            listBox1.Items.Clear();
            if (output != null)
            {
                output.Clear();
            }
        }
    }
}

