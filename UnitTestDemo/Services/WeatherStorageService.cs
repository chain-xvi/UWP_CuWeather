using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using WeatherAppUnitTestDemo.Models;
using Windows.Storage;

namespace WeatherAppUnitTestDemo.Services
{
    class WeatherStorageService
    {
        private static StorageFolder localStorageFolder = ApplicationData.Current.LocalFolder;

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

        internal async static Task<T> ReadWeatherFromFileAsync<T>()
        {
            StorageFile storageFile = await localStorageFolder.GetFileAsync("WeatherData.json");
            string weatherAsJson = await FileIO.ReadTextAsync(storageFile);
            return GetTFromJson<T>(weatherAsJson);
        }

        internal async static Task SaveTemperatureUnitToFileAsync(bool isCelcius)
        {
            await Task.Run(async () =>
            {
                StorageFile storageFile = await localStorageFolder.CreateFileAsync("TempData.json", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(storageFile, SerializeToJson(isCelcius));
            });
            await Task.CompletedTask;
        }

        internal async static Task<T> ReadTemperatureUnitFromFileAsync<T>()
        {
            StorageFile storageFile = await localStorageFolder.GetFileAsync("TempData.json");
            string weatherAsJson = await FileIO.ReadTextAsync(storageFile);
            return GetTFromJson<T>(weatherAsJson);
        }

        internal async static Task SaveFiveDaysWeatherAsync(FiveDaysWeatherRootObject fiveDaysWeatherRootObject)
        {
            await Task.Run(async() => {
                StorageFile storageFile = await localStorageFolder.CreateFileAsync("FiveDays.json", CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(storageFile, SerializeToJson(fiveDaysWeatherRootObject));
            });
            await Task.CompletedTask;
        }

        internal async static Task<T> ReadFiveDaysWeatherAsync<T>()
        {
            StorageFile storageFile = await localStorageFolder.GetFileAsync("FiveDays.json");
            string weatherAsJson = await FileIO.ReadTextAsync(storageFile);
            return GetTFromJson<T>(weatherAsJson);
        }

        private static string SerializeToJson(object o)
        {
            if (o == null)
                return null;
            else
                return JsonConvert.SerializeObject(o);
        }

        private static T GetTFromJson<T>(string jsonString) => 
            JsonConvert.DeserializeObject<T>(jsonString);

    }
}
