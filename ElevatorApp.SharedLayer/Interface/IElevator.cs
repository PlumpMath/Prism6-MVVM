using ElevatorApp.SharedLayer.Enum;

namespace ElevatorApp.SharedLayer.Interface
{
    public interface IElevator
    {
        int CurrentFloor { get; }
        EnumElevatorDirection Direction { get; }
        int GetDownQueueMax { get; }
        int GetDownQueueMin { get; }
        int GetUpQueueMax { get; }
        int GeUpQueueMin { get; }
        EnumElevatorStatus Status { get; }

        void MoveUp();
        void MoveDown();
        EnumElevatorDirection GetDirection(int destinationFloor);
        bool IsRequestOperated(EnumElevatorDirection direction, int destinationFloor);
        void Operate(EnumElevatorDirection direction);
        void SetUpQueue(EnumElevatorDirection direction, int destinationFloor);
    }
}