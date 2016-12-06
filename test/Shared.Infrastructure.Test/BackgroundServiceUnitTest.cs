using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Infrastructure.Background;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;

namespace Shared.Infrastructure.Test
{
    [TestClass]
    public class BackgroundServiceUnitTest : UnitTestBase
    {
        [TestMethod]
        public void InvokeShouldWork()
        {
            IServiceProvider serviceProvider = base.InitDependencyInjection(services => { }, builder => { });

            IBackgroundService backgroundService = serviceProvider.GetService<IBackgroundService>();
            int x = 0, y = 50;
            backgroundService.Invoke(() =>
            {
                x = y;
            });
            System.Threading.Thread.Sleep(1000);

            Assert.AreEqual(x, y);
        }

        [TestMethod]
        public void InvokeManyTimeShouldWork()
        {
            IServiceProvider serviceProvider = base.InitDependencyInjection(services => { }, builder => { });

            IBackgroundService backgroundService = serviceProvider.GetService<IBackgroundService>();
            int x = 0;

            for (var i = 0; i < 100; i++)
            {
                backgroundService.Invoke(() =>
                {
                    x++;
                });
            }
            System.Threading.Thread.Sleep(1000);

            Assert.AreEqual(x, 100);
        }

        [TestMethod]
        public void ActionShouldRunInDiferrentSingleThread()
        {
            int id1 = 0, id2 = 0, idCurrent = Thread.CurrentThread.ManagedThreadId;

            IServiceProvider serviceProvider = base.InitDependencyInjection(services => { }, builder => { });

            IBackgroundService backgroundService = serviceProvider.GetService<IBackgroundService>();

            backgroundService.Invoke(() =>
            {
                Thread.Sleep(1000);
                id1 = Thread.CurrentThread.ManagedThreadId;
            });
            backgroundService.Invoke(() =>
            {
                Thread.Sleep(1000);
                id2 = Thread.CurrentThread.ManagedThreadId;
            });

            Assert.AreEqual(id1, 0); //action should execute later
            Assert.AreEqual(id2, 0);

            Thread.Sleep(2500);

            Assert.AreNotEqual(id1, idCurrent);
            Assert.AreNotEqual(id2, idCurrent);
            Assert.AreEqual(id1, id2);
        }
    }
}
