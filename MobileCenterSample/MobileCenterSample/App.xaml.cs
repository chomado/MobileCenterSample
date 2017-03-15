using System.Collections.Generic;

using MobileCenterSample.Helpers;
using MobileCenterSample.Services;
using MobileCenterSample.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

// -- Mobile Center
using Microsoft.Azure.Mobile;
using Microsoft.Azure.Mobile.Analytics;
using Microsoft.Azure.Mobile.Crashes;
// ---

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MobileCenterSample
{
    public partial class App : Application
    {
        //MUST use HTTPS, neglecting to do so will result in runtime errors on iOS
        public static bool AzureNeedsSetup => AzureMobileAppUrl == "https://CONFIGURE-THIS-URL.azurewebsites.net";
        public static string AzureMobileAppUrl = "https://MobileCenterSample.azurewebsites.net";
        public static IDictionary<string, string> LoginParameters => null;

        public App()
        {
            InitializeComponent();

            if (AzureNeedsSetup)
                DependencyService.Register<MockDataStore>();
            else
                DependencyService.Register<AzureDataStore>();

            SetMainPage();
        }

        public static void SetMainPage()
        {
            if (!AzureNeedsSetup && !Settings.IsLoggedIn)
            {
                Current.MainPage = new NavigationPage(new LoginPage())
                {
                    BarBackgroundColor = (Color)Current.Resources["Primary"],
                    BarTextColor = Color.White
                };
            }
            else
            {
                GoToMainPage();
            }
        }

        protected override void OnStart()
        {
            base.OnStart();

            // Mobile Center
            MobileCenter.Start(
                appSecret: $"ios={Const.Keys.MobileCenterSecretIos};" + $"android={Const.Keys.MobileCenterSecretAndroid}",
                services: new[] { typeof(Analytics), typeof(Crashes) }
            );
        }

        public static void GoToMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                Children =
                {
                    new NavigationPage(new ItemsPage())
                    {
                        Title = "Browse",
                        Icon = Device.OnPlatform("tab_feed.png",null,null)
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = Device.OnPlatform("tab_about.png",null,null)
                    },
                }
            };
        }
    }
}
