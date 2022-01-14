using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft;
using Newtonsoft.Json;

namespace OrderFoodAppUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchView : ContentPage
    {
        User SearchUser;
        public SearchView()
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            ListResInit();
        }
        public SearchView(User user)
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            ListResInit();
            SearchUser = user;
        }

        List<Restaurant> restaurants = new List<Restaurant>();
        async void ListResInit()
        {
            HttpClient httpClient = new HttpClient();
            var ResList = await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/GetNhaHang");
            var RestListCV = JsonConvert.DeserializeObject<List<Restaurant>>(ResList);
            restaurants = RestListCV;
            ListRes.ItemsSource = RestListCV;
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
        public static string RemoveUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
                        "đ",
                        "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
                        "í","ì","ỉ","ĩ","ị",
                        "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
                        "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
                        "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
                        "d",
                        "e","e","e","e","e","e","e","e","e","e","e",
                        "i","i","i","i","i",
                        "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
                        "u","u","u","u","u","u","u","u","u","u","u",
                        "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        

        private void SearchRes_TextChanged(object sender, TextChangedEventArgs e)
        {
            var result = restaurants.Where(c => RemoveUnicode(c.TEN.ToLower()).Contains(RemoveUnicode(SearchRes.Text.ToLower())) || RemoveUnicode(c.DIADIEM.ToLower()).Contains(RemoveUnicode(SearchRes.Text.ToLower())) || RemoveUnicode(c.LOAI.ToLower()).Contains(RemoveUnicode(SearchRes.Text.ToLower())));
            if (result.Count() == 0)
            {
                NotFound.IsVisible = true;
                ListRes.IsVisible = false;
            }
            else
            {
                NotFound.IsVisible = false;
                ListRes.IsVisible = true;
                ListRes.ItemsSource = result;
            }

        }
       
        private void BtnPlus_Clicked(object sender, EventArgs e)
        {

        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }

        private void ListRes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Restaurant slRes = (Restaurant)ListRes.SelectedItem;
            App.Current.MainPage.Navigation.PushAsync(new RestaurantDetail(slRes,SearchUser), true);
        }
    }
}