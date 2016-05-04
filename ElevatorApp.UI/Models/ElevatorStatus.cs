using ElevatorApp.SharedLayer.Enum;

namespace ElevatorApp.UI.Models
{
    /// <summary>
    /// Class ElevatorStatus is used to communicate: Elevator to UI
    /// </summary>
    public class ElevatorStatus
    {
        public int FloorNo { get; set; }
       public EnumElevatorDirection Direction { get; set; }
        public EnumElevatorStatus Message { get; set; }

    }
}
