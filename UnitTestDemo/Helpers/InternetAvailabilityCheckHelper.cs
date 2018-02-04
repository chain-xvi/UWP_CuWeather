using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace WeatherAppUnitTestDemo.Helpers
{
    class InternetAvailabilityCheckHelper
    {
        internal static bool IsInternetAvailable()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            return connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
        }
    }
}
