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
        private string title;
        public Form1()
        {
            InitializeComponent();
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
                    title = result.Title.Pretty;
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

        private async void DownloadBtn_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folder = new FolderBrowserDialog())
            {
                if(folder.ShowDialog() == DialogResult.OK)
                {
                    button1.Enabled = false;
                    DownloadBtn.Enabled = false;
                    progressBar1.Maximum = number_page;

                    for(int i = 1; i <= number_page; i++)
                    {
                        using (WebClient client = new WebClient())
                        {
                            client.Credentials = CredentialCache.DefaultCredentials;
                            client.DownloadFileCompleted += Completed;
                            await client.DownloadFileTaskAsync(new Uri($"https://i.nhentai.net/galleries/{media_id}/{i}.jpg"), $@"{folder.SelectedPath}\{i}.jpg");
                        }
                    }
                    
                }
            }
        }

        private void Completed(object sender, AsyncCompletedEventArgs e)
        {
            progressBar1.Value++;
            if (progressBar1.Value == progressBar1.Maximum)
            {
                MessageBox.Show("Done!");
                progressBar1.Value = 0;
                button1.Enabled = true;
                DownloadBtn.Enabled = false;
            }
        }
    }
}
