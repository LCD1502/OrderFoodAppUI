using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderFoodAppUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentPage
    {
        User HomeUser;
        public HomeView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        public HomeView(User user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            HomeUser = user;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new ProducDetail(), true);
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new CartView(), true);
        }

        private async void GetLct_Clicked(object sender, EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    { 
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }
                if (location == null)
                    LabelLocation.Text = "No GPS";
                else
                    LabelLocation.Text = $"{location.Latitude} {location.Longitude}";

                var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                var placemark = placemarks?.FirstOrDefault();

                var geocodeAdress =
               $"AdminArea:         {placemark.AdminArea}\n" +
               $"CountryCode:       {placemark.CountryCode}\n" +
               $"CountryName:       {placemark.CountryName}\n" +
               $"FeatureName:       {placemark.FeatureName}\n" +
               $"Locality:          {placemark.Locality}\n" +
               $"PostalCode:        {placemark.PostalCode}\n" +
               $"SubAdminArea:      {placemark.SubAdminArea}\n" +
               $"SubLocality:       {placemark.SubLocality}\n" +
               $"SubThoroughfare:   {placemark.SubThoroughfare}\n" +
               $"Thoroughfare:      {placemark.Thoroughfare}\n";
                LabelLocation2.Text = geocodeAdress;
            }
            catch (Exception)
            {
                await DisplayAlert("Error !!!", "Something's wrong", "Try again!");
            }
        }

        //public async void GetLocation()
        //{
        //    Location Location = await Geolocation.GetLastKnownLocationAsync();
        //    if (Location == null)
        //    {
        //        Location = await Geolocation.GetLocationAsync(new GeolocationRequest
        //        {
        //            DesiredAccuracy = GeolocationAccuracy.Medium,
        //            Timeout = TimeSpan.FromSeconds(30)
        //        }); ;
        //    }
        //    var placemarks = await Geocoding.GetPlacemarksAsync(Location.Latitude, Location.Longitude);
        //    var placemark = placemarks?.FirstOrDefault();

        //    var geocodeAdress =
        //   $"AdminArea:         {placemark.AdminArea}\n" +
        //   $"CountryCode:       {placemark.CountryCode}\n" +
        //   $"CountryName:       {placemark.CountryName}\n" +
        //   $"FeatureName:       {placemark.FeatureName}\n" +
        //   $"Locality:          {placemark.Locality}\n" +
        //   $"PostalCode:        {placemark.PostalCode}\n" +
        //   $"SubAdminArea:      {placemark.SubAdminArea}\n" +
        //   $"SubLocality:       {placemark.SubLocality}\n" +
        //   $"SubThoroughfare:   {placemark.SubThoroughfare}\n" +
        //   $"Thoroughfare:      {placemark.Thoroughfare}\n";
        //    LabelLocation2.Text = geocodeAdress;
        //}
    }
}