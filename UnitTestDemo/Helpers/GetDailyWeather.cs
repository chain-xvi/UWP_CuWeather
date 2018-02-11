using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAppUnitTestDemo.Models;

namespace WeatherAppUnitTestDemo.Helpers
{
    class GetDailyWeather
    {
        internal async static Task<List<DailyWeather>> GetDailyWeatherAsync(FiveDaysWeatherRootObject fiveDaysWeatherRootObject) => await Task.Run(() =>
        {
            List<DailyWeather> dailyWeather = new List<DailyWeather>();

            foreach (var item in fiveDaysWeatherRootObject.List)
            {

                DailyWeather daily = new DailyWeather();


                // if the list is empty add in it,

                // if already got a value, check if the day has changed,

                if (dailyWeather.Count != 0)
                {
                    if (dailyWeather.Last().Day != Convert.ToDateTime(item.DtTxt).ToString("dddd"))
                    {
                        daily.Day = Convert.ToDateTime(item.DtTxt).ToString("dddd");
                        daily.High = item.Main.temp_max;
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

                    dailyWeather.Add(daily);
                }
            }


            dailyWeather.Remove(dailyWeather.First(d => d.Day == DateTime.Now.ToString("dddd")));

            return dailyWeather;
        });


        //private List<double> GetHighestTemperatureValue(List<DailyWeather> list)
        //{
        //    foreach (var item in list)
        //    {

        //    }
        //}
    }
}
