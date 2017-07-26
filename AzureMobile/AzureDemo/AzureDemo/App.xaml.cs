using System.Collections.Generic;

using Xamarin.Forms;

namespace AzureDemo
{
	public partial class App : Application
	{
		public static bool AzureNeedsSetup => false;
		public static string AzureMobileAppUrl = "[Replace your Azure Service Url here]";
		public static IDictionary<string, string> LoginParameters => null;

		public App()
		{
			InitializeComponent();

			DependencyService.Register<UsersViewModel>();
			DependencyService.Register<UsersPage>();

			if (AzureNeedsSetup)
				DependencyService.Register<MockDataStore>();
			else
				DependencyService.Register<UserCloudDataStore>();

			SetMainPage();
		}

		public static void SetMainPage()
		{
			if (!AzureNeedsSetup && !Settings.IsLoggedIn)
			{
				Current.MainPage = new NavigationPage(DependencyService.Get<UsersPage>())
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

		public static void GoToMainPage()
		{
			Current.MainPage = new TabbedPage
			{
				Children = {
					new NavigationPage(DependencyService.Get<UsersPage>())
					{
						Title = "Browse"
					},
					new NavigationPage(new AboutPage())
					{
						Title = "About"
					},
				}
			};
		}
	}
}
