using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace WeatherAppUnitTestDemo.Services
{
    class CoordsStorageService
    {
        private static StorageFolder storageFolder = ApplicationData.Current.LocalFolder;

        private static string SerializeToJson(object o) => JsonConvert.SerializeObject(o);

        internal static async Task StoreCoords((double lon, double lat) coords) => await Task.Run(async () =>
        {
            string jsonToStore = SerializeToJson(null);
            StorageFile file = await storageFolder.CreateFileAsync("coords.json", CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(file, jsonToStore);
        });
    }
}
