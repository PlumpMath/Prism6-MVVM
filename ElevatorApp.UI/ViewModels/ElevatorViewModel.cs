using ElevatorApp.SharedLayer.Enum;
using ElevatorApp.SharedLayer.Interface;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using ElevatorApp.UI.Events;
using ElevatorApp.UI.Models;

namespace ElevatorApp.UI.ViewModels
{
    /// <summary>
    /// Class ElevatorViewModel.
    /// </summary>
    public class ElevatorViewModel
    {
        #region Fields
        public DelegateCommand<string> NavigateCommand { get; set; }
        private readonly IEventAggregator _eventAggregator;
        #endregion  Fields
        public ElevatorViewModel(IEventAggregator eventAggregator, ILoggerFacade logger)
        {
            this._eventAggregator = eventAggregator;
            NavigateCommand = new DelegateCommand<string>(Navigate);
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
    }
}
