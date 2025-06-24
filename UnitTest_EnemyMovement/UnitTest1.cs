using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyGame;
using System;

namespace UnitTest_EnemyMovement
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var transform = new Transform(new Vector2(0, 0));
            var movement = new EnemyMovement(transform, speed: 2, dirY: 1);

            movement.Update();

            Assert.AreEqual(2, transform.Position.y);
        }
    }
}
