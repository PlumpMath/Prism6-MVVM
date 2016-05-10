using ElevatorApp.SharedLayer;
using ElevatorApp.SharedLayer.Interface;
using ElevatorApp.UI.Controllers;
using ElevatorApp.UI.Views;
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace ElevatorApp.UI
{
    /// <summary>
    /// Class ModuleInit. Modularity is one of the best future from Prism. 
    /// Advantages: Each module can be developed separately and can be grouped at later stage. 
    /// </summary>
    /// <seealso cref="Prism.Modularity.IModule" />
    public class ModuleInit : IModule
    {
        private readonly IUnityContainer _container;
        private readonly IRegionManager _regionManager;

        // [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        private ElevatorSystemController _elevatorSystemController;

        public ModuleInit(IUnityContainer container, IRegionManager regionManager)
        {
            this._container = container;
            this._regionManager = regionManager;
        }
        #region Initialize Mothod

        /// <summary>
        /// Initializes  instances for all views and Elevator System Controller.
        /// </summary>
        public void Initialize()
        {
            // Register the ElevatorControlSystemFactory concrete type with the container.
            //parameter 13 describes number of floor
            this._container.RegisterType<IElevator, Elevator>(new InjectionConstructor(0,12));

            // This is an example of View Discovery which associates the specified view type
            // with a region so that the view will be automatically added to the region when
            // the region is first displayed.


            // Get a reference to the main region.
            IRegion mainRegion = this._regionManager.Regions[RegionNames.MainRegion];
            if (mainRegion == null) return;

            // Check to see if we need to create an instance of the view.
            ElevatorMainControlView view = mainRegion.GetView("ElevatorMainControlView") as ElevatorMainControlView;
            if (view == null)
            {
                // Create a new instance of the ElevatorMainView using the Unity container.
                view = this._container.Resolve<ElevatorMainControlView>("ElevatorMainControlView");

                // Add the view to the main region. This automatically activates the view too.
                mainRegion.Add(view, "ElevatorMainControlView");
            }
            else
            {
                // The view has already been added to the region so just activate it.
                mainRegion.Activate(view);
            }


            this._container.RegisterTypeForNavigation<ElevatorView>("ElevatorView");
            this._container.RegisterTypeForNavigation<ElevatorFloorView>("ElevatorFloorView");
            this._container.RegisterTypeForNavigation<ElevatorDoorView>("ElevatorDoorView");


            this._regionManager.RegisterViewWithRegion(RegionNames.ElevatorRegion,
                                                      () => this._container.Resolve<ElevatorDoorView>());
            this._regionManager.RegisterViewWithRegion(RegionNames.ElevatorInRegion,
                                                       () => this._container.Resolve<ElevatorView>());
            this._regionManager.RegisterViewWithRegion(RegionNames.ElevatorFloorRegion,
                                                       () => this._container.Resolve<ElevatorFloorView>());

            //TODO: Develop a Door functionality
            this._regionManager.RequestNavigate(RegionNames.ElevatorInRegion, "ElevatorDoorView");

            this._elevatorSystemController = this._container.Resolve<ElevatorSystemController>();


        }
        #endregion Initialize Mothod
    }
}