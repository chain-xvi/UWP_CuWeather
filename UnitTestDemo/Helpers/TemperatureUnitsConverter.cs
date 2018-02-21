using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAppUnitTestDemo.Helpers
{
    class TemperatureUnitsConverter
    {
        internal static double GetTemperatureBasedOnUnit(double temp)
        {
            if (App.IsUnitCelcius)
                return temp;
            else
                return Math.Truncate(temp * 1.8 + 32);
        }
    }
}
