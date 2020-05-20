using DomoticzHome.Models;
using Prism.Mvvm;


namespace DomoticzHome.Services
{
	public class DomoticzData : BindableBase
	{
		private object _lock = new object();
		private static object _syncRoot = new object();
		private static DomoticzData _instance = new DomoticzData();

		private static bool _isDevicePortrait = false;

		private ObservableRangeCollection<Domoticz_Scene> _scenes;
		public ObservableRangeCollection<Domoticz_Switch> _devices;
		private ObservableRangeCollection<Domoticz_Switch> _switches;
		private ObservableRangeCollection<Domoticz_Switch> _security;
		private ObservableRangeCollection<Domoticz_Switch> _wakeOnLan;
		private ObservableRangeCollection<Domoticz_Switch> _utilities;
		private ObservableRangeCollection<Domoticz_Switch> _selectedSwitch;


		public bool IsDevicePortrait
		{
			get
			{
				return _isDevicePortrait;
			}
			set
			{
				_isDevicePortrait = value;
			}
		}

		public ObservableRangeCollection<Domoticz_Scene> Scenes
		{
			get
			{
				return _scenes;
			}
			set
			{
				lock (_lock)
				{
					SetProperty(ref _scenes, value);
				}
			}

		}
		public ObservableRangeCollection<Domoticz_Switch> Switches
		{
			get
			{
				return _switches;
			}
			set
			{
				lock (_lock)
				{
					SetProperty(ref _switches, value);
				}
			}
		}
		public ObservableRangeCollection<Domoticz_Switch> Security
		{
			get
			{
				return _security;
			}
			set
			{
				lock (_lock)
				{
					SetProperty(ref _security, value);
				}
			}

		}
		public ObservableRangeCollection<Domoticz_Switch> WakeOnLan
		{
			get
			{
				return _wakeOnLan;
			}
			set
			{
				lock (_lock)
				{
					SetProperty(ref _wakeOnLan, value);
				}
			}

		}
		public ObservableRangeCollection<Domoticz_Switch> Utilities
		{
			get
			{
				return _utilities;
			}
			set
			{
				lock (_lock)
				{
					SetProperty(ref _utilities, value);
				}
			}

		}
		public ObservableRangeCollection<Domoticz_Switch> SelectedSwitch
		{
			get
			{
				return _selectedSwitch;
			}
			set
			{
				lock (_lock)
				{
					SetProperty(ref _selectedSwitch, value);
				}
			}
		}


		public Domoticz_Switch SelectedSwitch2;


		public static DomoticzData Instance
		{
			get
			{
				if (_instance == null)
				{
					lock (_syncRoot)
					{
						_instance = new DomoticzData();
					}
				}
				return _instance;
			}
		}

		public DomoticzData()
		{
			_scenes = new ObservableRangeCollection<Domoticz_Scene>();
			_devices = new ObservableRangeCollection<Domoticz_Switch>();
			_switches = new ObservableRangeCollection<Domoticz_Switch>();
			_security = new ObservableRangeCollection<Domoticz_Switch>();
			_wakeOnLan = new ObservableRangeCollection<Domoticz_Switch>();
			_utilities = new ObservableRangeCollection<Domoticz_Switch>();
			_selectedSwitch = new ObservableRangeCollection<Domoticz_Switch>();
		}
	}
}
