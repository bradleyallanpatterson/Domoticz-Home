using DomoticzHome.Models;
using Prism.Navigation;
using System.Threading.Tasks;

namespace DomoticzHome.Interfaces
{

    public interface IDomoticzApiService
    {
        //Task<Domoticz_SwitchStatus> QueryStatusAsync(bool useLan, string idxs);
        //Task<Domoticz_Switch_Response> GetSwitchesAsync(bool useLan);
        //Task<Domoticz_Switch_Response> GetWakeOnLanAsync(bool useLan);
        //Task<Domoticz_Switch_Response> QueryGetSecurityAsync(bool useLan);
        //Task<Domoticz_Scene_Response> GetSceneGroupsAsync(bool useLan);
        Task<Domoticz_Times> GetSunRiseSunSetAsync ( string uriWithNoCredentialsOrQuery  );

        //Task<Domoticz_Response> TurnSwitchOnOff(string idxToTurnOnOff, bool turnOn);
        //Task<Domoticz_Response> ToggleSwitch(bool useLan, string idxToTurnOnOff);
        //Task<Domoticz_Response> ToggleScene(bool useLan, string idxToTurnOn);

        Task<bool> CheckNetworkConnections ( INavigationService navigationService );

        //bool CheckNetworkConnections();

        //bool IsLanAvailable();
        //bool IsWanAvailable();
        //string CurrentSunriseText();
        //string CurrentSunsetText();
        //string CurrentStatusUpdated();
        //string GetCurrentServerUri(bool useLan, string extension);

        //string GetLocalServerUri(bool addExtensions);
        //string GetWanServerUri(bool addExtensions);
        //string GetCurrentServerImagesUri(bool useLan);

        //Task<List<SerieSearch>> GetSeriesSearch(string name);
        //Task<SerieFollowersVM> GetStatsSerieHighlighted();
    }

}
