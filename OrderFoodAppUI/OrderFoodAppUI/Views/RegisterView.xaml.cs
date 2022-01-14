using Newtonsoft.Json;
using System.Net.Http;
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
    public partial class RegisterView : ContentPage
    {
        public RegisterView()
        {
            InitializeComponent();
        }
        
        private async void cmdDangkybtn_Clicked(object sender, EventArgs e)
        {
            if(password.Text!=password2.Text)
            {
                await DisplayAlert("Thông báo", "Tạo người dùng thất bại! Mật khẩu nhập lại không đúng", "OK");
                return;
            }    
            User newUser = new User();
            string Usrname = username.Text;
            string Pass = password.Text;
            string Name = name.Text;
            string sdt = phone_number.Text;
            string Email = email.Text;
            string DoB = DateOfBirth.Date.ToString();

            HttpClient httpClient = new HttpClient();
            var code =  await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/CreateUser?username=" + Usrname.ToString() + "&pw=" + Pass.ToString() + "&name=" + Name.ToString() + "&SDT="+ sdt + "&EMAIL=" + Email.ToString()+ "&NGAYSINH="+ DoB);
            code = code.ToString();
            code=code.Replace("[{\"Code\":", string.Empty);
            code = code.Replace("}]", string.Empty);
            if (code == "1")
            {
                await DisplayAlert("Thông báo", "Tạo người dùng thành công.", "OK");
                await App.Current.MainPage.Navigation.PushAsync(new LoginView(), false);
            }
            else
            {
                await DisplayAlert("Thông báo", "Tạo người dùng thất bại! Xin mời bạn tạo lại!" , "OK");
            }
        }
    }
}