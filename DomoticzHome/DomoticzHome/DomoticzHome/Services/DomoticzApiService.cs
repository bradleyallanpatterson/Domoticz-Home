using DomoticzHome.Interfaces;
using DomoticzHome.Models;
using Newtonsoft.Json;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace DomoticzHome.Services
{

    public class DomoticzApiService : BaseProvider, IDomoticzApiService
    {
        public enum Response_Status { OK, NOT_CHECKED, ERR }
       // public enum ServerConnections { PRIMARY, SECONDARY, BOTH, NONE }

        public enum ServerConnection { ONLINE, CHECKING, OFFLINE }

        public static string Credentials = "/json.htm?username=brad=&password=elky454=&";
        public static string SunRiseSunSet = "type=command&param=getSunRiseSet";
        //public static string AllSwitches = "type=command&param=getlightswitches";
        public static string GetAllScenes = "type=scenes";
        //public static string GetWeather = "type=devices&filter=weather&used=true&order=Name";
        //public static string GetTemp = "type=devices&filter=temp&used=true&order=Name";
        //public static string GetUtility = "type=devices&filter=utility&used=true&order=Name";

        private static DomoticzApiService _instance;
        private static object _syncRoot = new Object();
        private string _sunriseText = string.Empty;
        private string _sunsetText = string.Empty;
        private string _statusUpdated = string.Empty;
        private INavigationService _navigationService;


        public NetworkAccess Access { get; set; }

        public IEnumerable<ConnectionProfile> ConnectionProfiles { get; set; }



        //public bool IsLanAvailable()
        //{
        //    return _isLanAvailable;
        //}
        //public bool IsWanAvailable()
        //{
        //    return _isWanAvailable;
        //}


        //public void SetLanAvailable(bool yesOrNo)
        //{
        //    _isLanAvailable = yesOrNo;
        //  //  MessagingCenter.Send<DomoticzApiService, bool>(this, "SetNetworkStatus", true);
        //}
        //public void SetWanAvailable(bool yesOrNo)
        //{
        //    _isWanAvailable = yesOrNo;
        //   // MessagingCenter.Send<DomoticzApiService, bool>(this, "SetNetworkStatus", false);
        //}

        ///// <summary>
        ///// Gets or sets a value indicating whether LAN network endpoint is currently being checked for availablity.
        ///// </summary>
        ///// <value><c>true</c> if the LAN is available; otherwise, <c>false</c>.</value>
        //public bool CheckingLanNetwork
        //{
        //    get { return _checkingLanNetwork; }
        //    set
        //    {
        //        _checkingLanNetwork = value;
        //        //MessagingCenter.Send<DomoticzApiService, bool>(this, "CheckingNetwork", _checkingLanNetwork);
        //    }
        //}
        ///// <summary>
        ///// Gets or sets a value indicating whether LAN network endpoint is currently being checked for availablity.
        ///// </summary>
        ///// <value><c>true</c> if the LAN is available; otherwise, <c>false</c>.</value>
        //public bool CheckingWanNetwork
        //{
        //    get { return _checkingWanNetwork; }
        //    set
        //    {
        //        _checkingWanNetwork = value;
        //       // MessagingCenter.Send<DomoticzApiService, bool>(this, "CheckingNetwork", _checkingWanNetwork);
        //    }
        //}

        //public string CurrentSunriseText()
        //{
        //    return _sunriseText;
        //}
        //public string CurrentSunsetText()
        //{
        //    return _sunsetText;
        //}
        //public string CurrentStatusUpdated()
        //{
        //    return _statusUpdated;
        //}




        public static DomoticzApiService Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_syncRoot)
                    {
                        _instance = new DomoticzApiService();
                    }
                }
                return _instance;
            }
        }

        public DomoticzApiService ()
        {
            _baseUrl = "";
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;

            Task.Run ( ( ) => BackgroundTasks ( ) );
        }

        public DomoticzApiService( INavigationService navigationService )
        {
            _baseUrl = "";
            _instance = new DomoticzApiService ( navigationService );
            _navigationService = navigationService;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            
            Task.Run ( ( ) => BackgroundTasks ( ) );
        }


        public async Task<bool> CheckNetworkConnections( INavigationService navigationService )
        {
            if (_navigationService == null)
                _navigationService = navigationService;

            bool result = await CheckNetworkConnectionsAsync();
            return result;
        }

        //public bool CheckNetworkConnections()
        //{
        //    bool result = false; // await CheckNetworkConnectionsAsync();
        //    return result;
        //}

        //public async Task<Domoticz_Times> GetSunRiseSunSet(string uri)
        //{
        //    Domoticz_Times _domoResponse = null;
        //    try
        //    {
        //        string jsonstring = string.Empty;
        //        jsonstring = await GetAsync(uri);
        //        if (jsonstring == null)
        //        {
        //            #region MyRegion
        //            //jsonstring = "{\n \"ServerTime\" : \"2017-05-21 11:13:05\",\n \"Sunrise\" : \"04:34\",\n \"Sunset\" : \"19:39\",\n \"status\" : \"OK\",\n \"title\" : \"getSunRiseSet\"\n}\n";
        //            #endregion
        //        }
        //        if (jsonstring != null)
        //        {
        //            _domoResponse = JsonConvert.DeserializeObject<Domoticz_Times>(jsonstring);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.WriteLine(string.Format("Error Message: {0}", ex.Message));
        //        Debug.WriteLine(string.Format("Error Inner: {0}", ex.InnerException));
        //    }
        //    return _domoResponse;
        //}

        //public async Task<Domoticz_SwitchStatus> QueryStatusAsync(bool useLan, string idxs)
        //{
        //    await WaitWhileCheckinghNetwork();
        //    Domoticz_SwitchStatus _response = null;
        //    try
        //    {
        //        string newQuery = Constants.GetIdxStatus.Replace("IDX", idxs);
        //        string uri = GetCurrentServerUri(useLan, newQuery);
        //        string jsonstring = string.Empty;
        //        try
        //        {
        //            jsonstring = await GetAsync(uri);
        //        }
        //        catch (Exception)
        //        {
        //            uri = GetCurrentServerUri(!useLan, newQuery);
        //            jsonstring = await GetAsync(uri);
        //        }
        //        if (jsonstring != null)
        //        {
        //            _response = JsonConvert.DeserializeObject<Domoticz_SwitchStatus>(jsonstring);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.WriteLine(string.Format("QueryStatus Message: {0}", ex.Message));
        //        Debug.WriteLine(string.Format("QueryStatus Inner: {0}", ex.InnerException));
        //    }
        //    return _response;
        //}

        //public async Task<Domoticz_Switch_Response> GetSwitchesAsync(string uri)
        //{
        //    await WaitWhileCheckinghNetwork();
        //    Domoticz_Switch_Response _domoResponse = null;
        //    try
        //    {
        //        string jsonstring = string.Empty;
        //        jsonstring = await GetAsync(uri);
        //        if (jsonstring == null)
        //        {
        //            #region MyRegion
        //            jsonstring = "";
        //            #endregion
        //        }
        //        if (jsonstring != null)
        //        {
        //            _domoResponse = JsonConvert.DeserializeObject<Domoticz_Switch_Response>(jsonstring);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.WriteLine(string.Format("Error Message: {0}", ex.Message));
        //        Debug.WriteLine(string.Format("Error Inner: {0}", ex.InnerException));
        //    }
        //    return _domoResponse;
        //}

        //public async Task<Domoticz_Switch_Response> GetSwitchesAsync(bool useLan)
        //{
        //    await WaitWhileCheckinghNetwork();
        //    Domoticz_Switch_Response _domoResponse = null;
        //    try
        //    {
        //        string jsonstring = string.Empty;
        //        string uri = GetCurrentServerUri(useLan, Constants.GetAllSwitches);
        //        jsonstring = await GetAsync(uri);
        //        if (jsonstring == null)
        //        {
        //            #region MyRegion
        //            //IFile file = await PCLStorage.FileSystem.Current.GetFileFromPathAsync(@"C:\repositories\VSTS\Domoticz\DomoticzHome\DomoticzHome\DomoticzHome\bin\Debug\switches.json");
        //            jsonstring = "{\n   \"result\" : [\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"3 Way\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"38\"\n      },\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"3D Printer - FF Creator\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"41\"\n      },\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"3D Printer See CNC\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"29\"\n      },\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"Back Door Sensor\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"49\"\n      },\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"Basement Bath Fan\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"37\"\n      },\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"Basement Motion Sensor\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"12\"\n      },\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"Basement Window Sensor\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"48\"\n      },\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"Bathroom Motion\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"32\"\n      },\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"Bathroom Seismic\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"33\"\n      },\n      {\n         \"DimmerLevels\" : \"all\",\n         \"IsDimmer\" : true,\n         \"Name\" : \"Brads Lights Dimmer\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"17\"\n      },\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"Brads PC\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"11\"\n      },\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"Coffee Table Recessed Lights\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"14\"\n      },\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"Front Porch\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"13\"\n      },\n      {\n         \"DimmerLevels\" : \"all\",\n         \"IsDimmer\" : true,\n         \"Name\" : \"Great Room Dimmer\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"16\"\n      },\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"Great Room PC Recessed Lights\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"40\"\n      },\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"Great Room Recessed Lights\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"39\"\n      },\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"Internet\",\n         \"SubType\" : \"Switch\",\n         \"Type\" : \"Light/Switch\",\n         \"idx\" : \"15\"\n      },\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"MAINPC Wake On LAN\",\n         \"SubType\" : \"AC\",\n         \"Type\" : \"Lighting 2\",\n         \"idx\" : \"24\"\n      },\n      {\n         \"DimmerLevels\" : \"none\",\n         \"IsDimmer\" : false,\n         \"Name\" : \"Mac Mini Wake On LAN\",\n         \"SubType\" : \"AC\",\n         \"Type\" : \"Lighting 2\",\n         \"idx\" : \"57\"\n      }\n   ],\n   \"status\" : \"OK\",\n   \"title\" : \"GetLightSwitches\"\n}\n";
        //            //var assem = typeof(Class).GetTypeInfo().Assembly;
        //            //Stream stream = assem.GetManifestResourceStream(string.Format("switches.json"));
        //            //using (var reader = new System.IO.StreamReader(stream))
        //            //{
        //            //    jsonstring = reader.ReadToEnd();
        //            //}
        //            //using (StreamReader r = new StreamReader())
        //            //{
        //            //    jsonstring = r.ReadToEnd();
        //            //    // List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
        //            //} 
        //            #endregion
        //        }
        //        if (jsonstring != null)
        //        {
        //            _domoResponse = JsonConvert.DeserializeObject<Domoticz_Switch_Response>(jsonstring);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.WriteLine(string.Format("Error Message: {0}", ex.Message));
        //        Debug.WriteLine(string.Format("Error Inner: {0}", ex.InnerException));
        //    }
        //    return _domoResponse;
        //}

        //public async Task<Domoticz_Switch_Response> GetWakeOnLanAsync(bool useLan)
        //{
        //    await WaitWhileCheckinghNetwork();
        //    Domoticz_Switch_Response _domoResponse = null;
        //    try
        //    {
        //        string jsonstring = string.Empty;
        //        string uri = GetCurrentServerUri(useLan, Constants.GetAllSwitches);
        //        try
        //        {
        //            jsonstring = await GetAsync(uri);
        //        }
        //        catch (Exception)
        //        {
        //            // Initial local attempt failed try WAN server
        //            uri = GetCurrentServerUri(!useLan, Constants.GetAllScenes);
        //            jsonstring = await GetAsync(uri);
        //        }
        //        if (jsonstring != null)
        //        {
        //            _domoResponse = JsonConvert.DeserializeObject<Domoticz_Switch_Response>(jsonstring);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.WriteLine(string.Format("Error Message: {0}", ex.Message));
        //        Debug.WriteLine(string.Format("Error Inner: {0}", ex.InnerException));
        //    }
        //    return _domoResponse;
        //}

        //public async Task<Domoticz_Switch_Response> QueryGetSecurityAsync(bool useLan)
        //{
        //    Domoticz_Switch_Response _domoResponse = null;
        //    try
        //    {
        //        string jsonstring = string.Empty;
        //        string uri = GetCurrentServerUri(useLan, Constants.GetAllSwitches);
        //        try
        //        {
        //            jsonstring = await GetAsync(uri);
        //        }
        //        catch (Exception)
        //        {
        //            // Initial local attempt
        //            uri = GetCurrentServerUri(!useLan, Constants.GetAllScenes);
        //            jsonstring = await GetAsync(uri);
        //        }
        //        if (jsonstring != null)
        //        {
        //            _domoResponse = JsonConvert.DeserializeObject<Domoticz_Switch_Response>(jsonstring);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.WriteLine(string.Format("Error Message: {0}", ex.Message));
        //        Debug.WriteLine(string.Format("Error Inner: {0}", ex.InnerException));
        //    }
        //    return _domoResponse;
        //}

        //public async Task<Domoticz_Scene_Response> GetSceneGroupsAsync(bool useLan)
        //{
        //    await WaitWhileCheckinghNetwork();
        //    Domoticz_Scene_Response _domoResponse = null;
        //    try
        //    {
        //        string jsonstring = string.Empty;
        //        string uri = GetCurrentServerUri(useLan, Constants.GetAllScenes);
        //        try
        //        {
        //            jsonstring = await GetAsync(uri);
        //        }
        //        catch (Exception)
        //        {
        //            // Initial local attempt failed try WAN server
        //            uri = GetCurrentServerUri(!useLan, Constants.GetAllScenes);
        //            jsonstring = await GetAsync(uri);
        //        }
        //        if (jsonstring != null)
        //        {
        //            _domoResponse = JsonConvert.DeserializeObject<Domoticz_Scene_Response>(jsonstring);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.WriteLine(string.Format("Error Message: {0}", ex.Message));
        //        Debug.WriteLine(string.Format("Error Inner: {0}", ex.InnerException));
        //    }
        //    return _domoResponse;
        //}

        //public async Task<Domoticz_Switch_Response> GetWeatherUtilitiesAsync(bool useLan)
        //{
        //    Domoticz_Switch_Response _domoResponse = null;
        //    try
        //    {
        //        string jsonstring = string.Empty;
        //        string uri = GetCurrentServerUri(useLan, Constants.GetWeather);
        //        try
        //        {
        //            jsonstring = await GetAsync(uri);
        //        }
        //        catch (Exception)
        //        {
        //            // Initial local attempt
        //            uri = GetCurrentServerUri(!useLan, Constants.GetWeather);
        //            jsonstring = await GetAsync(uri);
        //        }
        //        if (jsonstring != null)
        //        {
        //            _domoResponse = JsonConvert.DeserializeObject<Domoticz_Switch_Response>(jsonstring);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.WriteLine(string.Format("Error Message: {0}", ex.Message));
        //        Debug.WriteLine(string.Format("Error Inner: {0}", ex.InnerException));
        //    }
        //    return _domoResponse;

        //}

        //public async Task<Domoticz_Switch_Response> GetTempUtilitiesAsync(bool useLan)
        //{
        //    Domoticz_Switch_Response _domoResponse = null;
        //    try
        //    {
        //        string jsonstring = string.Empty;
        //        string uri = GetCurrentServerUri(useLan, Constants.GetTemp);
        //        try
        //        {
        //            jsonstring = await GetAsync(uri);
        //        }
        //        catch (Exception)
        //        {
        //            // Initial local attempt
        //            uri = GetCurrentServerUri(!useLan, Constants.GetTemp);
        //            jsonstring = await GetAsync(uri);
        //        }
        //        if (jsonstring != null)
        //        {
        //            _domoResponse = JsonConvert.DeserializeObject<Domoticz_Switch_Response>(jsonstring);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.WriteLine(string.Format("Error Message: {0}", ex.Message));
        //        Debug.WriteLine(string.Format("Error Inner: {0}", ex.InnerException));
        //    }
        //    return _domoResponse;

        //}

        //public async Task<Domoticz_Switch_Response> GetUtilityUtilitiesAsync(bool useLan)
        //{
        //    Domoticz_Switch_Response _domoResponse = null;
        //    try
        //    {
        //        string jsonstring = string.Empty;
        //        string uri = GetCurrentServerUri(useLan, Constants.GetUtility);
        //        try
        //        {
        //            jsonstring = await GetAsync(uri);
        //        }
        //        catch (Exception)
        //        {
        //            // Initial local attempt
        //            uri = GetCurrentServerUri(!useLan, Constants.GetUtility);
        //            jsonstring = await GetAsync(uri);
        //        }
        //        if (jsonstring != null)
        //        {
        //            _domoResponse = JsonConvert.DeserializeObject<Domoticz_Switch_Response>(jsonstring);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.WriteLine(string.Format("Error Message: {0}", ex.Message));
        //        Debug.WriteLine(string.Format("Error Inner: {0}", ex.InnerException));
        //    }
        //    return _domoResponse;

        //}




        //public async Task<Domoticz_Response> TurnSwitchOnOff(string idxToTurnOnOff, bool turnOn)
        //{
        //    Domoticz_Response _domoResponse = null;
        //    try
        //    {
        //        _baseUrl = GetLocalServerUri(false);
        //        string newLightQuery = Constants.ToggleSwitch.Replace("99", idxToTurnOnOff);
        //        if (turnOn)
        //            newLightQuery = newLightQuery.Replace("Off", "On");
        //        string u = string.Format("{0}{1}", _baseUrl, newLightQuery);
        //        string jsonstring = string.Empty;
        //        try
        //        {
        //            jsonstring = await GetAsync(u);
        //        }
        //        catch (Exception)
        //        {
        //            // Initial local attempt failed try WAN server
        //            _baseUrl = GetWanServerUri(false);
        //            u = string.Format("{0}{1}", GetWanServerUri(true), newLightQuery);
        //            jsonstring = await GetAsync(u);
        //        }

        //        if (jsonstring != null)
        //        {
        //            _domoResponse = JsonConvert.DeserializeObject<Domoticz_Response>(jsonstring);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.WriteLine(string.Format("Error Message: {0}", ex.Message));
        //        Debug.WriteLine(string.Format("Error Inner: {0}", ex.InnerException));
        //    }
        //    return _domoResponse;
        //}

        //public async Task<Domoticz_Response> ToggleSwitch(bool useLan, string idxToTurnOnOff)
        //{
        //    Domoticz_Response _domoResponse = null;
        //    try
        //    {
        //        string newLightQuery = Constants.ToggleSwitch.Replace(Constants.DefaultId.ToString(), idxToTurnOnOff);
        //        string uri = GetCurrentServerUri(useLan, newLightQuery);
        //        string jsonstring = string.Empty;
        //        try
        //        {
        //            jsonstring = await GetAsync(uri);
        //        }
        //        catch (Exception)
        //        {
        //            // Initial local attempt failed try WAN server
        //            _baseUrl = GetWanServerUri(false);
        //            uri = string.Format("{0}{1}", GetWanServerUri(true), newLightQuery);
        //            jsonstring = await GetAsync(uri);
        //        }
        //        if (jsonstring != null)
        //        {
        //            _domoResponse = JsonConvert.DeserializeObject<Domoticz_Response>(jsonstring);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.WriteLine(string.Format("Error Message: {0}", ex.Message));
        //        Debug.WriteLine(string.Format("Error Inner: {0}", ex.InnerException));
        //    }
        //    return _domoResponse;
        //}

        //public async Task<Domoticz_Response> ToggleScene(bool useLan, string idxToTurnOn)
        //{
        //    Domoticz_Response _domoResponse = null;
        //    try
        //    {
        //        string newLightQuery = Constants.TurnOnScene.Replace(Constants.DefaultId.ToString(), idxToTurnOn);
        //        string uri = GetCurrentServerUri(useLan, newLightQuery);
        //        string jsonstring = await GetAsync(uri);
        //        if (jsonstring != null)
        //        {
        //            _domoResponse = JsonConvert.DeserializeObject<Domoticz_Response>(jsonstring);
        //        }
        //    }
        //    catch (System.Exception ex)
        //    {
        //        Debug.WriteLine(string.Format("Error Message: {0}", ex.Message));
        //        Debug.WriteLine(string.Format("Error Inner: {0}", ex.InnerException));
        //    }
        //    return _domoResponse;
        //}




        //public async Task<bool> WaitWhileCheckinghNetwork()
        //{
        //    while (CheckingLanNetwork && CheckingWanNetwork)
        //    {
        //        await Task.Delay(100);
        //    }

        //    return true;
        //}









        public async Task<Domoticz_Times> GetSunRiseSunSetAsync ( string uriWithNoCredentialsOrQuery )
        {
            Domoticz_Times _domoResponse = null;
            try
            {
                string jsonstring = string.Empty;
                jsonstring = await GetAsync ( $"{uriWithNoCredentialsOrQuery}{Credentials}{SunRiseSunSet}" );
                if (jsonstring == null)
                {
                    #region Example Result
                    //jsonstring = "{\n \"ServerTime\" : \"2017-05-21 11:13:05\",\n \"Sunrise\" : \"04:34\",\n \"Sunset\" : \"19:39\",\n \"status\" : \"OK\",\n \"title\" : \"getSunRiseSet\"\n}\n";
                    #endregion
                }
                if (jsonstring != null)
                {
                    _domoResponse = JsonConvert.DeserializeObject<Domoticz_Times> ( jsonstring );
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine ( string.Format ( "Error Message: {0}", ex.Message ) );
                Debug.WriteLine ( string.Format ( "Error Inner: {0}", ex.InnerException ) );
            }
            return _domoResponse;
        }

        public async Task<Domoticz_Scene_Response> GetScenesAsync ( string uriWithNoCredentialsOrQuery )
        {
            //await WaitWhileCheckinghNetwork ( );
            Domoticz_Scene_Response _domoResponse = null;
            try
            {
                string jsonstring = string.Empty;
                try
                {
                    jsonstring = await GetAsync ( $"{uriWithNoCredentialsOrQuery}{Credentials}{GetAllScenes}" );
                }
                catch (Exception)
                {
                    //// Initial local attempt failed try WAN server
                    //uri = GetCurrentServerUri ( !useLan, Constants.GetAllScenes );
                    //jsonstring = await GetAsync ( uri );
                }
                if (jsonstring != null)
                {
                    _domoResponse = JsonConvert.DeserializeObject<Domoticz_Scene_Response> ( jsonstring );
                }
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine ( string.Format ( "Error Message: {0}", ex.Message ) );
                Debug.WriteLine ( string.Format ( "Error Inner: {0}", ex.InnerException ) );
            }
            return _domoResponse;
        }







        private async void BackgroundTasks()
        {
            while (true)
            {
                Response_Status primaryDomoticzIsSeen = Response_Status.NOT_CHECKED;
                Response_Status secondaryDomoticzIsSeen = Response_Status.NOT_CHECKED;

                // Establish connection with Domoticz
                string pUri = $"{Preferences.Get ( "Protocol", null )}{Preferences.Get ( "Address", null )}:{Preferences.Get ( "Port", null )}";
                string sUri = $"{Preferences.Get ( "SecondaryProtocol", null )}{Preferences.Get ( "SecondaryAddress", null )}:{Preferences.Get ( "SecondaryPort", null )}";


                if ( !pUri.Equals(":") && !sUri.Equals ( ":" ))
                {
                    string primaryUri = $"{Preferences.Get ( "Protocol", null )}{Preferences.Get ( "Address", null )}:{Preferences.Get ( "Port", null )}";
                    string secondaryUri = $"{Preferences.Get ( "Protocol", null )}{Preferences.Get ( "SecondaryAddress", null )}:{Preferences.Get ( "SecondaryPort", null )}";

                    // Establish if Network is available then query the servers
                    bool networkSeen = await CheckNetworkConnections ( _navigationService );
                    if (!networkSeen)
                    {
                        //await App.Current  DisplayAlert ( "Question?", "Would you like to play a game", "Yes", "No" );
                        await Prism.PrismApplicationBase.Current?.MainPage.DisplayAlert ( "Network Unavailable", "Please enable the network and retry.", "OK" );
                        await _navigationService.NavigateAsync ( "SettingsPage" );
                    }
                    else
                    {
                        if (!Preferences.Get ( "UseSecondaryServer", false ))
                        {
                            primaryDomoticzIsSeen = await CheckNetworkForDomoticz ( primaryUri );
                            secondaryDomoticzIsSeen = Response_Status.NOT_CHECKED;
                        }
                        else
                        {
                            secondaryDomoticzIsSeen = await CheckNetworkForDomoticz ( secondaryUri );
                            primaryDomoticzIsSeen = await CheckNetworkForDomoticz ( primaryUri );
                        }

                        SetImages ( primaryDomoticzIsSeen, secondaryDomoticzIsSeen );
                    } 
                }










                await Task.Delay(5000);
            }
        }













        private void Connectivity_ConnectivityChanged ( object sender, ConnectivityChangedEventArgs e )
        {
            Access = e.NetworkAccess;
            ConnectionProfiles = e.ConnectionProfiles;
        }

        private void Accelerometer_ShakeDetected ( object sender, EventArgs e )
        {
            // Process shake event
        }

        private async Task<bool> CheckNetworkConnectionsAsync()
        {
            string result = null;
            try
            {
                // First save the current Network State
                Access = Connectivity.NetworkAccess;
                IEnumerable<ConnectionProfile> profiles = Connectivity.ConnectionProfiles;
            }
            catch (Exception)
            {
                await Task.Delay ( 1 );
                return false;
            }
            return true;

        }

        private async Task<Response_Status> CheckNetworkForDomoticz ( string uriWithNoCredentialsOrQuery )
        {
            Domoticz_Times result = null;
            Response_Status success = Response_Status.ERR;
            try
            {
                // You must set the Uri to the base HtpClient or the request fails
                _baseUrl = uriWithNoCredentialsOrQuery;
                result = await GetSunRiseSunSetAsync ( $"{uriWithNoCredentialsOrQuery}" );
                if ( result?.status == Response_Status.OK.ToString ( ) )
                {
                    success = Response_Status.OK;
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine ( string.Format ( "Error Message: {0}", ex.Message ) );
                Debug.WriteLine ( string.Format ( "Error Inner: {0}", ex.InnerException ) );
            }

            return success;
        }

        private void SetImages( Response_Status primaryDomoticzIsSeen , Response_Status secondaryDomoticzIsSeen )
        {
            if (primaryDomoticzIsSeen == Response_Status.OK)
            {
                Preferences.Set ( $"PrimaryServerAvailable", true );
            }
            else
            {
                Preferences.Set ( $"PrimaryServerAvailable", false );
            }

            if (secondaryDomoticzIsSeen == Response_Status.OK)
            {
                Preferences.Set ( $"SecondaryServerAvailable", true );
            }
            else
            {
                Preferences.Set ( $"SecondaryServerAvailable", false );
            }
        }


    }

}
