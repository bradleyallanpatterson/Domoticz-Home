using Newtonsoft.Json;
using Prism.Mvvm;

namespace DomoticzHome.Models
{
    public class Domoticz_Scene : BindableBase
    {
        [JsonIgnore]
        private string _description;
        [JsonIgnore]
        private int _favorite;
        [JsonIgnore]
        private string _lastUpdate;
        [JsonIgnore]
        private string _name;
        [JsonIgnore]
        private string _offAction;
        [JsonIgnore]
        private string _onAction;
        [JsonIgnore]
        private bool _protected;
        [JsonIgnore]
        private string _status;
        [JsonIgnore]
        private string _timers;
        [JsonIgnore]
        private string _type;
        [JsonIgnore]
        private bool _usedByCamera;
        [JsonIgnore]
        private string _idx;
        [JsonIgnore]
        private string _switchImagePath;


        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }
        public int Favorite
        {
            get { return _favorite; }
            set { SetProperty(ref _favorite, value); }
        }
        public string LastUpdate
        {
            get { return _lastUpdate; }
            set { SetProperty(ref _lastUpdate, value); }
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string OffAction
        {
            get { return _offAction; }
            set { SetProperty(ref _offAction, value); }
        }
        public string OnAction
        {
            get { return _onAction; }
            set { SetProperty(ref _onAction, value); }
        }
        public bool Protected
        {
            get { return _protected; }
            set { SetProperty(ref _protected, value); }
        }
        public string Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }
        public string Timers
        {
            get { return _timers; }
            set { SetProperty(ref _timers, value); }
        }
        public string Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }
        public bool UsedByCamera
        {
            get { return _usedByCamera; }
            set { SetProperty(ref _usedByCamera, value); }
        }
        public string idx
        {
            get { return _idx; }
            set { SetProperty(ref _idx, value); }
        }
        public string SwitchImagePath
        {
            get { return _switchImagePath; }
            set { SetProperty(ref _switchImagePath, value); }
        }

        public void SetImagePath(string uri)
        {
            SwitchImagePath = string.Format("{0}push.png", uri);
        }

    }
}
