﻿using Newtonsoft.Json;
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
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            ListCartInit(ProUser);
        }
        public ProductView(User user)
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            ProUser = user;
            ListCartInit(ProUser);
            OnAppearing();
        }

        List<Cart> carts = new List<Cart>();

        async void ListCartInit( User user)
        {
            if(user!=null)
            {
                HttpClient httpClient = new HttpClient();
                var CartList = await httpClient.GetStringAsync("http://172.30.8.50/AppFoodApi//api/CartController/GetCartFood?mand=" + user.MAND.ToString());
                var CartListCV = JsonConvert.DeserializeObject<List<Cart>>(CartList);
                carts = CartListCV;
               // carts.Sort((x, y) => x.TENNH.CompareTo(y.TENNH));
                float tien = 0;
                float TongTien;
                foreach (Cart x in carts)
                {
                    tien = tien + x.TONGGIA;
                }
                //Money.Text = tien.ToString();
                TongTien = tien;
                LbTongTien.Text = TongTien.ToString();
                LstCart.HeightRequest = (100 * carts.Count);
                LstCart.ItemsSource = carts;
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
                        //HttpClient httpClient = new HttpClient();
                        //await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/InsertGioHang?mand=" + ProUser.MAND.ToString()+"&mama=" +x.MAMA.ToString());
                        //break;

                        User_Food user_food = new User_Food { MAND = ProUser.MAND, MAMA = x.MAMA };
                        HttpClient http = new HttpClient();
                        string jsonUserFood = JsonConvert.SerializeObject(user_food);
                        StringContent httpContent = new StringContent(jsonUserFood, Encoding.UTF8, "application/json");
                        HttpResponseMessage result = await http.PostAsync("http://172.30.8.50/AppFoodApi/api/CartController/InsertGioHang", httpContent);
                        var code = await result.Content.ReadAsStringAsync();

                        if (Int32.Parse(code) > 0)
                        {
                            //await DisplayAlert("Thông báo", "Thêm món ăn vào giỏ hàng thành công", "OK");
                            ListCartInit(ProUser);
                        }
                        else
                        {
                            await DisplayAlert("Thông báo", "Có lỗi xảy ra, thêm món ăn KHÔNG thành công", "OK");
                        }
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
                            //HttpClient httpClient = new HttpClient();
                            //await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/DeleteGioHang?magh=" + MAGH.ToString());

                            User_Food user_food = new User_Food { MAND = ProUser.MAND, MAMA = x.MAMA };
                            HttpClient http = new HttpClient();
                            string jsonUserFood = JsonConvert.SerializeObject(user_food);
                            StringContent httpContent = new StringContent(jsonUserFood, Encoding.UTF8, "application/json");
                            HttpResponseMessage result = await http.PostAsync("http://172.30.8.50/AppFoodApi/api/CartController/DeleteGioHang", httpContent);
                            var code = await result.Content.ReadAsStringAsync();
                            if (Int32.Parse(code) > 0)
                            {
                                //await DisplayAlert("Thông báo", "Thêm món ăn vào giỏ hàng thành công", "OK");
                                ListCartInit(ProUser);
                            }
                            else if (Int32.Parse(code) == 0 )
                            {
                                await DisplayAlert("Thông báo", "Xóa sản phẩm thành công", "OK");
                                ListCartInit(ProUser);
                            }
                            else
                            {
                                await DisplayAlert("Thông báo", "Có lỗi xảy ra", "OK");
                            }
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