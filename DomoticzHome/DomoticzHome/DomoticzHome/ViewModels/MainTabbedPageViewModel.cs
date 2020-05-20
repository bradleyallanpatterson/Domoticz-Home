using Prism.Mvvm;
using Prism.Navigation;

namespace DomoticzHome.ViewModels
{
    public class MainTabbedPageViewModel : ViewModelBase
    { 
        public MainTabbedPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Tabbed";
        }
    }
}
