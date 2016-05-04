using ElevatorApp.SharedLayer.Interface;
using Prism.Events;
using Prism.Logging;
using Prism.Regions;

namespace ElevatorApp.UI.ViewModels
{
    /// <summary>
    /// Class ElevatorDoorViewModel.
    /// </summary>
    public class ElevatorDoorViewModel
    {
        //TODO: Elevator Door Open and Close need to implemented at later stage
        //TODO: Display Elevator door and open upon clicking
        public ElevatorDoorViewModel(ILoggerFacade logger)
        {
            logger.Log("ElevatorDoorViewModel Configured started", Category.Info, Priority.None);
            logger.Log("ElevatorDoorViewModel Configured ended", Category.Info, Priority.None);
        }
    }
}
