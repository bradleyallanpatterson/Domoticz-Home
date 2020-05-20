using DomoticzHome.Models;
using DomoticzHome.Services;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;

namespace DomoticzHome.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        private const int _smallWightResolution = 768;
        private const int _smallHeightResolution = 1280;

        private string _primaryServerImage = string.Empty;
        private string _secondaryServerImage = string.Empty;

        private ObservableRangeCollection<Domoticz_Switch> _switches;


        private string _protocol;
        private string _address;
        private string _port;

        private string _secondaryProtocol;
        private string _secondaryAddress;
        private string _secondaryPort;

        private bool _useSecondaryServer;


        public string Protocol
        {
            get { return (String.IsNullOrEmpty(_protocol)) ? Preferences.Get ( "Protocol", null ) : _protocol ; }
            set
            {
                SetProperty ( ref _protocol, value );
                Preferences.Set ( "Protocol", value );
            }
        }
        public string Address
        {
            get { return ( String.IsNullOrEmpty ( _address ) ) ? Preferences.Get ( "Address", null ) : _address; }
            set
            {
                if (value != string.Empty)
                {
                    SetProperty ( ref _address, value );
                    Preferences.Set ( "Address", value );
                }
            }
        }
        public string Port
        {
            get { return ( String.IsNullOrEmpty ( _port ) ) ? Preferences.Get ( "Address", null ) : _port; }
            set
            {
                if (value != string.Empty)
                {
                    SetProperty ( ref _port, value );
                    Preferences.Set ( "Port", value );
                }
            }
        }



        public string SecondaryProtocol
        {
            get { return ( String.IsNullOrEmpty ( _secondaryProtocol ) ) ? Preferences.Get ( "Protocol", null ) : _secondaryProtocol; }
            set
            {
                SetProperty ( ref _secondaryProtocol, value );
                Preferences.Set ( "SecondaryProtocol", value );
            }
        }
        public string SecondaryAddress
        {
            get { return ( String.IsNullOrEmpty ( _secondaryAddress ) ) ? Preferences.Get ( "Protocol", null ) : _secondaryAddress; }
            set { SetProperty ( ref _secondaryAddress, value ); Preferences.Set ( "SecondaryAddress", value ); }
        }
        public string SecondaryPort
        {
            get { return ( String.IsNullOrEmpty ( _secondaryPort ) ) ? Preferences.Get ( "Protocol", null ) : _secondaryPort; }
            set { SetProperty ( ref _secondaryPort, value ); Preferences.Set ( "SecondaryPort", value ); }
        }

        public string TargetDevice
        {
            get
            {
                return DeviceInfo.Platform.ToString();
            }

        }

        public bool UseSecondaryServer
        {
            get { return _useSecondaryServer; }
            set
            {
                if (value != _useSecondaryServer)
                {
                    Preferences.Set ( "UseSecondaryServer", value );
                    SetProperty ( ref _useSecondaryServer, value );
                }
            }
        }

        public string Uri
        {
            get
            {
                bool primaryServerSeen = Preferences.Get ( "PrimaryServerAvailable", false );
                bool secondaryServerSeen = Preferences.Get ( "SecondaryServerAvailable", false );
             //   bool useSecondaryServer = Preferences.Get ( "UseSecondaryServer", false );
                string newUri = string.Empty;
                string pUri = $"{Preferences.Get ( "Protocol", null )}{Preferences.Get ( "Address", null )}:{Preferences.Get ( "Port", null )}";
                string sUri = $"{Preferences.Get ( "SecondaryProtocol", null )}{Preferences.Get ( "SecondaryAddress", null )}:{Preferences.Get ( "SecondaryPort", null )}";

                if ( primaryServerSeen )
                    newUri = $"{Protocol}{Address}:{Port}";
                else if( secondaryServerSeen )
                    newUri = $"{SecondaryProtocol}{SecondaryAddress}:{SecondaryPort}";


                return newUri;
            }
        }





        public string PrimaryServerImage
        {
            get { return _primaryServerImage; }
            set
            {
                if( value != _primaryServerImage)
                    SetProperty ( ref _primaryServerImage, value );
            }
        }
        public string SecondaryServerImage
        {
            get { return _secondaryServerImage; }
            set
            {
                if (value != _secondaryServerImage)
                    SetProperty ( ref _secondaryServerImage, value );
            }
        }


        public ObservableRangeCollection<Domoticz_Scene> Scenes
        {
            get
            {
                return DomoticzData.Instance.Scenes;
            }
            set
            {
                DomoticzData.Instance.Scenes = value;
            }

        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }


        public void VibrateAFewSeconds( double lengthToVibrarte )
        {
            var duration = TimeSpan.FromSeconds ( lengthToVibrarte );
            Vibration.Vibrate ( duration );
        }

        public void GetTrackingInformation()
        {
            // For now this is so I can troubleshoot


            // First time ever launched application
            var firstLaunch = VersionTracking.IsFirstLaunchEver;

            // First time launching current version
            var firstLaunchCurrent = VersionTracking.IsFirstLaunchForCurrentVersion;

            // First time launching current build
            var firstLaunchBuild = VersionTracking.IsFirstLaunchForCurrentBuild;

            // Current app version (2.0.0)
            var currentVersion = VersionTracking.CurrentVersion;

            // Current build (2)
            var currentBuild = VersionTracking.CurrentBuild;

            // Previous app version (1.0.0)
            var previousVersion = VersionTracking.PreviousVersion;

            // Previous app build (1)
            var previousBuild = VersionTracking.PreviousBuild;

            // First version of app installed (1.0.0)
            var firstVersion = VersionTracking.FirstInstalledVersion;

            // First build of app installed (1)
            var firstBuild = VersionTracking.FirstInstalledBuild;

            // List of versions installed (1.0.0, 2.0.0)
            var versionHistory = VersionTracking.VersionHistory;

            // List of builds installed (1, 2)
            var buildHistory = VersionTracking.BuildHistory;
        }



        public static bool IsASmallDevice ( )
        {
            // Get Metrics
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

            // Width (in pixels)
            var width = mainDisplayInfo.Width;

            // Height (in pixels)
            var height = mainDisplayInfo.Height;
            return ( width <= _smallWightResolution && height <= _smallHeightResolution );
        }


        public async Task<bool> QueryScenesAsync ( string url )
        {
            try
            {
                //await DomoticzApiService.Instance.WaitWhileCheckinghNetwork ( );
                //IsSceneBusy = true;
                ObservableRangeCollection<Domoticz_Scene> tempCollection = new ObservableRangeCollection<Domoticz_Scene> ( );
                var result = await DomoticzApiService.Instance.GetScenesAsync ( url );
                if (result?.status == "OK")
                {
                    foreach (Domoticz_Scene dScene in result.result)
                    {
                        if (!Scenes.Contains ( dScene ))
                        {
                           // dScene.SetImagePath ( "Images/Scene/" );
                            tempCollection.Add ( dScene );
                        }
                    }
                    Scenes.ReplaceRange ( tempCollection );
                }
                else
                {
                    //IsSceneBusy = false;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine ( string.Format ( "QuerySwitches Error Message: '{0}'", ex.Message ) );
                Debug.WriteLine ( string.Format ( "QuerySwitches Error Inner: '{0}'", ex.InnerException ) );
            }
            //IsSceneBusy = false;
            return true;
        }












        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;

            CheckNetworkConnections( navigationService );

            Task.Run ( ( ) => BackgroundTasks ( ) );
        }


        public async void CheckNetworkConnections( INavigationService navigationService )
        {
            if( DomoticzApiService.Instance.ConnectionProfiles == null ) 
                await DomoticzApiService.Instance.CheckNetworkConnections( navigationService );
        }


        private async void BackgroundTasks()
        {
            while (true)
            {
                bool queryResult = false;
                // Update Server Images
                var primaryServerSeen = Preferences.Get ( "PrimaryServerAvailable", false );
                var secondaryServerSeen = Preferences.Get ( "SecondaryServerAvailable", false );

                if ( primaryServerSeen )
                {
                    if( TargetDevice == "Android" )
                        PrimaryServerImage = "circle_green.png";
                    else
                        PrimaryServerImage = "Images/circle_green.png";
                }
                else
                {
                    if (TargetDevice == "Android")
                        PrimaryServerImage = "circle_green.png";
                    else
                        PrimaryServerImage = "Images/circle_red.png";
                }

                if (secondaryServerSeen )
                {
                    SecondaryServerImage = "Images/circle_green.png";
                }
                else
                {
                    SecondaryServerImage = "Images/circle_red.png";
                }

                // First query the primary if available
                string uri = Uri;
                if( !string.IsNullOrEmpty( uri ) )
                    queryResult = await QueryScenesAsync ( uri );
     


                await Task.Delay(2000);
            }
        }



        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
