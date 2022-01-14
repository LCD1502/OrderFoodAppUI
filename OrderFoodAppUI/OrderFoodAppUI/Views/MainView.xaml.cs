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
    public partial class MainView : TabbedPage
    {
        public User Guser;
        readonly Page tab1Page;
        readonly Page tab2Page;
        readonly Page tab3Page;
        readonly Page tab4Page;
        public MainView()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            this.Children.Add(new HomeView()  { IconImageSource = "home"});
            this.Children.Add(new SearchView() { IconImageSource = "search" });
            this.Children.Add(new ProductView() { IconImageSource = "list" });
            this.Children.Add(new PersonalView() { IconImageSource = "user" });
        }
        public MainView(User user)
        {
            NavigationPage.SetHasBackButton(this, false);
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
            Guser = user;
            this.Children.Add(new HomeView(user) { IconImageSource = "home" }); //pass
            this.Children.Add(new SearchView(user) { IconImageSource = "search" }); //pass
            this.Children.Add(new ProductView(user) { IconImageSource = "list" });
            this.Children.Add(new PersonalView(user) { IconImageSource = "user" });

            tab1Page= new HomeView(user) { IconImageSource = "home" };
            tab2Page= new SearchView(user) { IconImageSource = "search" };
            tab3Page=new ProductView(user) { IconImageSource = "list" };
            tab4Page= new PersonalView(user) { IconImageSource = "user" };
            //this.Children.Add(new HistoryView(user) { IconImageSource = "history"});
        }
        protected override bool OnBackButtonPressed()
        {
            return false;
        }

    }
}