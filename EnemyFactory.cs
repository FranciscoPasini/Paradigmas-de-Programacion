using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public static class EnemyFactory
    {
        private static readonly Dictionary<Type, GenericPool<BaseEnemy>> _pools =
            new Dictionary<Type, GenericPool<BaseEnemy>>();

        private static readonly Random _random = new Random();

        static EnemyFactory()
        {
            InitializePools();
        }

        private static void InitializePools()
        {
            _pools.Add(typeof(PoliceEnemy), CreatePool<PoliceEnemy>(5));
            _pools.Add(typeof(RedCarEnemy), CreatePool<RedCarEnemy>(8));
            _pools.Add(typeof(WhiteCarEnemy), CreatePool<WhiteCarEnemy>(8));
            _pools.Add(typeof(TaxiEnemy), CreatePool<TaxiEnemy>(6));
            _pools.Add(typeof(MotoEnemy), CreatePool<MotoEnemy>(6));
        }

        private static GenericPool<BaseEnemy> CreatePool<T>(int initialSize) where T : BaseEnemy, new()
        {
            return new GenericPool<BaseEnemy>(
                factoryMethod: () => new T(),
                resetAction: enemy => enemy.Reset(),
                initialSize: initialSize
            );
        }

        public static BaseEnemy CreateEnemy(float x, float y, int type, int laneIndex, int dirY)
        {
            BaseEnemy enemy = GetEnemyFromPool(type);
            Image enemyImage = LoadEnemyImage(type, dirY);
            int speed = GetEnemySpeed(type);

            enemy.Initialize(x, y, speed, dirY, enemyImage, laneIndex);
            return enemy;
        }

        private static BaseEnemy GetEnemyFromPool(int type)
        {
            Type enemyType = GetEnemyType(type);
            if (_pools.TryGetValue(enemyType, out GenericPool<BaseEnemy> pool))
            {
                return pool.Get();
            }
            throw new ArgumentException($"Tipo de enemigo no válido: {type}");
        }

        private static Type GetEnemyType(int type)
        {
            switch (type)
            {
                case 0: return typeof(PoliceEnemy);
                case 1: return typeof(RedCarEnemy);
                case 2: return typeof(WhiteCarEnemy);
                case 3: return typeof(TaxiEnemy);
                case 4: return typeof(MotoEnemy);
                default: return typeof(PoliceEnemy);
            }
        }

        private static Image LoadEnemyImage(int type, int dirY)
        {
            string imagePath;
            string direction = dirY == 1 ? "D.png" : "Up.png";

            switch (type)
            {
                case 0:
                    imagePath = "assets/Enemies/Police/Police" + direction;
                    break;
                case 1:
                    imagePath = "assets/Enemies/RedCar/RedCar" + direction;
                    break;
                case 2:
                    imagePath = "assets/Enemies/WhiteCar/WhiteCar" + direction;
                    break;
                case 3:
                    imagePath = "assets/Enemies/Taxi/Taxi" + direction;
                    break;
                case 4:
                    imagePath = "assets/Enemies/Moto/Moto" + direction;
                    break;
                default:
                    imagePath = "assets/Enemies/Police/Police" + direction;
                    break;
            }

            return Engine.LoadImage(imagePath);
        }

        private static int GetEnemySpeed(int type)
        {
            switch (type)
            {
                case 0: return 4; 
                case 1: return 3; 
                case 2: return 2; 
                case 3: return 2; 
                case 4: return 3; 
                default: return 2; 
            }
        }

        public static void ReturnEnemy(BaseEnemy enemy)
        {
            if (enemy == null) return;

            Type enemyType = enemy.GetType();
            if (_pools.TryGetValue(enemyType, out GenericPool<BaseEnemy> pool))
            {
                pool.Return(enemy);
            }
        }
    }
}