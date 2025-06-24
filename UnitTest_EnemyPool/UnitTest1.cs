using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using MyGame;

namespace UnitTest_EnemyPool
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            int created = 0;
            var pool = new GenericPool<TaxiEnemy>(
                factoryMethod: () => { created++; return new TaxiEnemy(); },
                resetAction: (e) => e.Reset(),
                initialSize: 1
            );

            var enemy1 = pool.Get();
            pool.Return(enemy1);
            var enemy2 = pool.Get();

            Assert.AreEqual(enemy1, enemy2);
            Assert.AreEqual(1, created);
        }
    }
}
