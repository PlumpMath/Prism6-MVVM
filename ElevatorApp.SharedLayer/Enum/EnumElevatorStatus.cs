using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorApp.SharedLayer.Enum
{
    /// <summary>
    /// Enum EnumElevatorStatus
    /// </summary>
    public enum EnumElevatorStatus
    {
        Moving,
        Idle,
        Stopped,
        Starting,
        DestinationReached,
        MovingDown,
        MovingUp,
        OpenDoor,
        CloseDoor,
        LastFloor,
        GroundFloor,
        Stopping,
        Hold
    }
}
