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
using System.IO;

namespace RustOxideDownloadFunction
{
    public partial class Form1 : Form
    {
        private string OxideMod_Location;
        private string OxideMod_Decompression;
        private const string OxideDownload = "https://raw.githubusercontent.com/OxideMod/Snapshots/master/Oxide-Rust.zip";
        private string fileDelete;

        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            // Progress bar for downloading of file.
        }

        private void Download_btn_Click(object sender, EventArgs e)
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
        private void Download_Location_btn_Click(object sender, EventArgs e)
        {
            SaveFileDialog SelectFolderObject1 = new SaveFileDialog();

            SelectFolderObject1.FileName = "Oxide-Rust.zip";

            SelectFolderObject1.Filter = "Zip Files|*.zip|All Files (*.*)|*.*";

            SelectFolderObject1.ShowDialog();

            var storeFolderPath = SelectFolderObject1.FileName;

            textBox1.Text = storeFolderPath;

            OxideMod_Location = storeFolderPath;

            // Write variable string to settings variable, this is to store it to be read from form load so user settings are saved on close from previous use/save.
            Properties.Settings.Default.download_Location = storeFolderPath;

            Properties.Settings.Default.Save();

        }

        private void DecompressOxideMod()
        {
            ZipFile zipdecompression = ZipFile.Read(OxideMod_Location);

            foreach (ZipEntry zipentry in zipdecompression)
            {
                zipentry.Extract(OxideMod_Decompression, ExtractExistingFileAction.OverwriteSilently);
            }

            MessageBox.Show("Download and extraction complete", "Notification");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Loads the string variable whats been written to when the user
            string load1 = Properties.Settings.Default.download_Location;

            string load2 = Properties.Settings.Default.extract_Location;

            string load3 = Properties.Settings.Default.file_Wipe_Location;

            for (int startnum = 0; startnum <=1; startnum++)
            {
                OxideMod_Location = load1;

                OxideMod_Decompression = load2;

                fileDelete = load3;

                textBox1.Text = load1;

                textBox2.Text = load2;

                textBox3.Text = load3;
            }
           


        }


        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        // Allows the user to select extraction location.
        private void Extract_Location_btn_click(object sender, EventArgs e)
        {
            FolderBrowserDialog DecompressSelectObject1 = new FolderBrowserDialog();

            DecompressSelectObject1.ShowDialog();

            var storeFolderPath = DecompressSelectObject1.SelectedPath;

            textBox2.Text = storeFolderPath;

            OxideMod_Decompression = storeFolderPath;

            Properties.Settings.Default.extract_Location = storeFolderPath;

            Properties.Settings.Default.Save();


        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button_Delete_Click(object sender, EventArgs e)
        {
            DialogResult deleteDialog = MessageBox.Show("This will permanently delete your rust save file, are you sure", "  WARNING", MessageBoxButtons.YesNo);
            if(deleteDialog == DialogResult.Yes)
            {
                File.Delete(fileDelete);
            }
            else
            {
                // Do nothing. Does not delete file.
            }
            
        }

        private void button_browse_delete_Click(object sender, EventArgs e)
        {
            OpenFileDialog oFDObject = new OpenFileDialog();

            oFDObject.ShowDialog();

            var test = oFDObject.FileName;

            textBox3.Text = test;

            fileDelete = test;

            Properties.Settings.Default.file_Wipe_Location = test;

            Properties.Settings.Default.Save();


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
             
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
    }
}
