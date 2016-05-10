using ElevatorApp.SharedLayer.Enum;
using ElevatorApp.SharedLayer.Interface;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using ElevatorApp.UI.Events;
using ElevatorApp.UI.Models;
using Prism.Mvvm;

namespace ElevatorApp.UI.ViewModels
{
    /// <summary>
    /// Class ElevatorViewModel.
    /// </summary>
    public class ElevatorViewModel:BindableBase
    {
        #region Fields

        private bool _isChecked1 = false;
        private bool _isChecked2 = false;
        private bool _isChecked3 = false;
        private bool _isChecked4 = false;
        private bool _isChecked5 = false;
        private bool _isChecked6 = false;
        private bool _isChecked7 = false;
        private bool _isChecked8 = false;
        private bool _isChecked9 = false;
        private bool _isChecked10 = false;
        private bool _isChecked11 = false;
        private bool _isChecked12 = false;
        private bool _isChecked0= false;
       
        public DelegateCommand<string> NavigateCommand { get; set; }
        private readonly ILoggerFacade _logger;
        private readonly IEventAggregator _eventAggregator;
        #endregion  Fields
        public ElevatorViewModel(IEventAggregator eventAggregator, ILoggerFacade logger)
        {
            logger.Log("ElevatorViewModel Configured started", Category.Info, Priority.None);
            this._eventAggregator = eventAggregator;
            this._logger = logger;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            this._eventAggregator.GetEvent<ElevatorControlMessageSystemEvent>().Subscribe(UpdateElevatorControlSystem, ThreadOption.BackgroundThread, false);
            logger.Log("ElevatorViewModel Configured ended", Category.Info, Priority.None);
        }

        private void UpdateElevatorControlSystem(ElevatorStatus elevatorStatus)
        {
            switch (elevatorStatus.FloorNo)
            {
                case 0:
                    IsChecked0 = false;
                    break;
                case 1:
                    IsChecked1 = false;
                    break;
                case 2:
                    IsChecked2 = false;
                    break;
                case 3:
                    IsChecked3 = false;
                    break;
                case 4:
                    IsChecked4 = false;
                    break;
                case 5:
                    IsChecked5 = false;
                    break;
                case 6:
                    IsChecked6 = false;
                    break;
                case 7:
                    IsChecked7 = false;
                    break;
                case 8:
                    IsChecked8 = false;
                    break;
                case 9:
                    IsChecked9 = false;
                    break;
                case 10:
                    IsChecked10 = false;
                    break;
                case 11:
                    IsChecked11 = false;
                    break;
                case 12:
                    IsChecked12 = false;
                    break;
            }
            ;
        }

        #region  Methods
        private void Navigate(string inElevatorPress)
        {

            ElevatorMessage elevatorMessage = new ElevatorMessage();
            switch (inElevatorPress)
            {
                case "0":
                    elevatorMessage.DestinationFloor = 0;
                    break;
                case "1":
                    elevatorMessage.DestinationFloor = 1;
                    break;
                case "2":
                    elevatorMessage.DestinationFloor = 2;
                    break;
                case "3":
                    elevatorMessage.DestinationFloor = 3;
                    break;
                case "4":
                    elevatorMessage.DestinationFloor = 4;
                    break;
                case "5":
                    elevatorMessage.DestinationFloor = 5;
                    break;
                case "6":
                    elevatorMessage.DestinationFloor = 6;
                    break;
                case "7":
                    elevatorMessage.DestinationFloor = 7;
                    break;
                case "8":
                    elevatorMessage.DestinationFloor = 8;
                    break;
                case "9":
                    elevatorMessage.DestinationFloor = 9;
                    break;
                case "10":
                    elevatorMessage.DestinationFloor = 10;
                    break;
                case "11":
                    elevatorMessage.DestinationFloor = 11;
                    break;
                case "12":
                    elevatorMessage.DestinationFloor = 12;
                    break;
            }
            
            elevatorMessage.RequestType = EnumElevatorRequestType.Elevator;
            this._eventAggregator.GetEvent<ElevatorControlSystemBackgroundEvent>().Publish(elevatorMessage);
        }
        #endregion Methods

        #region  Properties
        public bool IsChecked1
        {
            get
            {
                return this._isChecked1;
            }
            set
            {
                _isChecked1 = value;
                SetProperty(ref this._isChecked1, value);
                OnPropertyChanged("IsChecked1");
            }
        }
        public bool IsChecked2
        {
            get
            {
                return this._isChecked2;
            }
            set
            {
                _isChecked2 = value;
                SetProperty(ref this._isChecked2, value);
                OnPropertyChanged("IsChecked2");
            }
        }
        public bool IsChecked3
        {
            get
            {
                return this._isChecked3;
            }
            set
            {
                _isChecked3 = value;
                SetProperty(ref this._isChecked3, value);
                OnPropertyChanged("IsChecked3");
            }
        }
        public bool IsChecked4
        {
            get
            {
                return this._isChecked4;
            }
            set
            {
                _isChecked4 = value;
                SetProperty(ref this._isChecked4, value);
                OnPropertyChanged("IsChecked4");
            }
        }
        public bool IsChecked5
        {
            get
            {
                return this._isChecked5;
            }
            set
            {
                _isChecked5 = value;
                SetProperty(ref this._isChecked5, value);
                OnPropertyChanged("IsChecked5");
            }
        }
        public bool IsChecked6
        {
            get
            {
                return this._isChecked6;
            }
            set
            {
                _isChecked6 = value;
                SetProperty(ref this._isChecked6, value);
                OnPropertyChanged("IsChecked6");
            }
        }
        public bool IsChecked7
        {
            get
            {
                return this._isChecked7;
            }
            set
            {
                _isChecked7 = value;
                SetProperty(ref this._isChecked7, value);
                OnPropertyChanged("IsChecked7");
            }
        }
        public bool IsChecked8
        {
            get
            {
                return this._isChecked8;
            }
            set
            {
                _isChecked8 = value;
                SetProperty(ref this._isChecked8, value);
                OnPropertyChanged("IsChecked8");
            }
        }
        public bool IsChecked9
        {
            get
            {
                return this._isChecked9;
            }
            set
            {
                _isChecked1 = value;
                SetProperty(ref this._isChecked9, value);
                OnPropertyChanged("IsChecked9");
            }
        }
        public bool IsChecked10
        {
            get
            {
                return this._isChecked10;
            }
            set
            {
                _isChecked1 = value;
                SetProperty(ref this._isChecked10, value);
                OnPropertyChanged("IsChecked10");
            }
        }
        public bool IsChecked11
        {
            get
            {
                return this._isChecked11;
            }
            set
            {
                _isChecked11 = value;
                SetProperty(ref this._isChecked11, value);
                OnPropertyChanged("IsChecked11");
            }
        }

        public bool IsChecked12
        {
            get
            {
                return this._isChecked12;
            }
            set
            {
                _isChecked12 = value;
                SetProperty(ref this._isChecked12, value);
                OnPropertyChanged("IsChecked12");
            }
        }

        public bool IsChecked0
        {
            get
            {
                return this._isChecked0;
            }
            set
            {
                _isChecked1 = value;
                SetProperty(ref this._isChecked0, value);
                OnPropertyChanged("IsChecked0");
            }
        }
        #endregion Properties
    }
}
