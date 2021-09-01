using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nhentai_dl_winform
{
    using nhentai_dl_winform.nhentai;
    using System.Net;

    public partial class Form1 : Form
    {
        private string media_id;
        private int number_page;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private async void button1_ClickAsync(object sender, EventArgs e)
        {
            string id = IdInput.Text;
            if(id != "")
            {
                try
                {
                    button1.Enabled = false;
                    editTextLog($"Get Data Manga With ID: {id}", logText);
                    Nhentai result = await Request.getMangaData(id);
                    editTextLog($"Done!", logText);
                    editTextLog($"Title: {result.Title.Pretty}" +
                        $"\nID: {result.Id}" +
                        $"\nMedia ID: {result.Media_id}" +
                        $"\nChapters: {result.Num_pages}", richTextBox1);
                    Request.GetImage($"https://t.nhentai.net/galleries/{result.Media_id}/cover.jpg", pictureBox1);
                    media_id = result.Media_id;
                    number_page = result.Num_pages;
                    button1.Enabled = true;
                    DownloadBtn.Enabled = true;
                }
                catch(Exception ex)
                {
                    DownloadBtn.Enabled = false;
                    button1.Enabled = true;
                    editTextLog($"Failed!", logText);
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                editTextLog($"{id} Invaild", logText);
            }
        }

        private static void editTextLog(string message, RichTextBox richTextBox)
        {
            richTextBox.Text = $"{message}";
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private async void DownloadBtn_Click(object sender, EventArgs e)
        {
            progressBar1.Minimum = 0;
            progressBar1.Maximum = number_page;
            FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
            if(folderBrowser.ShowDialog() == DialogResult.OK)
            {
                DownloadBtn.Enabled = false;
                button1.Enabled = false;
                for(int i = 0; i < number_page; i++)
                {
                    await Download.DownloadImg($"https://i.nhentai.net/galleries/{media_id}/{i + 1}.jpg", folderBrowser.SelectedPath);
                    progressBar1.Value++;
                }

                button1.Enabled = true;
                MessageBox.Show($"Manga {media_id} with {number_page} pages\nDone!");
                progressBar1.Value = 0;
            } else
            {
                MessageBox.Show("Folder Invaild!");
            }
        }
    }
}
