using QuickDeliveryApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace QuickDeliveryApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TheMainTabbedPage : TabbedPage
    {
        public Shops shopsPage;
        public ShoppingCart shoppingCart;
        public Login login;
        public PersonalArea personalArea;
        public ShopManager shopManager;
        public Admin admin;

        public TheMainTabbedPage()
        {
            this.BindingContext = new TheMainTabbedPageViewModel();
            InitializeComponent();

            shopsPage = new Shops();
            shopsPage.Title = "חנויות";
            shopsPage.IconImageSource = "Search.png";
            this.Children.Add(shopsPage);

            shoppingCart = new ShoppingCart();
            shoppingCart.Title = "סל קניות";
            shoppingCart.IconImageSource = "ShoppingBag.png";
            this.Children.Add(shoppingCart);

            login = new Login();
            login.Title = "התחברות";
            login.IconImageSource = "Login.png";
            this.Children.Add(login);

            personalArea = new PersonalArea();
            personalArea.IconImageSource = "Login.png";
            personalArea.Title = "אזור אישי";

            shopManager = new ShopManager();
            shopManager.Title = "מנהל חנות";
            shopManager.IconImageSource = "ShoppingBag.png";

            admin = new Admin();
            admin.Title = "מנהל אתר";
            admin.IconImageSource = "ShoppingBag.png";
        }

        public void AddTab(Page p)
        {
            if (!this.Children.Contains(p))
                this.Children.Add(p);
        }

        public void RemoveTab(Page p)
        {
            if (this.Children.Contains(p))
                this.Children.Remove(p);
        }

        public void CurrentTab(Page p)
        {
            this.CurrentPage = p;
        }
    }
}