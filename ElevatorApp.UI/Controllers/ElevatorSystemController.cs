using System;
using System.Linq;
using Prism.Regions;
using Prism.Events;
using Microsoft.Practices.Unity;
using ElevatorApp.SharedLayer.Interface;
using ElevatorApp.SharedLayer.Enum;
using System.Threading;
using ElevatorApp.UI.Models;
using ElevatorApp.UI.Events;
using Prism.Logging;

namespace ElevatorApp.UI.Controllers
{
    /// <summary>
    /// Controllers are typically used to programmatically add
    /// and remove views to regions using view injection.
    /// This controller subscribes to a loosely coupled event,
    /// which is published by the EmployeeListViewModel when the
    /// user selects an employee. When an employee is selected,
    /// the EmployeeSummaryView is created and added to the main
    /// region using view injection.
    /// </summary>
    public class ElevatorSystemController
    {
        #region Fields
        private readonly IEventAggregator _eventAggregator;
        private readonly ILoggerFacade _logger;
        private readonly IElevator _elevatorService;
        const int MIN_FLOOR = 0;
        const int MAX_FLOOR = 12;
        static readonly object _object = new object();
        #endregion fields

        public ElevatorSystemController(
                                    IUnityContainer container,
                                    IRegionManager regionManager,
                                    IEventAggregator eventAggregator,
                                     IElevator elevatorService,
                                     ILoggerFacade logger)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (regionManager == null) throw new ArgumentNullException("regionManager");
            if (eventAggregator == null) throw new ArgumentNullException("eventAggregator");
            if (elevatorService == null) throw new ArgumentNullException("elevatorService");
            if (logger == null) throw  new ArgumentException(" logger");
            this._eventAggregator = eventAggregator;
            this._elevatorService = elevatorService;
            this._logger = logger;

            //InitializeElevator with default value
            InitializeElevatorService();
            this._eventAggregator.GetEvent<ElevatorControlSystemBackgroundEvent>().Subscribe(ElevatorFloorControlSystem, ThreadOption.BackgroundThread, false);
        }

        #region  Methods

        /// <summary>
        /// Retrieves all the request from Elevator screen and floor screen.
        /// Process all the request in a background thread and returns back to UI screen
        /// </summary>
        /// <param name="elevatorMessage">The elevator message.</param>
        private void ElevatorFloorControlSystem(ElevatorMessage elevatorMessage)
        {
            //Floor Press direction decision
            switch (elevatorMessage.RequestType)
            {
                //Button Floor press 
                case EnumElevatorRequestType.Floor:
                    _elevatorService.SetUpQueue(elevatorMessage.Direction, elevatorMessage.DestinationFloor);
                    ElevatorProcessExecute(elevatorMessage);
                    break;
                // //Button Elevator press
                case EnumElevatorRequestType.Elevator:
                    elevatorMessage.Direction = _elevatorService.GetDirection(elevatorMessage.DestinationFloor);
                    if (elevatorMessage.Direction != EnumElevatorDirection.Invalid)
                    {
                        _elevatorService.SetUpQueue(elevatorMessage.Direction, elevatorMessage.DestinationFloor);
                        ElevatorProcessExecute(elevatorMessage);
                    }
                    break;
                default:
                    Console.Write("Invalid Request");
                    break;
            }

        }
        private void InitializeElevatorService()
        {
            this._elevatorService.Operate(EnumElevatorDirection.Idle);
            ElevatorStatus elevatorMessage = new ElevatorStatus();
            elevatorMessage.FloorNo = this._elevatorService.CurrentFloor;
            elevatorMessage.Message = this._elevatorService.Status;
            elevatorMessage.Direction = this._elevatorService.Direction;
            PublishElevatorMessage(elevatorMessage);
        }


        private void ElevatorProcessExecute(ElevatorMessage elevatorMessage)
        {
            ElevatorStatus responseMessage = new ElevatorStatus();

            try
            {
                //check if other thread processed your request 
                if (!_elevatorService.IsRequestOperated(elevatorMessage.Direction, elevatorMessage.DestinationFloor))
                {
                    //Wait in the queue to process your request until other thread request releases the lock
                    lock (_object)
                    {
                        //Now this thread got an access, need to check again for the request.

                        if (!_elevatorService.IsRequestOperated(elevatorMessage.Direction, elevatorMessage.DestinationFloor))
                        {
                            if (elevatorMessage.DestinationFloor >= _elevatorService.CurrentFloor)
                            {

                                while (elevatorMessage.DestinationFloor >= _elevatorService.CurrentFloor
                                    && _elevatorService.CurrentFloor <= MAX_FLOOR
                                     && _elevatorService.GetUpQueueMax != -1)
                                {
                                    _elevatorService.Operate(EnumElevatorDirection.Up);
                                    if (_elevatorService.Status == EnumElevatorStatus.Stopped)
                                    {
                                        responseMessage.Direction = EnumElevatorDirection.Up;
                                        responseMessage.Message = EnumElevatorStatus.Stopping;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(3000);
                                        responseMessage.Direction = EnumElevatorDirection.Up;
                                        responseMessage.Message = EnumElevatorStatus.Hold;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(1000);
                                    }
                                    else
                                    {
                                        responseMessage.Direction = EnumElevatorDirection.Up;
                                        responseMessage.Message = _elevatorService.Status;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(1000);
                                    }
                                    if (_elevatorService.GetUpQueueMax != -1)
                                        _elevatorService.MoveUp();
                                }

                                while (_elevatorService.GetUpQueueMax > elevatorMessage.DestinationFloor
                                    && _elevatorService.CurrentFloor <= MAX_FLOOR
                                    && _elevatorService.GetUpQueueMax != -1)
                                {

                                    _elevatorService.Operate(EnumElevatorDirection.Up);
                                    if (_elevatorService.Status == EnumElevatorStatus.Stopped)
                                    {
                                        responseMessage.Direction = EnumElevatorDirection.Up;
                                        responseMessage.Message = EnumElevatorStatus.Stopping;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(3000);
                                        responseMessage.Direction = _elevatorService.Direction;
                                        responseMessage.Message = EnumElevatorStatus.Hold;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(1000);
                                    }
                                    else
                                    {
                                        responseMessage.Direction = EnumElevatorDirection.Up;
                                        responseMessage.Message = _elevatorService.Status;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                    }
                                    if (_elevatorService.GetUpQueueMax != -1)
                                        _elevatorService.MoveUp();
                                }

                                //Change direction if no more request from top floor
                                while (_elevatorService.GetDownQueueMax >= _elevatorService.CurrentFloor
                                    && _elevatorService.CurrentFloor >= MIN_FLOOR
                                    && _elevatorService.GetDownQueueMax != -1)
                                {
                                    _elevatorService.Operate(EnumElevatorDirection.Down);
                                    _elevatorService.Operate(EnumElevatorDirection.Up);
                                    if (_elevatorService.Status == EnumElevatorStatus.Stopped)
                                    {
                                        responseMessage.Direction = EnumElevatorDirection.Up;
                                        responseMessage.Message = EnumElevatorStatus.Stopping;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(3000);
                                        responseMessage.Direction = _elevatorService.Direction;
                                        responseMessage.Message = EnumElevatorStatus.Hold;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(1000);
                                    }
                                    else
                                    {

                                        responseMessage.Direction = EnumElevatorDirection.Up;
                                        responseMessage.Message = _elevatorService.Status;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(1000);
                                    }
                                    if (_elevatorService.GetDownQueueMax != -1)
                                        _elevatorService.MoveUp();
                                }

                                //Change direction if no more request from top floor
                                while (_elevatorService.GetDownQueueMax >= _elevatorService.CurrentFloor
                                    && _elevatorService.CurrentFloor <= MAX_FLOOR
                                    && _elevatorService.GetDownQueueMax != -1)
                                {
                                    _elevatorService.Operate(EnumElevatorDirection.Down);
                                    _elevatorService.Operate(EnumElevatorDirection.Up);
                                    if (_elevatorService.Status == EnumElevatorStatus.Stopped)
                                    {
                                        responseMessage.Direction = EnumElevatorDirection.Up;
                                        responseMessage.Message = EnumElevatorStatus.Stopping;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(3000);
                                        
                                        responseMessage.Direction = EnumElevatorDirection.Up;
                                        responseMessage.Message = EnumElevatorStatus.Hold;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(1000);
                                    }
                                    else
                                    {

                                        responseMessage.Direction = EnumElevatorDirection.Up;
                                        responseMessage.Message = _elevatorService.Status;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(1000);
                                    }
                                    if (_elevatorService.GetDownQueueMax != -1)
                                        _elevatorService.MoveUp();
                                }

                            }
                            else if (elevatorMessage.DestinationFloor <= _elevatorService.CurrentFloor)
                            {

                                while (elevatorMessage.DestinationFloor <= _elevatorService.CurrentFloor
                                    && _elevatorService.CurrentFloor >= MIN_FLOOR
                                    && _elevatorService.GetDownQueueMin != -1)
                                {

                                    _elevatorService.Operate(EnumElevatorDirection.Down);
                                    if (_elevatorService.Status == EnumElevatorStatus.Stopped)
                                    {
                                        responseMessage.Direction = EnumElevatorDirection.Down;
                                        responseMessage.Message = EnumElevatorStatus.Stopping;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(3000);
                                        responseMessage.Direction = EnumElevatorDirection.Down;
                                        responseMessage.Message = EnumElevatorStatus.Hold;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(1000);
                                    }
                                    else
                                    {

                                        responseMessage.Direction = EnumElevatorDirection.Down;
                                        responseMessage.Message = _elevatorService.Status;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(1000);
                                    }
                                    if (_elevatorService.GetDownQueueMin != -1)
                                        _elevatorService.MoveDown();
                                }


                                while (_elevatorService.GetDownQueueMin < elevatorMessage.DestinationFloor
                                    && _elevatorService.CurrentFloor >= MIN_FLOOR
                                    && _elevatorService.GetDownQueueMin != -1)
                                {

                                    _elevatorService.Operate(EnumElevatorDirection.Down);
                                    if (_elevatorService.Status == EnumElevatorStatus.Stopped)
                                    {
                                        responseMessage.Direction = EnumElevatorDirection.Down;
                                        responseMessage.Message = EnumElevatorStatus.Stopping;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(3000);
                                        responseMessage.Direction = EnumElevatorDirection.Down;
                                        responseMessage.Message = EnumElevatorStatus.Hold;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(1000);
                                    }
                                    else
                                    {

                                        responseMessage.Direction = EnumElevatorDirection.Down;
                                        responseMessage.Message = _elevatorService.Status;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(1000);
                                    }
                                    if (_elevatorService.GetDownQueueMin != -1)
                                        _elevatorService.MoveDown();
                                }

                                //Change direction if no more request from down floors
                                while (_elevatorService.GetUpQueueMax >= _elevatorService.CurrentFloor
                                    && _elevatorService.CurrentFloor <= MAX_FLOOR
                                    && _elevatorService.GetUpQueueMax != -1)
                                {

                                    _elevatorService.Operate(EnumElevatorDirection.Up);
                                    _elevatorService.Operate(EnumElevatorDirection.Down);
                                    if (_elevatorService.Status == EnumElevatorStatus.Stopped)
                                    {
                                        responseMessage.Direction = _elevatorService.Direction;
                                        responseMessage.Message = EnumElevatorStatus.Stopping;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(3000);
                                        responseMessage.Direction = EnumElevatorDirection.Down;
                                        responseMessage.Message = EnumElevatorStatus.Hold;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(1000);
                                    }
                                    else
                                    {

                                        responseMessage.Direction = EnumElevatorDirection.Down;
                                        responseMessage.Message = _elevatorService.Status;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(1000);
                                    }
                                    if (_elevatorService.GetUpQueueMax != -1)
                                        _elevatorService.MoveDown();
                                }

                                //Change direction if no more request from down floors
                                while (_elevatorService.GetUpQueueMax <= _elevatorService.CurrentFloor
                                    && _elevatorService.CurrentFloor >= MIN_FLOOR
                                    && _elevatorService.GetUpQueueMax != -1)
                                {

                                    _elevatorService.Operate(EnumElevatorDirection.Up);
                                    _elevatorService.Operate(EnumElevatorDirection.Down);
                                    if (_elevatorService.Status == EnumElevatorStatus.Stopped)
                                    {
                                        responseMessage.Direction = EnumElevatorDirection.Down;
                                        responseMessage.Message = EnumElevatorStatus.Stopping;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(3000);
                                        responseMessage.Direction = EnumElevatorDirection.Down;
                                        responseMessage.Message = EnumElevatorStatus.Hold;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(1000);
                                    }
                                    else
                                    {

                                        responseMessage.Direction = EnumElevatorDirection.Down;
                                        responseMessage.Message = _elevatorService.Status;
                                        responseMessage.FloorNo = _elevatorService.CurrentFloor;
                                        PublishElevatorMessage(responseMessage);
                                        Thread.Sleep(1000);
                                    }
                                    if (_elevatorService.GetUpQueueMax != -1)
                                        _elevatorService.MoveDown();
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.Log("ElevatorFloorControlSystem : " +  ex, Category.Exception, Priority.High);
            }
            
        }

        /// <summary>
        /// Returns the message back to UI screen.
        /// </summary>
        /// <param name="elevatorMessage">The elevator message.</param>
        private void PublishElevatorMessage(ElevatorStatus elevatorMessage)
        {

            this._eventAggregator.GetEvent<ElevatorControlMessageSystemEvent>().Publish(elevatorMessage);
        }

    }

    #endregion Methods
}
