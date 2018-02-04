using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Windows.Storage;
using WeatherAppUnitTestDemo.Models;

namespace BackgroundWeatherRetrieving
{
    public sealed class SaveWeather
    {
        private static StorageFolder localStorageFolder = ApplicationData.Current.LocalFolder;

        private static string ObjectToJsonString(object o) => JsonConvert.SerializeObject(o);

        private static readonly JsonSerializerSettings settings = new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented
        };

        internal async static Task SaveWeatherToFileAsync(RootObject rootObject)
        {
            await Task.Run(async () =>
            {
                StorageFile storageFile = await localStorageFolder.CreateFileAsync("WeatherData.json", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(storageFile, SerializeToJson(rootObject));
            });
            await Task.CompletedTask;
        }
    }
}
