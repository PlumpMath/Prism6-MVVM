//using ElevatorApp.SharedLayer.Enum;
//using ElevatorApp.SharedLayer.Interface;
//using Prism.Mvvm;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace ElevatorApp.SharedLayer
//{
//    /// <summary>
//    /// Class ElevatorControlSystem.
//    /// </summary>
//    /// <seealso cref="Prism.Mvvm.BindableBase" />
//    /// <seealso cref="ElevatorApp.SharedLayer.Interface.IElevatorControlSystemFactory" />
//    public class ElevatorControlSystem : BindableBase, IElevatorControlSystemFactory
//    {

//        /// <summary>
//        /// The _number of floors
//        /// </summary>
//        private int _numberOfFloors = 0;
//        /// <summary>
//        /// The _elevators
//        /// </summary>
//        private Elevator _elevators;
//        /// <summary>
//        /// The _drop locations
//        /// </summary>
//        private Dictionary<string, int> _dropLocations;

//        private EnumElevatorDirection _direction;
//        /// <summary>
//        /// The _lock
//        /// </summary>
//        private Object _lock = new Object();
//        /// <summary>
//        /// Initializes a new instance of the <see cref="ElevatorControlSystem"/> class.
//        /// </summary>
//        /// <param name="numberOfFloors">The number of floors.</param>
//        public ElevatorControlSystem(int numberOfFloors)
//        {
//            this._numberOfFloors = numberOfFloors;
//            this._elevators = new Elevator(0);
//            this._dropLocations = new Dictionary<string, int>();
//        }
//        /// <summary>
//        /// Nexts the destination.
//        /// </summary>
//        /// <param name="direction">The direction.</param>
//        /// <param name="floorNo">The floor no.</param>
//        /// <returns>System.Int32.</returns>
//        public Dictionary<string,int> NextDestination(string direction, int floorNo)
//        {
//            return this._elevators.NextDestination(direction, floorNo);
//        }
//        /// <summary>
//        /// Floors the press.
//        /// </summary>
//        /// <param name="direction">The direction.</param>
//        /// <param name="floorNo">The floor no.</param>
//        public void RequestFloor(string direction, int floorNo)
//        {
//            this._elevators.AddNewFloorDestination(direction, floorNo);
//        }

//        /// <summary>
//        /// Current floor of the Elevator.
//        /// </summary>
//        /// <returns>System.Int32.</returns>
//        public int CurrentFloor()
//        {
//            return this._elevators.CurrentFloor;
//        }


//        /// <summary>
//        /// Directions the specified floor no UP/Down.
//        /// </summary>
//        /// <param name="floorNo">The floor no.</param>
//        /// <returns>EnumElevatorDirection.</returns>
//        public EnumElevatorDirection Direction(int floorNo)
//        {
//            return this._elevators.Direction(floorNo);

//        }

//        /// <summary>
//        /// Destination arrived, remove the request from queue.
//        /// </summary>
//        /// <param name="direction">The direction.</param>
//        /// <param name="floorNo">The floor no.</param>
//        /// <returns><c>true</c> if XXXX, <c>false</c> otherwise.</returns>
//        public bool PopFloorDestination(string direction, int floorNo)
//        {
//            return this._elevators.PopFloorDestination(direction, floorNo);
//        }

//        /// <summary>
//        /// Requested the in Elevator and pressed the floor button in the Elevator
//        /// </summary>
//        /// <param name="direction">The direction.</param>
//        /// <param name="floorNo">The floor no.</param>
//        public void CheckedIn(string direction, int floorNo)
//        {
//           if (!_dropLocations.ContainsKey(direction))
//            {
//                _dropLocations.Add(direction, floorNo);
//            }
//            RequestFloor(direction, floorNo);
//        }

//        /// <summary>
//        /// Statuses this instance.
//        /// </summary>
//        /// <returns>EnumElevatorStatus.</returns>
//        public EnumElevatorStatus Status()
//        {
//            return (_dropLocations.Any()) ? EnumElevatorStatus.Occupied : EnumElevatorStatus.Empty;
//        }


//        /// <summary>
//        /// Elevators the process execute.
//        /// </summary>
//        /// <param name="direction">The direction.</param>
//        /// <param name="floorNo">The floor no.</param>
//        /// <returns>EnumElevatorMessage.</returns>
//        /// <exception cref="System.ArgumentOutOfRangeException">
//        /// </exception>
//        public EnumElevatorMessage ElevatorProcessExecute(string direction, int floorNo)
//        {
//            if (_elevators.GetFloorDestination(direction, floorNo))
//            {
//                switch (Status())
//                {
//                    case EnumElevatorStatus.Empty:
//                        switch (Direction(NextDestination(direction, floorNo).Select(s => s.Value).SingleOrDefault()))
//                        {
//                            case EnumElevatorDirection.Up:
//                                this._elevators.MoveUp();
//                                if (this._elevators.PopFloorDestination(_elevators.CurrentFloor + "U",
//                                    _elevators.CurrentFloor))
//                                {
//                                    return EnumElevatorMessage.stopped;
//                                }
//                                break;
//                            case EnumElevatorDirection.Down:
//                                this._elevators.MoveDown();
//                                if (this._elevators.PopFloorDestination(_elevators.CurrentFloor + "D",
//                                    _elevators.CurrentFloor))
//                                {
//                                    return EnumElevatorMessage.stopped;
//                                }
//                                break;
//                            case EnumElevatorDirection.Hold:
//                                // TODO: Check timer here to alert users that they are holding the door open to long
//                                // TODO: Emergency situation where elevator can't be used
//                                // TODO: Maintenance Mode e.g. movers or maintenance people
//                                break;
//                            default:
//                                throw new ArgumentOutOfRangeException();
//                        }
//                        break;
//                    case EnumElevatorStatus.Occupied:
//                        switch (Direction(NextDestination(direction, floorNo).Select(s => s.Value).SingleOrDefault()))
//                        {
//                            case EnumElevatorDirection.Up:
//                                this._elevators.MoveUp();
//                                if (this._elevators.PopFloorDestination(_elevators.CurrentFloor + "U",
//                                    _elevators.CurrentFloor))
//                                {

//                                    if (_dropLocations.ContainsKey(_elevators.CurrentFloor + "U"))
//                                    {
//                                        lock (_lock)
//                                        {
//                                            _dropLocations.Remove(direction);
//                                        }
//                                    }
//                                    return EnumElevatorMessage.stopped;
//                                }
//                                break;
//                            case EnumElevatorDirection.Down:
//                                this._elevators.MoveDown();
//                                if (this._elevators.PopFloorDestination(_elevators.CurrentFloor + "D",
//                                    _elevators.CurrentFloor))
//                                {
//                                    if (_dropLocations.ContainsKey(_elevators.CurrentFloor + "D"))
//                                    {
//                                        lock (_lock)
//                                        {
//                                            _dropLocations.Remove(direction);
//                                        }
//                                    }
//                                    return EnumElevatorMessage.stopped;
//                                }
//                                break;
//                            case EnumElevatorDirection.Hold:
//                                // TODO: Check timer here to alert users that they are holding the door open to long
//                                // TODO: Emergency situation where elevator can't be used
//                                // TODO: Maintenance Mode e.g. movers or maintenance people
//                                break;
//                            default:
//                                throw new ArgumentOutOfRangeException();
//                        }
//                        break;
//                }
//            }
//            return EnumElevatorMessage.Moving;
//        }
//    }
//}
