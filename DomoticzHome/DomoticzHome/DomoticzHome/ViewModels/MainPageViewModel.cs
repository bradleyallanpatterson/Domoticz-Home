using Prism.Navigation;

namespace DomoticzHome.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {


        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Title = "Main Page";
        }
    }
}
