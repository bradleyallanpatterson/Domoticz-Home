using System.Collections.Generic;

namespace DomoticzHome.Models
{
    public class Domoticz_Scene_Response : Domoticz_Times
    {
        private int _actTime;
        private bool _allowWidgetOrdering;
        private ObservableRangeCollection<Domoticz_Scene> _result;

        public int ActTime { get { return _actTime; } set { SetProperty(ref _actTime, value); } }
        public bool AllowWidgetOrdering { get { return _allowWidgetOrdering; } set { SetProperty(ref _allowWidgetOrdering, value); } }
#pragma warning disable IDE1006 // Naming Styles
        public ObservableRangeCollection<Domoticz_Scene> result { get { return _result; } set { SetProperty(ref _result, value); } }
#pragma warning restore IDE1006 // Naming Styles


    }
}
