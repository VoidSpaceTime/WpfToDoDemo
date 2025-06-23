﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfDemo.ViewModels.Dialogs
{
    internal class AddMemoViewModel : IDialogAware
    {
        public DialogCloseListener RequestClose { get; set; }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}
