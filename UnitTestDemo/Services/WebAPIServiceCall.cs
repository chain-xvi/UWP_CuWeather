using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherAppUnitTestDemo.Models;

namespace WeatherAppUnitTestDemo.Services
{
    public class WebAPIServiceCall
    {
        internal async static Task<RootObject> CallWeatherAPIAsync(double lon, double lat) => await Task.Run(async () =>
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage message = await client.GetAsync("http://api.openweathermap.org/data/2.5/weather?lat=" + lat + "&lon=" + lon + "&appid=035a9b9c1ad756225aee214f122f2036");
                string response = await message.Content.ReadAsStringAsync();
                RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(response) as RootObject;
                rootObject.dateTime = DateTimeOffset.Now;
                return rootObject;
            }
            catch (Exception)
            {
                throw;
                //return null;
            }
        });
    }
}
