using Prism;
using Prism.Ioc;
using DomoticzHome.ViewModels;
using DomoticzHome.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace DomoticzHome
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            //Core.initializeKit ( typeof ( App ).GetTypeInfo ( ).Assembly );

            // await NavigationService.NavigateAsync("NavigationPage/MainPage");

            await NavigationService.NavigateAsync( "MainTabbedPage" );
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            //containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();

            containerRegistry.RegisterForNavigation<MainPage> ( );
            containerRegistry.RegisterForNavigation<MainTabbedPage> ();
            containerRegistry.RegisterForNavigation<SwitchPage> ( );
            containerRegistry.RegisterForNavigation<ScenePage> ( );

            containerRegistry.RegisterForNavigation<SettingsPage> ( );

        }
    }
}
