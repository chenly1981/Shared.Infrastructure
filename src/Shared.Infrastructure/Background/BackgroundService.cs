using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Background
{
    /// <summary>
    /// 
    /// </summary>
    public class BackgroundService : IBackgroundService
    {
        private ConcurrentQueue<Action> ActionList { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BackgroundService()
        {
            ActionList = new ConcurrentQueue<Action>();
            Dispatch();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public void Invoke(Action action)
        {
            ActionList.Enqueue(action);
        }

        /// <summary>
        /// 
        /// </summary>
        private async Task Dispatch()
        {
            while (true)
            {
                Action action;
                if (ActionList.TryDequeue(out action))
                {
                    action.Invoke();
                }
                else
                {
                    await Task.Delay(50);
                }
            }
        }
    }
}
