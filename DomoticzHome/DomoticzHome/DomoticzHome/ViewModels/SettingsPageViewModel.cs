using Prism.Navigation;
using Xamarin.Essentials;
using Prism.Commands;
using Xamarin.Forms;
using System.Linq;
using System;
using System.ComponentModel;

namespace DomoticzHome.ViewModels
{
	public class SettingsPageViewModel : ViewModelBase, INavigationAware, INotifyPropertyChanged
	{
		private readonly INavigationService _navigationService;

		private bool _useWanSettings;




		public SettingsPageViewModel(INavigationService navigationService) : base(navigationService)
		{
			_navigationService = navigationService;

			Protocol = Preferences.Get("Protocol", "Http://");
			Address = Preferences.Get("Address", "192.168.1.53");
			Port = Preferences.Get("Port", "8080");

			SecondaryProtocol = Preferences.Get ( "Protocol", "Http://" );
			SecondaryAddress = Preferences.Get("SecondaryAddress", "192.168.1.53");
			SecondaryPort = Preferences.Get("SecondaryPort", "8080");

			base.PrimaryServerImage = "Images/circle_yellow.png";
			base.SecondaryServerImage = "Images/circle_yellow.png";

			UseSecondaryServer = Preferences.Get ( "UseSecondaryServer", false );


		}



		public void OnNavigatedFrom ( NavigationParameters parameters )
		{

		}

		public void OnNavigatedTo ( NavigationParameters parameters )
		{

		}

		public void OnNavigatingTo ( NavigationParameters parameters )
		{

		}


	}


}
