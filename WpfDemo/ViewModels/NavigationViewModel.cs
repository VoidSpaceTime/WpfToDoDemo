using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.ViewModels
{
    /// <summary>
    /// 页面显示加载中 导航
    /// </summary>
    public class NavigationViewModel : INavigationAware
    {
        private readonly IContainerProvider containerProvider;
        private readonly IEventAggregator eventAggregator;
        public NavigationViewModel(IContainerProvider containerProvider, IEventAggregator eventAggregator)
        {
            this.containerProvider = containerProvider;
            this.eventAggregator = eventAggregator;
            //this.eventAggregator = containerProvider.Resolve<IEventAggregator>();
        }
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true; 
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }
    }
}
