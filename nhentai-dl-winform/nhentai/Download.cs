using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace nhentai_dl_winform.nhentai
{
    class Download
    {
        public static async Task DownloadImg(string url, string path, int sizebuffer = 500)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string[] urlParams = url.Split(new char[] { '/' });
                string fileName = urlParams[urlParams.Length-1];
                
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    using Stream stream = await response.Content.ReadAsStreamAsync();
                    using FileStream streamWrite = File.OpenWrite($@"{path}\{fileName}");
                    byte[] buffer = new byte[sizebuffer];

                    bool end = false;
                    do
                    {
                        int numRead = await stream.ReadAsync(buffer, 0, sizebuffer);
                        if (numRead == 0)
                        {
                            end = true;
                        }
                        else
                        {
                            await streamWrite.WriteAsync(buffer, 0, numRead);
                        }
                    } while (!end);
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
