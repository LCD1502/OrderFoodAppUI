using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            //Placeinit();
        }

        public HomeView(User user)
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            //await Placeinit();
            HomeUser = user;
            Hello.Text = "Xin chào " + user.HOTEN;
            InfoInit(user);


        }
        List<Restaurant> restaurants = new List<Restaurant>();
        async void InfoInit(User user)
        {
            HttpClient httpClient = new HttpClient();
            var ResList = await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/GetNhaHang");
            var RestListCV = JsonConvert.DeserializeObject<List<Restaurant>>(ResList);
            restaurants = RestListCV;
            Hinh1.Source = restaurants[0].IMG;
            Hinh2.Source = restaurants[1].IMG;
            Hinh3.Source = restaurants[2].IMG;
            Hinh4.Source = restaurants[3].IMG;
            Hinh5.Source = restaurants[4].IMG;

            Ten1.Text = restaurants[0].TEN;
            Ten2.Text = restaurants[1].TEN;
            Ten3.Text = restaurants[2].TEN;
            Ten4.Text = restaurants[3].TEN;
            Ten5.Text = restaurants[4].TEN;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new RestaurantDetail(restaurants[0], HomeUser), true);
        }
        private void TapGestureRecognizer_Tapped2(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new RestaurantDetail(restaurants[1], HomeUser), true);
        }

        private void TapGestureRecognizer_Tapped3(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new RestaurantDetail(restaurants[2], HomeUser), true);
        }

        private void TapGestureRecognizer_Tapped4(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new RestaurantDetail(restaurants[3], HomeUser), true);
        }

        private void TapGestureRecognizer_Tapped5(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new RestaurantDetail(restaurants[4], HomeUser), true);
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new CartView(), true);
        }


        private async void Placeinit()
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
                {
                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();
                    var Provine = placemark.AdminArea;
                    var District = placemark.SubAdminArea;
                    var SubAdd1 = placemark.FeatureName;
                    var SubAdd2 = placemark.Thoroughfare;
                    var tempaddress = SubAdd2 + ", " + SubAdd1 + ", " + District + ", " + Provine;
                    LabelLocation.Text = tempaddress;
                }    
            }
            catch (Exception)
            {
                await DisplayAlert("Error !!!", "Something's wrong", "Try again!");
            }
        }

        private async void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
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
                {
                    var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
                    var placemark = placemarks?.FirstOrDefault();
                    var Provine = placemark.AdminArea;
                    var District = placemark.SubAdminArea;
                    var SubAdd1 = placemark.FeatureName;
                    var SubAdd2 = placemark.Thoroughfare;
                    var tempaddress = SubAdd2 + ", " + SubAdd1 + ", " + District + ", " + Provine;
                    LabelLocation.Text = tempaddress;
                }
            }
            catch (Exception)
            {
                await DisplayAlert("Error !!!", "Something's wrong", "Try again!");
            }
        }
        //private async void GetLct_Clicked(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        var location = await Geolocation.GetLastKnownLocationAsync();
        //        if (location == null)
        //        {
        //            location = await Geolocation.GetLocationAsync(new GeolocationRequest
        //            { 
        //                DesiredAccuracy = GeolocationAccuracy.Medium,
        //                Timeout = TimeSpan.FromSeconds(30)
        //            });
        //        }
        //        if (location == null)
        //            LabelLocation.Text = "No GPS";
        //        else
        //            LabelLocation.Text = $"{location.Latitude} {location.Longitude}";

        //        var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
        //        var placemark = placemarks?.FirstOrDefault();
        //        var Provine = placemark.AdminArea;
        //        var District = placemark.SubAdminArea;
        //        var SubAdd1 = placemark.FeatureName;
        //        var SubAdd2 = placemark.Thoroughfare;
        //        string address;
        //        //if (SubAdd2=="")

        //        var tempaddress = SubAdd2 + ", " + SubAdd1 + ", " + District + ", " + Provine;

        //        LabelLocation.Text = tempaddress;
        //       // var geocodeAdress =
        //       //$"AdminArea:         {placemark.AdminArea}\n" +
        //       //$"CountryCode:       {placemark.CountryCode}\n" +
        //       //$"CountryName:       {placemark.CountryName}\n" +
        //       //$"FeatureName:       {placemark.FeatureName}\n" +
        //       //$"Locality:          {placemark.Locality}\n" +
        //       //$"PostalCode:        {placemark.PostalCode}\n" +
        //       //$"SubAdminArea:      {placemark.SubAdminArea}\n" +
        //       //$"SubLocality:       {placemark.SubLocality}\n" +
        //       //$"SubThoroughfare:   {placemark.SubThoroughfare}\n" +
        //       //$"Thoroughfare:      {placemark.Thoroughfare}\n";
        //       // LabelLocation2.Text = geocodeAdress;
        //    }
        //    catch (Exception)
        //    {
        //        await DisplayAlert("Error !!!", "Something's wrong", "Try again!");
        //    }
        //}




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