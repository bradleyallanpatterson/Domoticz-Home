using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomoticzHome.ViewModels
{
    public class SwitchPageViewModel : ViewModelBase
    {
        public SwitchPageViewModel ( INavigationService navigationService )
            : base ( navigationService )
        {
            Title = "Switch Page";
        }
    }
}
