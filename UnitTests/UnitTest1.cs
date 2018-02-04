using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
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
