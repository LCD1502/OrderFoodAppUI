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
    public partial class RestaurantDetail : ContentPage
    {
        public RestaurantDetail()
        {
            InitializeComponent();
        }
       
        public RestaurantDetail(Restaurant restaurant)
        {
            InitializeComponent();
            InforInit(restaurant);
            Title = restaurant.TEN;
        }
        void InforInit(Restaurant restaurant)
        {
            ResName.Text = restaurant.TEN;
            ResImg.Source = restaurant.IMG;
            ResPlace.Text = restaurant.DIADIEM;
        }

        private void BtnPlus_Clicked(object sender, EventArgs e)
        {

        }
    }
}