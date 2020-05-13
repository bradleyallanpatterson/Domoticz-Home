using DomoticzHome.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DomoticzHome.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;

            CheckNetworkConnections();

            //Task.Run(() => BackgroundTasks());
        }


        public async void CheckNetworkConnections()
        {
            bool test = await DomoticzApiService.Instance.CheckNetworkConnections();
        }


        private async void BackgroundTasks()
        {
            while (true)
            {
                //if (base.Switches.Count > 0 || base._devices.Count > 0)
                //{
                //    // We ONLY update status on Switches
                //    bool result = await QuerySwitchStatus();
                //    if (result && base.Switches.Count > 0)
                //    {
                //        await Task.Delay(4000);
                //        // OnLaunchDynamicTabbedPageCommandExecuted();
                //    }
                //    await Task.Delay(4000);
                //}
                //else
                //{
                //    await Task.Delay(10000);
                //}

                await Task.Delay(10);
            }
        }



        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
