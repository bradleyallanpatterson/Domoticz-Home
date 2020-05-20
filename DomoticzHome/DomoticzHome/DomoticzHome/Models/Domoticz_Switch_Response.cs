using System.Collections.Generic;

namespace DomoticzHome.Models
{
    public class Domoticz_Switch_Response : Domoticz_Times
    {
        private ObservableRangeCollection<Domoticz_Switch> _result;

        public ObservableRangeCollection<Domoticz_Switch> result { get { return _result; } set { SetProperty(ref _result, value); } }
    }
}
