using ElevatorApp.SharedLayer.Enum;

namespace ElevatorApp.UI.Models
{
    /// <summary>
    /// Class ElevatorMessage is used to  communicate : UI to Elevator
    /// </summary>
    public class ElevatorMessage
    {
        public int DestinationFloor { get; set; }
        public EnumElevatorDirection Direction { get; set; }
        public EnumElevatorRequestType RequestType { get; set; }
    }
}
