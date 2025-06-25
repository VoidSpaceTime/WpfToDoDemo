using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.Common
{
    public interface IDialogHostService : IDialogService
    {
        Task<IDialogResult> ShowDialogAsync(string name, IDialogParameters parameters, string regionName = "Root");
    }
}
