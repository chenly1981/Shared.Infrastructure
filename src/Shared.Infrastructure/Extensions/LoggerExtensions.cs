using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogDebugAsync<TCategory>(this ILogger<TCategory> logger, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogDebug(message, args);
            });            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogDebugAsync<TCategory>(this ILogger<TCategory> logger, EventId eventId, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogDebug(eventId, message, args);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogDebugAsync<TCategory>(this ILogger<TCategory> logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogDebug(eventId, exception, message, args);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogInformationAsync<TCategory>(this ILogger<TCategory> logger, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogInformation(message, args);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogInformationAsync<TCategory>(this ILogger<TCategory> logger, EventId eventId, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogInformation(eventId, message, args);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogInformationAsync<TCategory>(this ILogger<TCategory> logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogInformation(eventId, exception, message, args);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogCriticalAsync<TCategory>(this ILogger<TCategory> logger, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogCritical(message, args);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogCriticalAsync<TCategory>(this ILogger<TCategory> logger, EventId eventId, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogCritical(eventId, message, args);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogCriticalAsync<TCategory>(this ILogger<TCategory> logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogCritical(eventId, exception, message, args);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogErrorAsync<TCategory>(this ILogger<TCategory> logger, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogError(message, args);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogErrorAsync<TCategory>(this ILogger<TCategory> logger, EventId eventId, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogError(eventId, message, args);
            });
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogErrorAsync<TCategory>(this ILogger<TCategory> logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogError(eventId, exception, message, args);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogTraceAsync<TCategory>(this ILogger<TCategory> logger, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogTrace(message, args);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogTraceAsync<TCategory>(this ILogger<TCategory> logger, EventId eventId, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogTrace(eventId, message, args);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogTraceAsync<TCategory>(this ILogger<TCategory> logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogTrace(eventId, exception, message, args);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogWarningAsync<TCategory>(this ILogger<TCategory> logger, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogWarning(message, args);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogWarningAsync<TCategory>(this ILogger<TCategory> logger, EventId eventId, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogWarning(eventId, message, args);
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TCategory"></typeparam>
        /// <param name="logger"></param>
        /// <param name="eventId"></param>
        /// <param name="exception"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        public static void LogWarningAsync<TCategory>(this ILogger<TCategory> logger, EventId eventId, Exception exception, string message, params object[] args)
        {
            Task.Factory.StartNew(() =>
            {
                logger.LogWarning(eventId, exception, message, args);
            });
        }
    }
}
