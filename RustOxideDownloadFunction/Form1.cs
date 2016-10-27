using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using Ionic.Zip;

namespace RustOxideDownloadFunction
{
    public partial class Form1 : Form
    {
        string OxideMod_Location;
        string OxideMod_Decompression;
        const string OxideDownload = "https://github.com/OxideMod/Snapshots/raw/master/Oxide-Rust.zip?1474682786";

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            WebClient oxideModDL = new WebClient();

            // Progress bar event handlers.
            oxideModDL.DownloadProgressChanged += (s, t) =>
            {
                progressBar1.Visible = true;
                progressBar1.Value = t.ProgressPercentage;
            };

            // Progress bar event handlers.
            oxideModDL.DownloadFileCompleted += (s, t) =>
            {
                progressBar1.Value = 0;
                progressBar1.Visible = false;

                DecompressOxideMod();

            };



            oxideModDL.DownloadFileAsync(new Uri(OxideDownload), OxideMod_Location); 
        }

        // Allows the user to select the directory location for download.
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFileDialog SelectFolderObject1 = new SaveFileDialog();

            SelectFolderObject1.FileName = "Oxide-Rust.zip";

            SelectFolderObject1.Filter = "Zip Files|*.zip|All Files (*.*)|*.*";

            SelectFolderObject1.ShowDialog();

            var storeFolderPath = SelectFolderObject1.FileName;

            textBox1.Text = storeFolderPath;

            OxideMod_Location = storeFolderPath;

        }

        private void DecompressOxideMod()
        {
            // Working
             // ZipFile.ExtractToDirectory(OxideMod_Location, OxideMod_Decompression, ExtractExistingFileAction.OverwriteSilently);

            // Working
            ZipFile zipdecompression = ZipFile.Read(OxideMod_Location);

                foreach (ZipEntry zipentry in zipdecompression)
                {
                
                zipentry.Extract(OxideMod_Decompression, ExtractExistingFileAction.OverwriteSilently);

                } 

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        // Allows the user to select extraction location.
        private void button3_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog DecompressSelectObject1 = new FolderBrowserDialog();

            DecompressSelectObject1.ShowDialog();

            var storeFolderPath = DecompressSelectObject1.SelectedPath;

            textBox2.Text = storeFolderPath;

            OxideMod_Decompression = storeFolderPath;
        }
    }
}
