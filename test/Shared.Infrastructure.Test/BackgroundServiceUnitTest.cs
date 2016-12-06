using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Infrastructure.Background;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
