using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderFoodAppUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductView : ContentPage
    {
        User ProUser;
        public ProductView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            //ListCartInit();
        }
        public ProductView(User user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            ProUser = user;
            ListCartInit(ProUser);
        }

        async void ListCartInit( User user)
        {
            HttpClient httpClient = new HttpClient();
            var CartList = await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/GetGioHang?mand="+user.MAND.ToString());
            var CartListCV = JsonConvert.DeserializeObject<List<Cart>>(CartList);

            LstCart.ItemsSource = CartListCV;
        }

        //private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        //{
        //    grFilter.TranslationX = 200;
        //    grFilter.Opacity = 0;
        //    grFilter.IsVisible = true;

        //    grFilter.FadeTo(1, 400);
        //    grFilter.TranslateTo(0, 0, 700, Easing.SinInOut);
        //}

        //private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        //{
        //    grFilter.TranslationX = 0;
        //    grFilter.Opacity = 1;

        //    await grFilter.TranslateTo(400, 0, 900, Easing.SinInOut);
        //    await grFilter.FadeTo(0, 100);


        //    grFilter.IsVisible = false;
        //}

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new ProducDetail(), true);
        }

        private async void Stepper_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            HttpClient httpClient = new HttpClient();
            await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/DeleteGioHang?magh="+1);
            var CartList = await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/GetGioHang?mand=1");
            var CartListCV = JsonConvert.DeserializeObject<List<Cart>>(CartList);
            LstCart.ItemsSource = CartListCV;
        }

        private void btnThanhToan_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Bill());
        }
    }
}