namespace Collections
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Collections.Views.MainPage());
        }

    }
}