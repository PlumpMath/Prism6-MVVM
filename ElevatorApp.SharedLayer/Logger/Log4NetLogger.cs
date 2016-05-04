using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Prism.Logging;
namespace ElevatorApp.SharedLayer.Logger
{
    /// <summary>
    /// Class Log4NetLogger.
    /// </summary>
    /// <seealso cref="Prism.Logging.ILoggerFacade" />
    public class Log4NetLogger : ILoggerFacade
    {
        /// <summary>
        /// The _m logger
        /// </summary>
        private readonly ILog _mLogger = LogManager.GetLogger(typeof(Log4NetLogger));

        /// <summary>
        /// Logs the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="category">The category.</param>
        /// <param name="priority">The priority.</param>
        public void Log(string message, Category category, Priority priority)
        {
            switch (category)
            {
                case Category.Debug:
                    _mLogger.Debug(message);
                    break;
                case Category.Warn:
                    _mLogger.Warn(message);
                    break;
                case Category.Exception:
                    _mLogger.Error(message);
                    break;
                case Category.Info:
                    _mLogger.Info(message);
                    break;
            }
        }
    }
}
