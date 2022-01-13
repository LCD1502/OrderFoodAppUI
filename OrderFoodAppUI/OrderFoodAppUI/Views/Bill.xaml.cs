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
    public partial class Bill : ContentPage
    {
        User BillUser;
        public Bill()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
        List<Cart> carts = new List<Cart>();
        public Bill(List<Cart> cart,User user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            carts = cart;
            BillUser = user;
            BillInit(carts, BillUser);
        }

        private static double DegreesToRadians(double degrees) //ham doi do sang rad
        {
            return degrees * Math.PI / 180.0;
        }

        public static double CalculateDistance(Location location1, Location location2) //ham tinh khoang cach 2 toa do gps
        {
            double circumference = 40000.0; // Earth's circumference at the equator in km
            double distance = 0.0;

            double latitude1Rad = DegreesToRadians(location1.Latitude);
            double longitude1Rad = DegreesToRadians(location1.Longitude);
            double latititude2Rad = DegreesToRadians(location2.Latitude);
            double longitude2Rad = DegreesToRadians(location2.Longitude);
            double logitudeDiff = Math.Abs(longitude1Rad - longitude2Rad);

            if (logitudeDiff > Math.PI)
            {
                logitudeDiff = 2.0 * Math.PI - logitudeDiff;
            }

            double angleCalculation =
                Math.Acos(
                  Math.Sin(latititude2Rad) * Math.Sin(latitude1Rad) +
                  Math.Cos(latititude2Rad) * Math.Cos(latitude1Rad) * Math.Cos(logitudeDiff));

            distance = circumference * angleCalculation / (2.0 * Math.PI);

            distance = Math.Round(distance, 1);
            return distance;
        }
        async void BillInit(List<Cart> cart, User user)
        {
            //khoi tao cho UI
            USname.Text = user.HOTEN;
            Time.Text = DateTime.Now.ToString();

            //Tinh tong gia tien
            float tien=0;
            foreach (Cart x in cart)
            {
                tien = tien + x.TONGGIA;
            }
            Money.Text =tien.ToString();
            Ship.Text = "20000";
            Total.Text = (tien + 20000).ToString();

            //lay vitri hien tai
            var location = await Geolocation.GetLastKnownLocationAsync();
            var location2 = await Geolocation.GetLastKnownLocationAsync();

            if (location == null)
            {
                location = await Geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy = GeolocationAccuracy.Medium,
                    Timeout = TimeSpan.FromSeconds(30)
                });
            }
            if (location == null)
                MyLocation.Text = "No GPS";
            else
                MyLocation.Text = $"{location.Latitude} {location.Longitude}";

            var placemarks = await Geocoding.GetPlacemarksAsync(location.Latitude, location.Longitude);
            var placemark = placemarks?.FirstOrDefault();

            var Provine = placemark.AdminArea;
            var District = placemark.SubAdminArea;
            var SubAdd1 = placemark.FeatureName;
            var SubAdd2 = placemark.Thoroughfare;
                var tempaddress = SubAdd2 + ", " + SubAdd1 + ", " + District + ", " + Provine;       
            MyLocation.Text = tempaddress;

            //location2.Latitude = 13.05157847078659;
            //location2.Longitude = 109.34687624119483;
            //double distance = CalculateDistance(location, location2);
            //USname.Text = distance.ToString();
        }

        private void BtnLater_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopAsync(true);
        }

        private async void BtnOrder_Clicked(object sender, EventArgs e)
        {
            DateTime time1 = DateTime.Now;
            DateTime time2 = time1.AddMinutes(15);//15 phút sau
            HttpClient httpClient = new HttpClient();

            string tgdat = time1.ToString("MM/dd/yyyy HH:mm:ss"); // chuyen doi thoi gian dat hop le
            tgdat=tgdat.Replace("SA", "AM");
            tgdat = tgdat.Replace("CH", "PM");

            string tggiao = time1.ToString("MM/dd/yyyy HH:mm:ss"); // chuyen doi thoi gian giao hop le 15 phut sau
            tggiao = tggiao.Replace("SA", "AM");
            tggiao = tggiao.Replace("CH", "PM");

            //goi api them vao hoa don
            var MAHD = await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/InsertHoaDon?mand="+BillUser.MAND.ToString()+"&tongtien="+50000.ToString()+"&tgdat="+tgdat+"&tggiao="+tggiao+"&ship="+Ship.Text);
            MAHD = MAHD.Replace("[{\"MAHD\":", string.Empty); //xu li ma hoa don nhan ve
            MAHD = MAHD.Replace(".0}]", string.Empty);
            
            //them vao trang chi tiet hoa don
            foreach (Cart x in carts)
            {
                var aaa = await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/InsertCTHoaDon?mahd="+MAHD+"&mama="+x.MAMA+"&soluong="+x.SOLUONG);
            }

            await DisplayAlert("Thông báo", "Đặt hàng thành công\nThời gian giao hàng dự kiến: " + time2.ToString() + "\nMã Hóa đơn: " + MAHD, "OK");
            App.Current.MainPage.Navigation.PopAsync(true);
        }
    }
}