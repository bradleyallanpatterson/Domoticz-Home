using Prism.Mvvm;

namespace DomoticzHome.Models
{
    public class Domoticz_Switch : BindableBase
    {
        private string _dimmerLevels;
        private bool _isDimmer;
        private string _name;
        private string _subtype;
        private string _type;
        private string _idx;
        private SwitchStatus _switchStatus;
        private string _switchImagePath;

        public string DimmerLevels
        {
            get { return _dimmerLevels; }
            set { SetProperty(ref _dimmerLevels, value); }
        }
        public bool IsDimmer
        {
            get { return _isDimmer; }
            set { SetProperty(ref _isDimmer, value); }
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string SubType
        {
            get { return _subtype; }
            set { SetProperty(ref _subtype, value); }
        }
        public string Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }
#pragma warning disable IDE1006 // Naming Styles
        public string idx
        {
            get { return _idx; }
            set { SetProperty(ref _idx, value); }
        }
#pragma warning restore IDE1006 // Naming Styles
        public SwitchStatus CurrentStatus
        {
            get { return _switchStatus; }
            set { SetProperty(ref _switchStatus, value); }
        }
        public string SwitchImagePath
        {
            get { return _switchImagePath; }
            set { SetProperty(ref _switchImagePath, value); }
        }

        private string _targetDevice = string.Empty;
        private string _switchImagePathOverride = string.Empty;
        private string _domoticzType = string.Empty;

        public bool UpdateUI(string deviceType, string os)
        {
            _targetDevice = os;
            _domoticzType = deviceType;
            return UpdateUI(null, deviceType, _targetDevice);
        }

        public bool UpdateUI(SwitchStatus newStatus, string deviceType, string os)
        {
            _targetDevice = os;
            _domoticzType = deviceType;
            bool statusChanged = HasStatusChanged(newStatus);

            SetImage();

            return statusChanged;
        }

		public void SetImage(bool flip)
		{
			bool deviceIsOn = false;
			string baseUrl = string.Empty;
			string picturePng = string.Empty;

			switch (_domoticzType)
			{
				case "WakeOnLan":
					if (_targetDevice == "Android")
						baseUrl = "";
					else
						baseUrl = "Images/WakeOnLAN/";
					picturePng = "wal.png";
					break;
				case "Security":
					if (_targetDevice == "Android")
						baseUrl = "";
					else
						baseUrl = "Images/Security/";
					break;
				case "Scene":
					if (_targetDevice == "Android")
						baseUrl = "";
					else
						picturePng = "push.png";
					break;
				case "Utility":
					if (_targetDevice == "Android")
						baseUrl = "";
					else
						baseUrl = "Images/Utility/";
					break;
				case "Switch":
				default:
					if (_targetDevice == "Android")
						baseUrl = "";
					else
						baseUrl = "Images/Switch/";
					break;
			}

			if (picturePng != string.Empty)
			{
				SwitchImagePath = $"{baseUrl}{picturePng}";
				return;
			}

			if (CurrentStatus != null && CurrentStatus.Status != null)
				deviceIsOn = (CurrentStatus.Status == "On" ||
					CurrentStatus.Status.Contains("Set Level:")) ? true : false;

			if (flip)
				deviceIsOn = !deviceIsOn;

			// Utility section
			if (Type.CompareTo("UV") == 0)
			{
				SwitchImagePath = string.Format("{0}uv.png", baseUrl);
				return;
			}
			else if (Type.CompareTo("Lux") == 0 && SubType.CompareTo("Lux") == 0)
			{
				SwitchImagePath = string.Format("{0}lux.png", baseUrl);
				return;
			}
			else if ((Type.CompareTo("Usage") == 0 && SubType.CompareTo("Electric") == 0) ||
				(Type.CompareTo("General") == 0 && SubType.CompareTo("kWh") == 0) ||
				(Type.CompareTo("General") == 0 && SubType.CompareTo("Voltage") == 0) ||
				(Type.CompareTo("Current") == 0 && (SubType.Contains("CM113") || SubType.Contains("Electrisave"))))
			{
				SwitchImagePath = string.Format("{0}current.png", baseUrl);
				return;
			}
			else if (Type.CompareTo("Temp + Humidity") == 0)
			{
				if (CurrentStatus == null)
				{
					SwitchImagePath = string.Format("{0}temp_25_30.png", baseUrl);
					return;
				}
				string[] split = CurrentStatus.Data.Split(',');
				string tempStr = split[0].Substring(0, split[0].IndexOf(' '));
				double dTemp = 0.0;
				double.TryParse(tempStr, out dTemp);
				if (dTemp >= 59 && dTemp < 68)
				{
					SwitchImagePath = string.Format("{0}temp_15_20.png", baseUrl);
					return;
				}
				else if (dTemp >= 68 && dTemp < 77)
				{
					SwitchImagePath = string.Format("{0}temp_20_25.png", baseUrl);
					return;
				}
				else if (dTemp >= 77 && dTemp < 86)
				{
					SwitchImagePath = string.Format("{0}temp_25_30.png", baseUrl);
					return;
				}


				SwitchImagePath = string.Format("{0}lux.png", baseUrl);
				return;
			}

			if (Name.Contains("Motion"))
			{
				if (deviceIsOn)
					SwitchImagePath = string.Format("{0}motion_on.png", baseUrl);
				else
					SwitchImagePath = string.Format("{0}motion_off.png", baseUrl);
			}
			else if (Name.Contains("3D Printer"))
			{
				if (deviceIsOn)
					SwitchImagePath = string.Format("{0}threedprinter_on.png", baseUrl);
				else
					SwitchImagePath = string.Format("{0}threedprinter_off.png", baseUrl);
			}
			else if (Name.Contains("Porch"))
			{
				if (deviceIsOn)
					SwitchImagePath = string.Format("{0}porchlight_on.png", baseUrl);
				else
					SwitchImagePath = string.Format("{0}porchlight_off.png", baseUrl);
			}
			else if (Name.Contains("Fan"))
			{
				if (deviceIsOn)
					SwitchImagePath = string.Format("{0}fan_on.png", baseUrl);
				else
					SwitchImagePath = string.Format("{0}fan_off.png", baseUrl);
			}
			else if (Name.Contains("Recessed"))
			{
				if (deviceIsOn)
					SwitchImagePath = string.Format("{0}recessedlight_on.png", baseUrl);
				else
					SwitchImagePath = string.Format("{0}recessedlight_off.png", baseUrl);
			}
			else if (Name.Contains("Internet"))
			{
				if (deviceIsOn)
					SwitchImagePath = string.Format("{0}www_on.png", baseUrl);
				else
					SwitchImagePath = string.Format("{0}www_off.png", baseUrl);
			}
			else if (Name.Contains("Plug"))
			{
				if (deviceIsOn)
					SwitchImagePath = string.Format("{0}plug_on.png", baseUrl);
				else
					SwitchImagePath = string.Format("{0}plug_Oof.png", baseUrl);
			}
			else if (Name.Contains("Sensor"))
			{
				if (deviceIsOn)
					SwitchImagePath = string.Format("{0}contact_open.png", baseUrl);
				else
					SwitchImagePath = string.Format("{0}contact.png", baseUrl);
			}
			else if (Name.Contains("PC"))
			{
				if (deviceIsOn)
					SwitchImagePath = string.Format("{0}computerpc_on.png", baseUrl);
				else
					SwitchImagePath = string.Format("{0}computerpc_off.png", baseUrl);
			}
			//else if (Name.Contains("Wake On LAN") && Name.Contains("Mac Mini"))
			//{
			//    SwitchImagePath = string.Format("{0}macmini.jpg", baseUrl);
			//}
			//else if (Name.Contains("Wake On LAN") && Name.Contains("PC"))
			//{
			//    SwitchImagePath = string.Format("{0}ComputerPC48_On.png", baseUrl);
			//}
			else
			{
				if (deviceIsOn)
					SwitchImagePath = string.Format("{0}light_on.png", baseUrl);
				else
					SwitchImagePath = string.Format("{0}light_off.png", baseUrl);
			}
			if (IsDimmer)
			{
				if (deviceIsOn)
					SwitchImagePath = string.Format("{0}dimmer_on.png", baseUrl);
				else
					SwitchImagePath = string.Format("{0}dimmer_off.png", baseUrl);
			}
		}

		private void SetImage()
        {
			SetImage(false);
        }

        private bool HasStatusChanged(SwitchStatus newStatus)
        {
            if (newStatus == null || CurrentStatus == null)
                return false;

            if (CurrentStatus.AddjMulti != newStatus.AddjMulti)
                return true;
            else if (CurrentStatus.AddjMulti2 != newStatus.AddjMulti2)
                return true;
            else if (CurrentStatus.AddjValue != newStatus.AddjValue)
                return true;
            else if (CurrentStatus.AddjValue2 != newStatus.AddjValue2)
                return true;
            else if (CurrentStatus.BatteryLevel != newStatus.BatteryLevel)
                return true;
            else if (CurrentStatus.CustomImage != newStatus.CustomImage)
                return true;
            else if (CurrentStatus.Data != newStatus.Data)
                return true;
            else if (CurrentStatus.Description != newStatus.Description)
                return true;
            else if (CurrentStatus.Favorite != newStatus.Favorite)
                return true;
            else if (CurrentStatus.HardwareID != newStatus.HardwareID)
                return true;
            else if (CurrentStatus.HardwareName != newStatus.HardwareName)
                return true;
            else if (CurrentStatus.HardwareType != newStatus.HardwareType)
                return true;
            else if (CurrentStatus.HardwareTypeVal != newStatus.HardwareTypeVal)
                return true;
            else if (CurrentStatus.HaveDimmer != newStatus.HaveDimmer)
                return true;
            else if (CurrentStatus.HaveGroupCmd != newStatus.HaveGroupCmd)
                return true;
            else if (CurrentStatus.HaveTimeout != newStatus.HaveTimeout)
                return true;
            else if (CurrentStatus.ID != newStatus.ID)
                return true;
            else if (CurrentStatus.idx != newStatus.idx)
                return true;
            else if (CurrentStatus.Image != newStatus.Image)
                return true;
            else if (CurrentStatus.IsSubDevice!= newStatus.IsSubDevice)
                return true;
            //else if (CurrentStatus.LastUpdate != newStatus.LastUpdate)
            //    return true;
            //else if (CurrentStatus.Level != newStatus.Level)
            //    return true;
            else if (CurrentStatus.LevelInt != newStatus.LevelInt)
                return true;
            else if (CurrentStatus.MaxDimLevel != newStatus.MaxDimLevel)
                return true;
            else if (CurrentStatus.Name != newStatus.Name)
                return true;
            else if (CurrentStatus.Notifications != newStatus.Notifications)
                return true;
            else if (CurrentStatus.PlanID != newStatus.PlanID)
                return true;
            //else if (CurrentStatus.PlanIDs != newStatus.PlanIDs)
            //    return true;
            else if (CurrentStatus.Protected != newStatus.Protected)
                return true;
            else if (CurrentStatus.ShowNotifications != newStatus.ShowNotifications)
                return true;
            else if (CurrentStatus.SignalLevel != newStatus.SignalLevel)
                return true;
            else if (CurrentStatus.Status != newStatus.Status)
                return true;
            else if (CurrentStatus.StrParam1 != newStatus.StrParam1)
                return true;
            else if (CurrentStatus.StrParam2 != newStatus.StrParam2)
                return true;
            else if (CurrentStatus.SubType != newStatus.SubType)
                return true;
            else if (CurrentStatus.SwitchType != newStatus.SwitchType)
                return true;
            else if (CurrentStatus.SwitchTypeVal != newStatus.SwitchTypeVal)
                return true;
            else if (CurrentStatus.Timers != newStatus.Timers)
                return true;
            else if (CurrentStatus.Type != newStatus.Type)
                return true;
            else if (CurrentStatus.TypeImg != newStatus.TypeImg)
                return true;
            else if (CurrentStatus.Unit != newStatus.Unit)
                return true;
            else if (CurrentStatus.Used != newStatus.Used)
                return true;
            else if (CurrentStatus.UsedByCamera != newStatus.UsedByCamera)
                return true;
            else if (CurrentStatus.XOffset != newStatus.XOffset)
                return true;
            else if (CurrentStatus.YOffset != newStatus.YOffset)
                return true;


            return false;
        }

    }
}
