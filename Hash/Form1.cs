using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace Hash
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        String pathOpenFile = "";
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Open File";
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                pathOpenFile = ofd.FileName;
                cboState.Enabled = true;
                label3.Text = ofd.SafeFileName;
            }
        }


        String pathSaveFile = "";
        private void button2_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Title = "Save As";
            sfd.Filter = "Text File|*.txt";
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string ext = Path.GetExtension(sfd.FileName);
                pathSaveFile = sfd.FileName;
                pathSaveFile = pathSaveFile.Replace(ext, "-" + cboState.Text + ext);
                StreamWriter sw = new StreamWriter(File.Create(pathSaveFile));
                sw.Write(myHash);
                sw.Dispose();

            }

        }

        String myHash = "";
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            String cboText = cboState.Text;
            HashAlgorithm function = null;
            if (cboText == "SHA-1")
            {
                function = new SHA1CryptoServiceProvider();
            }

            else if (cboText == "SHA-256")
            {
                function = new SHA256CryptoServiceProvider();
            }

            else if(cboText == "MD5")
            {
                function = new MD5CryptoServiceProvider();
            }
            if (function != null)
            {
                UTF8Encoding utf8 = new UTF8Encoding();
                button2.Enabled = true;
                myHash = BitConverter.ToString(function.ComputeHash(utf8.GetBytes(pathOpenFile)));
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }
    }
}
