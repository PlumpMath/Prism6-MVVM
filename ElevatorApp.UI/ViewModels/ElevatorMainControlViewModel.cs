using Prism.Mvvm;
using Prism.Commands;
using Prism.Regions;
using Prism.Events;
using Prism.Logging;
using ElevatorApp.UI.Events;
using ElevatorApp.UI.Models;

namespace ElevatorApp.UI.ViewModels
{

    /// <summary>
    /// Class ElevatorMainControlViewModel.
    /// </summary>
    /// <seealso cref="Prism.Mvvm.BindableBase" />
    public class ElevatorMainControlViewModel : BindableBase
    {
        #region fields
        public DelegateCommand<string> NavigateCommand { get; set; }
        private readonly IRegionManager _regionManager;
        readonly IEventAggregator _eventAggregator;
        private string _elevatorDirection;
        private bool _directionDown;
        private string _elevatorFloor;
        private string _elevatorStatus;
        #endregion
        public ElevatorMainControlViewModel(IEventAggregator eventAggregator, IRegionManager regionManager, ILoggerFacade logger)
        {
            logger.Log("ElevatorMainControlViewModel Configured started", Category.Info, Priority.None);
            this._regionManager = regionManager;
            this._eventAggregator = eventAggregator;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            logger.Log("ElevatorMainControlViewModel Configured ended", Category.Info, Priority.None);
            this._eventAggregator.GetEvent<ElevatorControlMessageSystemEvent>().Subscribe(UpdateElevatorControlSystem, ThreadOption.UIThread, true);
        }

        #region Properties
        public string ElevatorDirection
        {
            get
            {
                return this._elevatorDirection;
            }
            set
            {
                _elevatorDirection = value;
                SetProperty(ref this._elevatorDirection, value);
                OnPropertyChanged("ElevatorDirection");
            }
        }

        public bool DirectionDown
        {
            get
            {
                return this._directionDown;
            }
            set
            {
                _directionDown = value;
                SetProperty(ref this._directionDown, value);
                OnPropertyChanged("DirectionDown");
            }
        }

        public string ElevatorStatus
        {
            get
            {
                return this._elevatorStatus;
            }
            set
            {
                _elevatorFloor = value;
                SetProperty(ref this._elevatorStatus, value);
                OnPropertyChanged("ElevatorStatus");
            }
        }
        public string ElevatorFloor
        {
            get
            {
                return this._elevatorFloor;
            }
            set
            {
                _elevatorFloor = value;
                SetProperty(ref this._elevatorFloor, value);
                OnPropertyChanged("ElevatorFloor");
            }
        }

        #endregion  Properties

        #region ControlNavigate methods
        
        //TODO: switching the control between to view. will be implemented at later stage.
        private void Navigate(string Url)
        {
            if (Url == "ElevatorView")
            {
                _regionManager.RequestNavigate(RegionNames.MainRegion, Url);
                _regionManager.RequestNavigate(RegionNames.MainRegion, Url);

            }
            else if (Url == "ElevatorDoorView")
            {

                _regionManager.RequestNavigate(RegionNames.MainRegion, Url);

            }
        }
        private void UpdateElevatorControlSystem(ElevatorStatus elevatorStatus)
        {
            ElevatorFloor = $"Current Floor : {elevatorStatus.FloorNo} ";
            ElevatorStatus = $"Elevator Status : {elevatorStatus.Message}".Replace("_"," ");
            ElevatorDirection = $"Direction : {elevatorStatus.Direction} ";
        }
        #endregion
    }
}