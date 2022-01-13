using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace OrderFoodAppUI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryView : ContentPage
    {

        public HistoryView()
        {
            InitializeComponent();
        }
        public HistoryView(User user)
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            ListHistoryInit(user);
        }

        async void ListHistoryInit(User user)
        {
            HttpClient httpClient = new HttpClient();
            var HistoryList = await httpClient.GetStringAsync("http://appfood.somee.com/api/AppFoodController/GetHoaDonByUser?mand=" + user.MAND.ToString());
            var HistoryListCV = JsonConvert.DeserializeObject<List<Receipt>>(HistoryList);

            LstHistory.ItemsSource = HistoryListCV;
        }

    }
}