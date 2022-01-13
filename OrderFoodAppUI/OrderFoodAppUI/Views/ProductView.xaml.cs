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
            ListCartInit(ProUser);
        }
        public ProductView(User user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            ProUser = user;
            ListCartInit(ProUser);
        }

        List<Cart> carts = new List<Cart>();

        async void ListCartInit( User user)
        {
            if(user!=null)
            {
                HttpClient httpClient = new HttpClient();
                var CartList = await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/GetGioHang?mand=" + user.MAND.ToString());
                var CartListCV = JsonConvert.DeserializeObject<List<Cart>>(CartList);
                carts = CartListCV;
                float tien = 0;
                float TongTien;
                foreach (Cart x in carts)
                {
                    tien = tien + x.TONGGIA;
                }
                Money.Text = tien.ToString();
                TongTien = tien + 20000;
                LbTongTien.Text = TongTien.ToString();
                LstCart.ItemsSource = CartListCV;
            }    
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

        private void btnThanhToan_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new Bill(carts,ProUser), true);
        }

        private async void stepper_Clicked(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            int MAGH = int.Parse(button.CommandParameter.ToString());
            string text = button.Text.ToString();
            if (text == "+")
            {
                
                foreach(Cart x in carts)
                {
                    if(x.MAGH == MAGH)
                    {
                        HttpClient httpClient = new HttpClient();
                        await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/InsertGioHang?mand=" + ProUser.MAND.ToString()+"&mama=" +x.MAMA.ToString());
                        break;
                   }      
                }    
            }
            else {
               foreach (Cart x in carts)
               {
                    if (x.MAGH == MAGH)
                   {
                        if (x.SOLUONG > 0)
                        {
                            HttpClient httpClient = new HttpClient();
                            await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/DeleteGioHang?magh=" + MAGH.ToString());
                            break;
                        }    
                            
                    }
                }
            }
            ListCartInit(ProUser);
        }

        private void BtnRefresh_Clicked(object sender, EventArgs e)
        {
             ListCartInit(ProUser);
        }
    }
}