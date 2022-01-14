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
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PopAsync(true);
        }
        

        private async void CmdLogin_Clicked(object sender, EventArgs e)
        {
            string Usrname = Username.Text;
            string Pw = Pass.Text;
            User GlobalUser;

            //goi api so sanh nguoiw dung hop le   
            HttpClient httpClient = new HttpClient();
            var User = await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/CheckOneUser?username=" + Usrname + "&pw=" + Pw);
            User = User.Replace("[", string.Empty);
            User = User.Replace("]", string.Empty); // chuyen array thanh object

            //Kiem tra ket qua api
            if (User=="")
            {
                await DisplayAlert("Thông báo", "Đăng nhập thất bại: \nKhông đúng mật khẩu hoặc username", "OK");
            }
            else //neu dung truyen bien user qua mainpage
            {
                var UserCV = JsonConvert.DeserializeObject<User>(User);
                GlobalUser = UserCV;        
                //await DisplayAlert("Thông báo", "Đăng nhập thành công ", "OK");
                await App.Current.MainPage.Navigation.PushAsync(new MainView(GlobalUser), true);
            }
        }

        private void CmdRegister_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new RegisterView(), true);
        }
    }
}