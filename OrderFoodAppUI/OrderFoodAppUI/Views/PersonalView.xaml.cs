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
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        public PersonalView(User user)
        {
            NavigationPage.SetHasBackButton(this, false);
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
                }
                else
                {
                    lbName.Text = user.HOTEN;
                    lbEmail.Text =user.EMAIL;
                }    
            }    
            else
            {
                if (user.EMAIL == null)
                {
                    lbName.Text = user.HOTEN;
                    lbSDT.Text = user.SDT;
                }
                else
                {
                    lbName.Text = user.HOTEN;
                    lbSDT.Text =  user.SDT;
                    lbEmail.Text = user.EMAIL;
                }
            }     
        }    
        private void btnHistory_Clicked(object sender, EventArgs e)
        {
                App.Current.MainPage.Navigation.PushAsync(new HistoryView(PersonalUser), true);
        }
        private async void BtnLogout_Clicked(object sender, EventArgs e)
        {
            
            bool answer = await DisplayAlert("Thông báo", "Bạn có muốn đăng xuất", "Có", "Không");
            if (answer)
            {
                App.Current.MainPage.Navigation.PushAsync(new LoginView(), true);
            }
            else
                return;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new PersonalUpdateView(PersonalUser), true);
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new HistoryView(PersonalUser), true);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            var tabbedPage = this.Parent as TabbedPage;
            Console.WriteLine(tabbedPage.ToString());
            tabbedPage.CurrentPage = tabbedPage.Children[1];
        }
    }
}