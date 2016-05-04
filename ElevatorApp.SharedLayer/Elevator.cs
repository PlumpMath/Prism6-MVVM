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
    //    /// <summary>
    //    /// Class Elevator.
    //    /// </summary>
    //    /// <seealso cref="ElevatorApp.SharedLayer.Interface.IElevatorFactory" />
    //    public class Elevator : IElevatorFactory
    //    {
    //        /// <summary>
    //        /// The _current floor
    //        /// </summary>
    //        private int _currentFloor;
    //        /// <summary>
    //        /// Dictionary for requested floors
    //        /// </summary>
    //        private Dictionary<string, int> _destinationFloors;
    //        /// <summary>
    //        /// The _current direction
    //        /// </summary>
    //        private EnumElevatorDirection _currentDirection;
    //        /// <summary>
    //        /// The lock
    //        /// </summary>
    //        private Object Lock = new Object();
    //        // private EnumElevatorDirection _direction;
    //        /// <summary>
    //        /// Initializes a new instance of the <see cref="Elevator"/> class.
    //        /// </summary>
    //        /// <param name="currentFloor">The current floor.</param>
    //        public Elevator(int currentFloor)
    //        {
    //            _currentFloor = currentFloor;
    //            _destinationFloors = new Dictionary<string, int>();
    //        }

    //        /// <summary>
    //        /// Identifies next floor destination.
    //        /// Identifies any other request on the same way
    //        /// </summary>
    //        /// <param name="direction">The direction.</param>
    //        /// <param name="floorNo">The floor no.</param>
    //        /// <returns>System.Int32.</returns>
    //        public Dictionary<string, int> NextDestination(string direction, int floorNo)
    //        {
    //            if (_destinationFloors.Count > 0)
    //            {
    //                if (_destinationFloors.Count == 1)
    //                {
    //                    return new Dictionary<string, int>
    //                    {
    //                        { _destinationFloors.Select(w => w.Key).FirstOrDefault(),
    //                           _destinationFloors.Select(w => w.Value).FirstOrDefault()
    //                        }};
    //                }
    //                else if (direction.Last().ToString().Contains("U"))
    //                {
    //                    return new Dictionary<string, int>
    //                    {
    //                        { _destinationFloors.Where(w => w.Value <= 12 && w.Key.EndsWith(direction.Last().ToString())).Select(w => w.Key).FirstOrDefault(),
    //                        _destinationFloors.Where(w => w.Value <= 12 && w.Key.EndsWith(direction.Last().ToString())).Select(w => w.Value).FirstOrDefault()
    //                        }};
    //                }
    //                else if (direction.Last().ToString().Contains("D"))
    //                {

    //                    return new Dictionary<string, int>
    //                    {
    //                        { _destinationFloors.Where(w => w.Value >= 0 && w.Key.EndsWith(direction.Last().ToString())).Select(w => w.Key).FirstOrDefault(),
    //                        _destinationFloors.Where(w => w.Value >= 0 && w.Key.EndsWith(direction.Last().ToString())).Select(w => w.Value).FirstOrDefault()
    //                        }};

    //                }
    //                return new Dictionary<string, int> {{"-1",-1}};
    //            }
    //            //Default value mapping with hold status
    //            return new Dictionary<string, int> {{"-1",-1} };
    //        }

    //        /// <summary>
    //        /// Gets or sets the current floor.
    //        /// </summary>
    //        /// <value>The current floor.</value>
    //        public int CurrentFloor
    //        {
    //            get { return this._currentFloor; }
    //            set { this._currentFloor = value; }
    //        }

    //        /// <summary>
    //        /// Removes the floor destination from the Dictionary.
    //        /// </summary>
    //        /// <param name="direction">The direction.</param>
    //        /// <param name="floorNo">The floor no.</param>
    //        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
    //        public bool PopFloorDestination(string direction, int floorNo)
    //        {
    //            if (_destinationFloors.ContainsKey(direction))
    //            {
    //                lock (Lock)
    //                {
    //                    _destinationFloors.Remove(direction);
    //                    return true;
    //                }
    //            }
    //            {
    //                return false;
    //            }
    //        }
    //        /// <summary>
    //        /// Adds the new floor destination.
    //        /// </summary>
    //        /// <param name="direction">The direction.</param>
    //        /// <param name="floorNo">The floor no.</param>
    //        public void AddNewFloorDestination(string direction, int floorNo)
    //        {
    //            if (!_destinationFloors.ContainsKey(direction))
    //            {
    //                _destinationFloors.Add(direction, floorNo);
    //            }
    //        }

    //        /// <summary>
    //        /// Adds the new floor destination.
    //        /// </summary>
    //        /// <param name="direction">The direction.</param>
    //        /// <param name="floorNo">The floor no.</param>
    //        public bool GetFloorDestination(string direction, int floorNo)
    //        {
    //            return _destinationFloors.ContainsKey(direction);

    //        }
    //        /// <summary>
    //        /// Directions the specified floor no.
    //        /// </summary>
    //        /// <param name="floorNo">The floor no.</param>
    //        /// <returns>EnumElevatorDirection.</returns>
    //        public EnumElevatorDirection Direction(int floorNo)
    //        {
    //            if (_destinationFloors.Count > 0)
    //            {
    //                if (_currentFloor < floorNo)
    //                {
    //                    return EnumElevatorDirection.Up;
    //                }
    //                else if (_currentFloor > floorNo)
    //                {
    //                    return EnumElevatorDirection.Down;
    //                }
    //            }
    //            return EnumElevatorDirection.Hold;
    //        }

    //        /// <summary>
    //        /// Moves one floor down.
    //        /// </summary>
    //        public void MoveDown()
    //        {
    //            _currentFloor--;
    //        }

    //        /// <summary>
    //        /// Moves one floor up.
    //        /// </summary>
    //        public void MoveUp()
    //        {
    //            _currentFloor++;
    //        }
    //    }
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
