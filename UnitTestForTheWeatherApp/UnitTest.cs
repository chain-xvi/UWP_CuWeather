using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherAppUnitTestDemo.Models;
using WeatherAppUnitTestDemo.Services;

namespace UnitTestForTheWeatherApp
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async void WeatherAPITest()
        {
            RootObject rootObject = await WebAPIServiceCall.CallWeatherAPIAsync();
            Assert.AreEqual("Région de Marrakech-Tensift-Al Haouz", rootObject.name);
        }
    }
}
