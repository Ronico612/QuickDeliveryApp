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
        public Admin admin;
        public ShopManager shopManager;
        public DeliveryPerson deliveryPerson;

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
            shoppingCart.IconImageSource = "ShoppingCart.png";
            this.Children.Add(shoppingCart);

            login = new Login();
            login.Title = "התחברות";
            login.IconImageSource = "Login.png";
            this.Children.Add(login);

            personalArea = new PersonalArea();
            personalArea.IconImageSource = "Login.png";
            personalArea.Title = "אזור אישי";

            admin = new Admin();
            admin.Title = "מנהל אתר";
            admin.IconImageSource = "admin.png";

            shopManager = new ShopManager();
            shopManager.Title = "מנהל חנות";
            shopManager.IconImageSource = "manager.png";

            deliveryPerson = new DeliveryPerson();
            deliveryPerson.Title = "שליח";
            deliveryPerson.IconImageSource = "delivery.png";
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