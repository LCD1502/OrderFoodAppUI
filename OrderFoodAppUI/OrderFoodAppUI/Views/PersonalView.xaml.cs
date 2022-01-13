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
    public partial class PersonalView : ContentPage
    {
        User PersonalUser;
        public PersonalView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        public PersonalView(User user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            PersonalUser = user;
            PersonInit(user);
        }

        void PersonInit(User user)
        {
            if(user.SDT==null)
            {
                if (user.EMAIL == null)
                {     
                    lbName.Text = user.HOTEN;
                lbSDT.Text = "Số điện thoại: " + "đang cập nhật";
                lbEmail.Text = "Email: " + "đang cập nhật";
                }
                else
                {
                    lbName.Text = user.HOTEN;
                    lbSDT.Text = "Số điện thoại: " + "đang cập nhật";
                    lbEmail.Text = "Email: " + user.EMAIL;
                }    
            }    
            else
            {
                if (user.EMAIL == null)
                {
                    lbName.Text = user.HOTEN;
                    lbSDT.Text = "Số điện thoại: " + user.SDT;
                    lbEmail.Text = "Email: " + "đang cập nhật"; 
                }
                else
                {
                    lbName.Text = user.HOTEN;
                    lbSDT.Text = "Số điện thoại: " + user.SDT;
                    lbEmail.Text = "Email: " + user.EMAIL;
                }
            }    
            
        }    
        private void btnHistory_Clicked(object sender, EventArgs e)
        {
                App.Current.MainPage.Navigation.PushAsync(new HistoryView(), true);
        }
        private void BtnLogout_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new LoginView(), true);
        }
    }
}