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
    public partial class RestaurantDetail : ContentPage
    {
        User RestaurantUser;
        public RestaurantDetail()
        {
            InitializeComponent();
        }
       
        public RestaurantDetail(Restaurant restaurant, User user)
        {
            InitializeComponent();
            InforInit(restaurant);
            Title = restaurant.TEN;
            RestaurantUser = user;
        }
        async void InforInit(Restaurant restaurant)
        {
            ResName.Text = restaurant.TEN;
            ResImg.Source = restaurant.IMG;
            ResPlace.Text = restaurant.DIADIEM;
            string maNH = restaurant.MANH;
            HttpClient httpClient = new HttpClient();
            var FoodList = await httpClient.GetStringAsync("http://172.30.8.50/AppFoodApi/api/FoodController/GetMonAnNhaHang?manh=" + maNH);
            var FoodListCV = JsonConvert.DeserializeObject<List<Food>>(FoodList);
            FoodLst.ItemsSource = FoodListCV;
            //restaurants = RestListCV;
            //ListRes.ItemsSource = RestListCV;
        }

        private async void BtnPlus_Clicked(object sender, EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            int MAMA = int.Parse(button.CommandParameter.ToString());
            //HttpClient httpClient = new HttpClient();
            //var CartList = await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/InsertGioHang?mand="+RestaurantUser.MAND.ToString()+"&mama="+MAMA.ToString());
            ////var CartListCV = JsonConvert.DeserializeObject<List<Cart>>(CartList);

            //User enteredUser = new User { USERNAME = Username.Text, PASS = Pass.Text };
            User_Food user_food = new User_Food { MAND = RestaurantUser.MAND, MAMA = MAMA };
            HttpClient http = new HttpClient();
            string jsonUserFood = JsonConvert.SerializeObject(user_food);
            StringContent httpContent = new StringContent(jsonUserFood, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await http.PostAsync("http://172.30.8.50/AppFoodApi/api/CartController/InsertGioHang", httpContent);
            var code = await result.Content.ReadAsStringAsync();
            
            if (Int32.Parse(code) > 0)
            {
                await DisplayAlert("Thông báo", "Thêm món ăn vào giỏ hàng thành công", "OK");
            }
            else
            {
                await DisplayAlert("Thông báo", "Có lỗi xảy ra, thêm món ăn KHÔNG thành công", "OK");
            }

        }
    }
}