using System.Collections.Generic;

namespace DomoticzHome.Models
{
    public class Domoticz_SwitchStatus : Domoticz_Times
    {
        private int _actTime;
        private ObservableRangeCollection<SwitchStatus> _result;

        public int ActTime { get { return _actTime; } set { SetProperty(ref _actTime, value); } }
        public ObservableRangeCollection<SwitchStatus> result { get { return _result; } set { SetProperty(ref _result, value); } }
    }

    public class SwitchStatus
    {
        public double AddjMulti { get; set; }
        public double AddjMulti2 { get; set; }
        public double AddjValue { get; set; }
        public double AddjValue2 { get; set; }
        public int BatteryLevel { get; set; }
        public int CustomImage { get; set; }
        public string Data { get; set; }
        public string Description { get; set; }
        public int Favorite { get; set; }
        public int HardwareID { get; set; }
        public string HardwareName { get; set; }
        public string HardwareType { get; set; }
        public int HardwareTypeVal { get; set; }
        public bool HaveDimmer { get; set; }
        public bool HaveGroupCmd { get; set; }
        public bool HaveTimeout { get; set; }
        public string ID { get; set; }
        public string Image { get; set; }
        public bool IsSubDevice { get; set; }
        public string LastUpdate { get; set; }
        public int Level { get; set; }
        public int LevelInt { get; set; }
        public int MaxDimLevel { get; set; }
        public string Name { get; set; }
        public string Notifications { get; set; }
        public string PlanID { get; set; }
        public List<int> PlanIDs { get; set; }
        public bool Protected { get; set; }
        public bool ShowNotifications { get; set; }
        public string SignalLevel { get; set; }
        public string Status { get; set; }
        public string StrParam1 { get; set; }
        public string StrParam2 { get; set; }
        public string SubType { get; set; }
        public string SwitchType { get; set; }
        public int SwitchTypeVal { get; set; }
        public string Timers { get; set; }
        public string Type { get; set; }
        public string TypeImg { get; set; }
        public int Unit { get; set; }
        public int Used { get; set; }
        public bool UsedByCamera { get; set; }
        public string XOffset { get; set; }
        public string YOffset { get; set; }
        public string idx { get; set; }
    }

}
