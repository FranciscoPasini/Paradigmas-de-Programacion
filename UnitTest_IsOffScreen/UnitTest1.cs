using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MyGame;

namespace UnitTest_IsOffScreen
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var enemy = new PoliceEnemy();
            enemy.Initialize(0, 850, 2, 1, null, 0);
            Assert.IsTrue(enemy.IsOffScreen());

            var enemy2 = new PoliceEnemy();
            enemy2.Initialize(0, 400, 2, 1, null, 0);
            Assert.IsFalse(enemy2.IsOffScreen());
        }
    }
}