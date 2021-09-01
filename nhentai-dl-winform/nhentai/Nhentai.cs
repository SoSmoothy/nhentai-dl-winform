using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace nhentai_dl_winform.nhentai
{
    class Title
    {
        [JsonProperty("english")]
        public string English { get; set; }
        
        [JsonProperty("japanese")]
        public string Japanese { get; set; }

        [JsonProperty("pretty")]
        public string Pretty { get; set; }
    }

    class Cover
    {
        [JsonProperty("t")]
        public string T { get; set; }

        [JsonProperty("w")]
        public string W { get; set; }

        [JsonProperty("h")]
        public string H { get; set; }
    }

    class Image
    {
        [JsonProperty("pages")]
        public Cover[] Pages { get; set; }

        [JsonProperty("cover")]
        public Cover Cover { get; set; }

        [JsonProperty("thumbnail")]
        public Cover Thumbnail { get; set; }
    }

    class Tag
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }

    class Nhentai
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("media_id")]
        public string Media_id { get; set; }

        [JsonProperty("scanlator")]
        public string Scanlator { get; set; }

        [JsonProperty("num_pages")]
        public int Num_pages { get; set; }

        [JsonProperty("num_favorites")]
        public int Num_favorites { get; set; }

        [JsonProperty("title")]
        public Title Title { get; set; }

        [JsonProperty("images")]
        public Image Images { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

    }
}
