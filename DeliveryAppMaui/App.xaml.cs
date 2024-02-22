namespace DeliveryAppMaui
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override Window CreateWindow(IActivationState activationState)
        {
            var window = base.CreateWindow(activationState);
            window.Title = "d.Code Mobility Support Application";
            window.MinimumHeight = 600;
            window.MinimumWidth = 1200;
            window.MaximumHeight = 1080;
            window.MaximumWidth = 1920;
            window.X = 50;
            window.Y = 50;

            return window;
        }
    }
}