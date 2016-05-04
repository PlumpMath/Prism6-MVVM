using ElevatorApp.UI.Models;
using Prism.Events;

namespace ElevatorApp.UI.Events
{
    /// <summary>
    /// Class ElevatorControlSystemBackgroundEvent.
    /// </summary>
    /// <seealso cref="Prism.Events.PubSubEvent{Elevator.UI.Models.ElevatorMessage}" />
    class ElevatorControlSystemBackgroundEvent : PubSubEvent<ElevatorMessage>
    {
    }
}
