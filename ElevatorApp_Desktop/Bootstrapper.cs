using System;
using System.Windows;
using Microsoft.Practices.Unity;
using Prism.Unity;
using ElevatorApp.Shell.Views;
using Prism.Modularity;
using Prism.Logging;
using ElevatorApp.SharedLayer.Logger;
using ElevatorApp.UI;

namespace ElevatorApp.Shell
{
    /// <summary>
    /// Class ElevatorAppBootstrapper.
    /// Using the Unity container, you can register a set of mappings that determine what concrete type you require 
    /// when a constructor (or property or method) identifies the type to be injected by an interface type or base class type
    /// </summary>
    /// <seealso cref="Prism.Unity.UnityBootstrapper" />
    public class ElevatorAppBootstrapper : UnityBootstrapper
    {
        // TODO: 02 - The Shell loads the ElevatorApp.UI, as specified in the module catalog (ModuleCatalog.xaml).
        #region  Methods

        /// <summary>
        /// Configures the module catalog. Modularity is a one best approach for big scale projects 
        /// </summary>
        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();
            ModuleCatalog moduleCatalog = (ModuleCatalog)this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(ModuleInit));
        }
        protected override DependencyObject CreateShell()
        {
           return Container.Resolve<ShellView>();
        }
        protected override void InitializeShell()
        {
          Application.Current.MainWindow.Show();
        }
        protected override ILoggerFacade CreateLogger()
        {
            return new Log4NetLogger();
        }
        #endregion Methods

    }
}