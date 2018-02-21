using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAppUnitTestDemo.Models;

namespace WeatherAppUnitTestDemo.Helpers
{
    class GettingDailyWeather
    {
        internal async static Task<List<DailyWeather>> GetDailyWeatherAsync(FiveDaysWeatherRootObject fiveDaysWeatherRootObject) => await Task.Run(async () =>
        {
            List<DailyWeather> dailyWeather = new List<DailyWeather>();

            foreach (var item in fiveDaysWeatherRootObject.List)
            {

                DailyWeather daily = new DailyWeather();

                if (dailyWeather.Count != 0)
                {
                    if (dailyWeather.Last().Day != Convert.ToDateTime(item.DtTxt).ToString("dddd"))
                    {
                        daily.Day = Convert.ToDateTime(item.DtTxt).ToString("dddd");
                        daily.High = item.Main.temp_max;



                        daily.ImageUrl = await GetImageURL(item.Weather[0].Description, item.Weather[0].Icon);


                        dailyWeather.Add(daily);
                    }
                    else
                    {
                        if (item.Main.temp_max > dailyWeather.Last().High)
                        {
                            dailyWeather.Last().High = item.Main.temp_max;
                        }
                    }
                }
                else
                {
                    daily.Day = Convert.ToDateTime(item.DtTxt).ToString("dddd");
                    daily.High = item.Main.temp_max;


                    daily.ImageUrl = await GetImageURL(item.Weather[0].Description, item.Weather[0].Icon);

                    dailyWeather.Add(daily);
                }






            }


            // Get five days not four

            if (dailyWeather.FirstOrDefault(d => d.Day == DateTime.Now.ToString("dddd")) != null)
            {
                dailyWeather.Remove(dailyWeather.First(d => d.Day == DateTime.Now.ToString("dddd")));
            }

            Parallel.ForEach(dailyWeather, (item) => { item.High -= 273; });


            // Assign images



            return dailyWeather;
        });



        // Get the descriptions available for the five day 
        private static async Task<string> GetImageURL(string description, string icon) =>
            await Task.Run(() =>
            {
                switch (description)
                {
                    case "thunderstorm":
                        return "Assets/WeatherIcons/Weather-Storm-icon[2].png";
                    case "shower rain":
                        return "Assets/WeatherIcons/Weather-Little-Rain-icon[3].png";
                    case "rain":
                        return "Assets/WeatherIcons/Weather-Partly-Cloudy-Rain-icon[2].png";
                    case "snow":
                        return "Assets/WeatherIcons/Weather-Snow-icon[2].png";
                    case "Atmosphere":
                    case "clear sky":
                        if (icon.Contains("d"))
                            return "Assets/WeatherIcons/Weather-Sun-icon[1].png";
                        else
                            return "Assets/WeatherIcons/Weather-Moon-icon[2].png";
                    case "clouds":
                    case "few clouds":
                    case "scattered clouds":
                    case "broken clouds":
                    case "mist":
                        if (description == "few clouds")
                        {
                            if (icon.Contains("d"))
                                return "Assets/WeatherIcons/Weather-Partly-Cloudy-Day-icon[2].png";
                            else
                                return "Assets/WeatherIcons/Weather-Partly-Cloudy-Night-icon[2].png";
                        }
                        else
                            return "Assets/WeatherIcons/clouds_weather_cloud_4496[1].png";
                    default:
                        return "Assets/WeatherIcons/Weather-Storm-icon[2].png";
                }
            });

        //private List<double> GetHighestTemperatureValue(List<DailyWeather> list)
        //{
        //    foreach (var item in list)
        //    {

        //    }
        //}
    }
}
