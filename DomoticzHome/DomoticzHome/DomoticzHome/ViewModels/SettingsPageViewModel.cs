using Prism.Navigation;
using Xamarin.Essentials;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomoticzHome.ViewModels
{
	public class SettingsPageViewModel : ViewModelBase, INavigationAware
	{
		private readonly INavigationService _navigationService;
		private bool _useWanSettings;

		private string _protocol;
		private string _address;
		private string _port;

		private string _secondaryProtocol;
		private string _secondaryAddress;
		private string _secondaryPort;


		private string _voiceControlWord;
		private string _lanImage = string.Empty;
		private string _wanImage = string.Empty;



		public string Protocol
		{
			get { return _protocol; }
			set 
			{ 
				SetProperty(ref _protocol, value); 
				Preferences.Set($"{Protocol}", $"{Protocol}");
			}
		}
		public string Address
		{
			get { return _address; }
			set 
			{ 
				SetProperty(ref _address, value); 
				Preferences.Set($"{Address}", $"{Address}");
			}
		}
		public string Port
		{
			get { return _port; }
			set { SetProperty(ref _port, value); Preferences.Set($"{Port}", $"{Port}"); }
		}


		public string SecondaryProtocol
		{
			get { return _secondaryProtocol; }
			set
			{
				SetProperty(ref _secondaryProtocol, value);
				Preferences.Set($"{SecondaryProtocol}", $"{SecondaryProtocol}");
			}
		}
		public string SecondaryAddress
		{
			get { return _secondaryAddress; }
			set { SetProperty(ref _secondaryAddress, value); Preferences.Set($"{SecondaryAddress}", $"{SecondaryAddress}"); }
		}
		public string SecondaryPort
		{
			get { return _secondaryPort; }
			set { SetProperty(ref _secondaryPort, value); Preferences.Set($"{SecondaryPort}", $"{SecondaryPort}"); }
		}
		public bool UseWanSettings
		{
			get { return _useWanSettings; }
			set { SetProperty(ref _useWanSettings, value); }
		}
		public string VoiceControlWord
		{
			get { return _voiceControlWord; }
			set { SetProperty(ref _voiceControlWord, value); }
		}
		public string LanImage
		{
			get { return _lanImage; }
			set
			{
				SetProperty(ref _lanImage, value);
			}
		}
		public string WanImage
		{
			get { return _wanImage; }
			set
			{
				SetProperty(ref _wanImage, value);
			}
		}



		public SettingsPageViewModel(INavigationService navigationService) : base(navigationService)
		{
			_navigationService = navigationService;

			Protocol = Preferences.Get("Protocol", "Http://");
			Address = Preferences.Get("Address", "192.168.1.53");
			Port = Preferences.Get("Port", "8080");
			SecondaryAddress = Preferences.Get("SecondaryAddress", "192.168.1.53");
			SecondaryPort = Preferences.Get("SecondaryPort", "8080");


			//UseWanSettings = CrossSettings.Current.AddOrUpdateValue("UseWanSettings", true);

			//VoiceControlWord = CrossSettings.Current.GetValueOrDefault("Controller", "Controller");

			//MessagingCenter.Subscribe<SettingsPage, bool>(this, "SetUseWanSettings", (sender, arg) =>
			//{
			//	UseWanSettings = arg;
			//});

			//MessagingCenter.Subscribe<DomoticzApiService, bool>(this, "CheckingNetwork", (sender, arg) =>
			//{
			//	SetNetworkImageWhileChecking(arg);
			//});

			//MessagingCenter.Subscribe<DomoticzApiService, bool>(this, "SetNetworkStatus", (sender, arg) =>
			//{
			//	SetInternetImage(arg);
			//});
		}



		public void OnNavigatedFrom(NavigationParameters parameters)
		{
			SetInternetImage(true);
			SetInternetImage(false);
		}

		public void OnNavigatedTo(NavigationParameters parameters)
		{
			SetInternetImage(true);
			SetInternetImage(false);
		}

		public void OnNavigatingTo(NavigationParameters parameters)
		{
			SetInternetImage(true);
			SetInternetImage(false);
		}




		private void SetNetworkImageWhileChecking(bool lan)
		{
			//if (TargetDevice == "Android")
			//{
			//	if (lan)
			//		LanImage = "circle_yellow.png";
			//	else
			//		WanImage = "circle_yellow.png";
			//}
			//else
			//{
				if (lan)
					LanImage = "Images/circle_yellow.png";
				else
					WanImage = "Images/circle_yellow.png";
			//}
		}

		private void SetInternetImage(bool checkingLan)
		{
			string result = string.Empty;

			//if (TargetDevice == "Android")
			//{
			//	//if (DomoticzApiService.Instance.IsLanAvailable() || DomoticzApiService.Instance.IsWanAvailable())
			//	//	result = "circle_green.png";
			//	//else
			//		result = "circle_red.png";
			//}
			//else
			//{
				//if (DomoticzApiService.Instance.IsLanAvailable() || DomoticzApiService.Instance.IsWanAvailable())
				//	result = "Images/circle_green.png";
				//else
					result = "Images/circle_red.png";
			//}

			if (checkingLan)
				LanImage = result;
			else
				WanImage = result;
		}



	}


}
