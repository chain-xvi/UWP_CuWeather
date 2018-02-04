using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using WeatherAppUnitTestDemo.Models;
using WeatherAppUnitTestDemo.Helpers;
using WeatherAppUnitTestDemo.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherAppUnitTestDemo
{

    // TODO: Add in the 10 days weather function...

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private RootObject rootObject;
        private GeolocationAccessStatus accessStatus;
        private DispatcherTimer dispatcherTimer;

        public MainPage()
        {
            this.InitializeComponent();

            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = new TimeSpan(0,0,60);
            dispatcherTimer.Start();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
        }

        private async void DispatcherTimer_Tick(object sender, object e)
        {
            try
            {
                rootObject.dateTime = DateTimeOffset.Now;
                if (InternetAvailabilityCheckHelper.IsInternetAvailable())
                {
                    UpdateWeatherControls();
                    await WeatherStorageService.SaveWeatherToFileAsync(rootObject);
                }
            }
            catch (Exception)
            {
                //Still not set to an instance.
            }

            
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            try
            {
                App.IsUnitCelcius = await WeatherStorageService.ReadTemperatureUnitFromFileAsync<bool>();
            }
            catch (Exception)
            {
                // No file yet.
            }

            if (InternetAvailabilityCheckHelper.IsInternetAvailable())
            {
                accessStatus = await Geolocator.RequestAccessAsync();

                switch (accessStatus)
                {
                    case GeolocationAccessStatus.Unspecified:
                        break;
                    case GeolocationAccessStatus.Allowed:
                        Geolocator geolocator = new Geolocator() { DesiredAccuracyInMeters = 1, MovementThreshold = 10 };
                        Geoposition geoposition = await geolocator.GetGeopositionAsync();
                        rootObject = await WebAPIServiceCall.CallWeatherAPIAsync(Convert.ToDouble(geoposition.Coordinate.Longitude), Convert.ToDouble(geoposition.Coordinate.Latitude));
                        if (rootObject != null)
                        {
                            UpdateWeatherControls();
                            await WeatherStorageService.SaveWeatherToFileAsync(rootObject);
                        }
                        break;
                    case GeolocationAccessStatus.Denied:
                        break;
                }
            }
            else
            {
                try
                {
                    rootObject = await WeatherStorageService.ReadWeatherFromFileAsync<RootObject>();
                }
                catch (Exception)
                {
                    // No file yet.
                }

                if (rootObject != null)
                {
                    UpdateWeatherControls();
                }
            }

            if (App.IsUnitCelcius)
            {
                CelciusToggleButton.IsChecked = true;
            }
            else
            {
                FahrenheitToggleButton.IsChecked = true;
            }


        }

        private void UpdateWeatherControls()
        {
            DayTextBlock.Text = GettingWeatherInformation.GetWeatherUpdateDate(rootObject).justDay;
            CityTextBlock.Text = GettingWeatherInformation.GetCity(rootObject);
            TheDegreeSymbol.Visibility = Visibility.Visible;
            //SmallerHSymbolTextBlock.Visibility = Visibility.Visible;
            //SmallerLSymbolTextBlock.Visibility = Visibility.Visible;
            //HTextBlock.Visibility = Visibility.Visible;
            //LTextBlock.Visibility = Visibility.Visible;
            TemperatureTextBlock.Text = GettingWeatherInformation.GetTemperature(rootObject);
            DescriptionTextBlock.Text = GettingWeatherInformation.GetWeatherDescription(rootObject);
            //HighTextBlock.Text = GettingWeatherInformation.GetMaxAndMin(rootObject).maxTemp;
            //LowTextBlock.Text = GettingWeatherInformation.GetMaxAndMin(rootObject).minTemp;

            BitmapImage bitmapImage = new BitmapImage
            {
                UriSource = new Uri(WeatherIconImage.BaseUri, GettingWeatherInformation.GetIconSource(rootObject))
            };
            WeatherIconImage.Source = bitmapImage;
            UpdatedTextBlock.Visibility = Visibility.Visible;
            UpdatedDateTextBlock.Text = GettingWeatherInformation.GetWeatherUpdateDate(rootObject).date;
            UpdatedTimeTextBlock.Text = GettingWeatherInformation.GetWeatherUpdateTime(rootObject);
            TemperatueToggleButtonsContainerStackPanel.Visibility = Visibility.Visible;

            LoadingProgressRing.IsActive = false;
        }

        private async void CelciusToggleButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FahrenheitToggleButton.IsChecked = false;
            CelciusToggleButton.IsChecked = true;

            App.IsUnitCelcius = true;
            UpdateWeatherControls();
            await WeatherStorageService.SaveTemperatureUnitToFileAsync(App.IsUnitCelcius);
        }

        private async void FahrenheitToggleButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            CelciusToggleButton.IsChecked = false;
            FahrenheitToggleButton.IsChecked = true;

            App.IsUnitCelcius = false;
            UpdateWeatherControls();
            await WeatherStorageService.SaveTemperatureUnitToFileAsync(App.IsUnitCelcius);
        }

        private async void CelciusToggleButton_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (CelciusToggleButton.IsChecked == false)
                FahrenheitToggleButton.IsChecked = false;

            await Task.Delay(100);
            CelciusToggleButton.IsChecked = true;
            App.IsUnitCelcius = true;
            UpdateWeatherControls();
            await WeatherStorageService.SaveTemperatureUnitToFileAsync(App.IsUnitCelcius);
        }

        private async void FahrenheitToggleButton_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (FahrenheitToggleButton.IsChecked == true)
                CelciusToggleButton.IsChecked = false;

            await Task.Delay(100);
            FahrenheitToggleButton.IsChecked = true;
            UpdateWeatherControls();
            await WeatherStorageService.SaveTemperatureUnitToFileAsync(App.IsUnitCelcius);
        }
    }
}
