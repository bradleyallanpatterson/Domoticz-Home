using Prism.Mvvm;

namespace DomoticzHome.Models
{
    public class Domoticz_Response : BindableBase
    {
        private string _message;
        private string _status;
        private string _title;

        public string message { get { return _message; } set { SetProperty(ref _message, value); } }
        public string status { get { return _status; } set { SetProperty(ref _status, value); } }
        public string title { get { return _title; } set { SetProperty(ref _title, value); } }
    }
}
