using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfDemo.Common.Events;
using WpfDemo.Extensions;

namespace WpfDemo.ViewModels
{
    /// <summary>
    /// 页面显示加载中 导航
    /// </summary>
    public class NavigationViewModel : BindableBase, INavigationAware
    {
        private readonly IContainerProvider containerProvider;
        private readonly IEventAggregator eventAggregator;

        public NavigationViewModel(IContainerProvider containerProvider)
        {
            this.containerProvider = containerProvider;
        }

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

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
        public void UpdateLoading(bool isOpen)
        {
            eventAggregator.UpdateLoading(new UpdateModel()
            {
                IsOpen = isOpen
            });
        }
    }
}
