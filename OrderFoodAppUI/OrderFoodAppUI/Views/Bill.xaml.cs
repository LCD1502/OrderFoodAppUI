using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        void BillInit(List<Cart> cart, User user)
        {
            USname.Text = user.HOTEN;
            Time.Text = DateTime.Now.ToString();
            float tien=0;
            foreach (Cart x in cart)
            {
                tien = tien + x.TONGGIA;
            }
            Money.Text =tien.ToString();
            Ship.Text = "20000";
            Total.Text = (tien + 20000).ToString();
        }

        private void BtnLater_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopAsync(true);
        }

        private void BtnOrder_Clicked(object sender, EventArgs e)
        {

        }
    }
}