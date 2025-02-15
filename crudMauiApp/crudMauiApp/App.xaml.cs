namespace crudMauiApp
{
    public partial class App : Application
    {
        public static GlobalApp GlobalApp;
        public App()
        {
            InitializeComponent();
            GlobalApp = new GlobalApp();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}