using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace nhentai_dl_winform.nhentai
{
    class Request
    {
        public static async Task<Nhentai> getMangaData(string id)
        {
            string baseUrl = "https://nhentai.net/api/gallery/";
            string result = "";

            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    HttpResponseMessage res = await httpClient.GetAsync(baseUrl + id);
                    res.EnsureSuccessStatusCode();
                    result = await res.Content.ReadAsStringAsync();
                } 
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return JsonConvert.DeserializeObject<Nhentai>(result);
            }
        }

        public static void GetImage(string url, PictureBox pictureBox)
        {
            try
            {
                var request = WebRequest.Create(url);

                using (var res = request.GetResponse())
                using (var stream = res.GetResponseStream())
                {
                    pictureBox.Image = Bitmap.FromStream(stream);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
