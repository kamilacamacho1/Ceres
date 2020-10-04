using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace GoogleMaps.Method
{
    public class ConexionData
    {
        public class LocateP 
        {
            [JsonProperty("Latitude")]
            public string lat { get; set; }
            [JsonProperty("Longitude")]
            public string lon { get; set; }
        }
        
        public string read_file(string animal)
        {
            List<LocateP> locate ;
            string url = "";
            if (animal == "Morsa")
            {
                url = "https://firebasestorage.googleapis.com/v0/b/forward-rarity-203200.appspot.com/o/Morsa.json?alt=media&token=f4db7800-378f-48f3-97ca-0f80d0fe64a5";
            }
            if(animal == "GansoCareto")
            {
                url = "https://firebasestorage.googleapis.com/v0/b/forward-rarity-203200.appspot.com/o/gansoCareto.json?alt=media&token=4fc5c7ac-11bf-4c06-b0a1-a4cb8d03ea13";
            }
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                locate = JsonConvert.DeserializeObject<List<LocateP>>(json);
                var lat = locate;
            }
            return locate[0].lat + "/" + locate[0].lon;
        }
    }
}
