
using ElevatorApp.SharedLayer.Enum;
using Prism.Commands;
using Prism.Events;
using Prism.Logging;
using ElevatorApp.UI.Events;
using ElevatorApp.UI.Models;


namespace ElevatorApp.UI.ViewModels
{
    /// <summary>
    /// Class ElevatorFloorViewModel.
    /// PRISM6 MVVM pattern implemented
    /// Navigating with UI through DelegateCommand command
    /// </summary>
  
    
    public class ElevatorFloorViewModel
    {
        #region Fields
        public DelegateCommand<string> NavigateCommand { get; set; }
        private readonly IEventAggregator _eventAggregator;
        #endregion Fields
        public ElevatorFloorViewModel(IEventAggregator eventAggregator, ILoggerFacade logger)
        {
            logger.Log("ElevatorDoorViewModel Configured started", Category.Info, Priority.None);
            
            this._eventAggregator = eventAggregator;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            
            logger.Log("ElevatorDoorViewModel Configured ended", Category.Info, Priority.None);
        }
        #region Method
        private void Navigate(string floorPress)
        {
            ElevatorMessage elevatorPress = new ElevatorMessage();
            switch (floorPress)
            {
                case "0U":
                    elevatorPress.Direction = EnumElevatorDirection.Up; 
                    elevatorPress.DestinationFloor = 0;
                    break;
                case "1U":
                    elevatorPress.Direction = EnumElevatorDirection.Up;
                    elevatorPress.DestinationFloor = 1;
                    break;
                case "1D":
                    elevatorPress.Direction = EnumElevatorDirection.Down;
                    elevatorPress.DestinationFloor = 1;
                    break;
                case "2U":
                    elevatorPress.Direction = EnumElevatorDirection.Up;
                    elevatorPress.DestinationFloor = 2;
                    break;
                case "2D":
                    elevatorPress.Direction = EnumElevatorDirection.Down;
                    elevatorPress.DestinationFloor = 2;
                    break;
                case "3U":
                    elevatorPress.Direction = EnumElevatorDirection.Up;
                    elevatorPress.DestinationFloor = 3;
                    break;
                case "3D":
                    elevatorPress.Direction = EnumElevatorDirection.Down;
                    elevatorPress.DestinationFloor = 3; ;
                    break;
                case "4U":
                    elevatorPress.Direction = EnumElevatorDirection.Up;
                    elevatorPress.DestinationFloor = 4;
                    break;
                case "4D":
                    elevatorPress.Direction = EnumElevatorDirection.Down;
                    elevatorPress.DestinationFloor = 4;
                    break;
                case "5U":
                    elevatorPress.Direction = EnumElevatorDirection.Up;
                    elevatorPress.DestinationFloor = 5;
                    break;
                case "5D":
                    elevatorPress.Direction = EnumElevatorDirection.Down;
                    elevatorPress.DestinationFloor = 5;
                    break;
                case "6U":
                    elevatorPress.Direction = EnumElevatorDirection.Up;
                    elevatorPress.DestinationFloor = 6;
                    break;
                case "6D":
                    elevatorPress.Direction = EnumElevatorDirection.Down;
                    elevatorPress.DestinationFloor = 6;
                    break;
                case "7U":
                    elevatorPress.Direction = EnumElevatorDirection.Up;
                    elevatorPress.DestinationFloor = 7;
                    break;
                case "7D":
                    elevatorPress.Direction = EnumElevatorDirection.Down;
                    elevatorPress.DestinationFloor = 7;
                    break;
                case "8U":
                    elevatorPress.Direction = EnumElevatorDirection.Up;
                    elevatorPress.DestinationFloor = 8;
                    break;
                case "8D":
                    elevatorPress.Direction = EnumElevatorDirection.Down;
                    elevatorPress.DestinationFloor = 8;
                    break;
                case "9U":
                    elevatorPress.Direction = EnumElevatorDirection.Up;
                    elevatorPress.DestinationFloor = 9;
                    break;
                case "9D":
                    elevatorPress.Direction = EnumElevatorDirection.Down;
                    elevatorPress.DestinationFloor = 9;
                    break;
                case "10U":
                    elevatorPress.Direction = EnumElevatorDirection.Up;
                    elevatorPress.DestinationFloor = 10;
                    break;
                case "10D":
                    elevatorPress.Direction = EnumElevatorDirection.Down;
                    elevatorPress.DestinationFloor = 10;
                    break;
                case "11U":
                    elevatorPress.Direction = EnumElevatorDirection.Up;
                    elevatorPress.DestinationFloor = 11;
                    break;
                case "11D":
                    elevatorPress.Direction = EnumElevatorDirection.Down;
                    elevatorPress.DestinationFloor = 11; 
                    break;
                case "12D":
                    elevatorPress.Direction = EnumElevatorDirection.Down; 
                    elevatorPress.DestinationFloor = 12;
                    break;
                
            }

            //Publishing UI request to Elevator Control system(ref:ElevatorSystemController)
            elevatorPress.RequestType = EnumElevatorRequestType.Floor;
            this._eventAggregator.GetEvent<ElevatorControlSystemBackgroundEvent>().Publish(elevatorPress);
        }
        #endregion Method

    }
}
