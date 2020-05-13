namespace DomoticzHome.Models
{
    public class Domoticz_Times : Domoticz_Response
    {
        private string _serverTime;
        private string _sunrise;
        private string _sunset;

        public string ServerTime { get { return _serverTime; } set { SetProperty(ref _serverTime, value); } }
        public string Sunrise { get { return _sunrise; } set { SetProperty(ref _sunrise, value); } }
        public string Sunset { get { return _sunset; } set { SetProperty(ref _sunset, value); } }



    }
}
