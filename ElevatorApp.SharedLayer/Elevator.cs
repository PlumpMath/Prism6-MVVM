using ElevatorApp.SharedLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorApp.SharedLayer.Enum;
using Prism.Mvvm;


namespace ElevatorApp.SharedLayer
{

    /// <summary>
    /// Class Elevator.
    /// </summary>
    /// <seealso cref="ElevatorApp.SharedLayer.Interface.IElevator" />
    public class Elevator : IElevator
    {
        #region fields

        private int _currentFloor;
        private int _minFloor;
        private int _maxFloor;
        private EnumElevatorDirection _direction;
        private EnumElevatorStatus _status;
        private List<int> _downQueue;
        private List<int> _upQueue;
        #endregion fields

        public Elevator(int minFloor, int maxFloor)
        {
            this._minFloor = minFloor;
            this._maxFloor = maxFloor;
            this._downQueue = new List<int>();
            this._upQueue = new List<int>();
            this._currentFloor = minFloor;
        }
        #region Properities

        public int CurrentFloor
        {
            get { return _currentFloor; }
        }

        public EnumElevatorDirection Direction
        {
            get { return _direction; }
        }

        public EnumElevatorStatus Status
        {
            get { return _status; }
        }

        public int GetUpQueueMax
        {
            get
            {
                return _upQueue.Count > 0 ? _upQueue.Max() : -1;
            }
        }

        public int GeUpQueueMin
        {
            get
            {
                return _upQueue.Count > 0 ? _upQueue.Min() : -1;
            }
        }

        public int GetDownQueueMax
        {
            get
            {
                return _downQueue.Count > 0 ? _downQueue.Max() : -1;
            }
        }

        public int GetDownQueueMin
        {
            get { return _downQueue.Count > 0 ? _downQueue.Min() : -1; }
        }
        #endregion Properities

        #region Private Methods

        #endregion Private Methods

        #region public methods
        public void MoveUp()
        {
            if (this._currentFloor < _maxFloor)
            {
                this._currentFloor += 1;
                _status = EnumElevatorStatus.MovingUp;
            }
            else
            {
                _status = EnumElevatorStatus.LastFloor;
            }
        }
        public void MoveDown()
        {
            if (_currentFloor > _minFloor)
            {
                this._currentFloor -= 1;
                _status = EnumElevatorStatus.MovingDown;
            }
            else
            {
                _status = EnumElevatorStatus.GroundFloor;
            }
        }
        public EnumElevatorDirection GetDirection(int destinationFloor)
        {
            switch (Direction)
            {
                case EnumElevatorDirection.Up:
                    if (destinationFloor > _currentFloor)
                    {
                        return EnumElevatorDirection.Up;

                    }
                    else
                    {
                        //TODO Need to check with Jane on this condition
                        return EnumElevatorDirection.Down;
                    }

                case EnumElevatorDirection.Down:
                    if (destinationFloor >= _currentFloor)
                    {
                        return EnumElevatorDirection.Up;
                    }
                    //TODO Need to check with Jane on this condition
                    else
                    {
                        return EnumElevatorDirection.Up;
                    }

                case EnumElevatorDirection.Hold:
                    return destinationFloor > _currentFloor ? EnumElevatorDirection.Up : EnumElevatorDirection.Down;
                case EnumElevatorDirection.Idle:
                    return destinationFloor > _currentFloor ? EnumElevatorDirection.Up : EnumElevatorDirection.Down;
                default:
                    return EnumElevatorDirection.Invalid;
            }
        }
        public bool IsRequestOperated(EnumElevatorDirection direction, int destinationFloor)
        {
            switch (direction)
            {
                case EnumElevatorDirection.Up:

                    return !_upQueue.Contains(destinationFloor);
                case EnumElevatorDirection.Down:
                    return !_downQueue.Contains(destinationFloor);
                default:
                    return true;
            }
        }
        public void SetUpQueue(EnumElevatorDirection direction, int destinationFloor)
        {
            switch (direction)
            {
                case EnumElevatorDirection.Up:
                    if (!_upQueue.Contains(destinationFloor))
                    {
                        _upQueue.Add(destinationFloor);
                    }
                    break;
                case EnumElevatorDirection.Down:
                    if (!_downQueue.Contains(destinationFloor))
                    {
                        _downQueue.Add(destinationFloor);
                    }
                    break;
            }
        }
        public void Operate(EnumElevatorDirection direction)
        {
            _direction = direction;
            switch (_direction)
            {
                case EnumElevatorDirection.Up:
                    if (_upQueue.Contains(_currentFloor))
                    {
                        _upQueue.Remove(_currentFloor);
                        _status = EnumElevatorStatus.Stopped;
                    }
                    else
                    {
                        _status = EnumElevatorStatus.Moving;
                    }

                    break;
                case EnumElevatorDirection.Down:
                    if (_downQueue.Contains(_currentFloor))
                    {
                        _downQueue.Remove(_currentFloor);
                        _status = EnumElevatorStatus.Stopped;
                    }
                    else
                    {
                        _status = EnumElevatorStatus.Moving;
                    }
                    break;
                case EnumElevatorDirection.Idle:
                    _status = EnumElevatorStatus.Idle;
                    break;
            }
        }
        #endregion public methods
    }
}
