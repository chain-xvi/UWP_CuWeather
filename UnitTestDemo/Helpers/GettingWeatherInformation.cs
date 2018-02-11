using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherAppUnitTestDemo.Models;

namespace WeatherAppUnitTestDemo.Helpers
{
    class GettingWeatherInformation
    {
        internal static string GetCity(RootObject rootObject) => rootObject.name;

        internal static string GetTemperature(RootObject rootObject) =>
            TemperatureUnitsConverter.GetTemperatureBasedOnUnit(Math.Truncate(rootObject.main.temp)).ToString();

        internal static string GetWeatherDescription(RootObject rootObject) => rootObject.weather[0].description;

        internal static (string maxTemp, string minTemp) GetMaxAndMin(RootObject rootObject) =>
            (TemperatureUnitsConverter.GetTemperatureBasedOnUnit(rootObject.main.temp).ToString(), (rootObject.main.temp_min - 273.15).ToString());

        internal static string GetIconSource(RootObject rootObject)
        {
            switch (rootObject.weather[0].main)
            {
                case "Thunderstorm":
                    return "Assets/WeatherIcons/Weather-Storm-icon[2].png";
                case "Drizzle":
                    return "Assets/WeatherIcons/Weather-Little-Rain-icon[3].png";
                case "Rain":
                    return "Assets/WeatherIcons/Weather-Partly-Cloudy-Rain-icon[2].png";
                case "Snow":
                    return "Assets/WeatherIcons/Weather-Snow-icon[2].png";
                case "Atmosphere":
                case "Clear":
                    if (rootObject.weather[0].icon.Contains("d"))
                        return "Assets/WeatherIcons/Weather-Sun-icon[1].png";
                    else
                        return "Assets/WeatherIcons/Weather-Moon-icon[2].png";
                case "Clouds":
                    if (rootObject.weather[0].description == "few clouds")
                    {
                        if (rootObject.weather[0].icon.Contains("d"))
                            return "Assets/WeatherIcons/Weather-Partly-Cloudy-Day-icon[2].png";
                        else
                            return "Assets/WeatherIcons/Weather-Partly-Cloudy-Night-icon[2].png";
                    }
                    else
                        return "Assets/WeatherIcons/clouds_weather_cloud_4496[1].png";
                default:
                    return "Assets/WeatherIcons/Weather-Storm-icon[2].png";
            }
        }

        internal static (string date, string justDay) GetWeatherUpdateDate(RootObject rootObject) =>
            (rootObject.dateTime.Date.ToString("MM/dd/yyyy"), rootObject.dateTime.Date.DayOfWeek.ToString().ToUpper());

        internal static string GetWeatherUpdateTime(RootObject rootObject) => rootObject.dateTime.DateTime.ToString("HH:mm");

        internal static void ConvertFiveDaysTemperatureToCelsius(List<DailyWeather> collection) => Parallel.ForEach(collection, (item)=> {
            item.High = TemperatureUnitsConverter.GetTemperatureBasedOnUnit(item.High);
        });
    }
}
