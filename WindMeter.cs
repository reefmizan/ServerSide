using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
namespace WpfClientReef
{
    public class WindManager
    {

        private static readonly HttpClient httpClient = new HttpClient();
        private const string apiKey = "681fc76b735714b3ef210819be2c1782";


        public static string[] getWindSpeed(string cityName)
        {
            try
            {
                string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}&units=metric";

                var response = httpClient.GetStringAsync(apiUrl).Result;
                var formattedResponseMain = JObject.Parse(response);
                var formattedResponseWindSpeed = formattedResponseMain["wind"]["speed"];
                var formattedResponseWindDirection = formattedResponseMain["wind"]["deg"];

                string[] result = { formattedResponseWindSpeed.ToString(), formattedResponseWindDirection.ToString() };
                return result;
            }
            catch { return null; }
        }
    }

    
}