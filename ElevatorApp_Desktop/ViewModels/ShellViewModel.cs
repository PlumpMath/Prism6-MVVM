// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System.ComponentModel;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Logging;

namespace ElevatorApp.Shell.ViewModels
{
    /// <summary>
    /// Class ShellViewModel.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class ShellViewModel : BindableBase
    {
        public ShellViewModel(ILoggerFacade logger)
        {

            logger.Log("ShellViewModel Configured started", Category.Info, Priority.None);
            logger.Log("ShellViewModel Configured Ended", Category.Info, Priority.None);
        }

    }
}