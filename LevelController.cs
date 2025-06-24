using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class LevelController
    {
        private List<BaseEnemy> enemyList = new List<BaseEnemy>();
        private Image fondo = Engine.LoadImage("assets/street.png");
        private Player player1;
        private Player2 player2;
        private bool p1Active = true;
        private bool p2Active = true;
        private readonly Vector2 p1StartPos = new Vector2(50, 200);
        private readonly Vector2 p2StartPos = new Vector2(50, 300);

        private List<LifePowerUp> activePowerUps = new List<LifePowerUp>();
        private GenericPool<LifePowerUp> powerUpPool;
        private float powerUpTimer = 0f;
        private const float POWERUP_SPAWN_INTERVAL = 10f;

        private Dictionary<int, bool> laneOccupied = new Dictionary<int, bool>
        {
            { 0, false },
            { 1, false },
            { 2, false },
            { 3, false }
        };

        private Random random = new Random();
        private float spawnTimer = 1f;
        private Font scoreFont;

        public Player Player1 => player1;
        public Player2 Player2 => player2;
        public List<BaseEnemy> EnemyList => enemyList;

        public void InitializeLevel()
        {
            player1 = new Player(p1StartPos.x, p1StartPos.y);
            player1.OnDeath += (s, e) => { p1Active = false; CheckBothDead(); };

            player2 = new Player2(p2StartPos.x, p2StartPos.y);
            player2.OnDeath += (s, e) => { p2Active = false; CheckBothDead(); };

            scoreFont = Engine.LoadFont("assets/Font/8bitOperatorPlus-Regular.ttf", 20);
            powerUpPool = new GenericPool<LifePowerUp>(() => new LifePowerUp(), p => p.Reset(), 3);
        }

        private void CheckBothDead()
        {
            if (!p1Active && !p2Active)
                GameManager.Instance.ChangeGameStatus(gameStatus.lose);
        }

        public void Update()
        {
            if (p1Active) player1.Update();
            if (p2Active) player2.Update();

            foreach (BaseEnemy e in enemyList)
            {
                e.Update();
            }

            spawnTimer += Time.DeltaTime;
            if (spawnTimer >= 1.0f) 
            {
                SpawnRandomEnemy();
                spawnTimer = 1f;
            }

            for (int i = enemyList.Count - 1; i >= 0; i--)
            {
                if (enemyList[i].IsOffScreen())
                {
                    laneOccupied[enemyList[i].laneIndex] = false;
                    EnemyFactory.ReturnEnemy(enemyList[i]);
                    enemyList.RemoveAt(i);
                }
            }

            powerUpTimer += Time.DeltaTime;

            if (powerUpTimer >= POWERUP_SPAWN_INTERVAL && activePowerUps.Count == 0)
            {
                SpawnPowerUp();
                powerUpTimer = 0f;
            }

            for (int i = activePowerUps.Count - 1; i >= 0; i--)
            {
                activePowerUps[i].Update();
            }
        }

        public void OnPowerUpCollected()
        {
            powerUpTimer = 0f;
        }

        private void SpawnPowerUp()
        {
            var powerUp = powerUpPool.Get();
            powerUp.Activate();
            activePowerUps.Add(powerUp);
        }

        private void SpawnRandomEnemy()
        {
            (int x, int dirY)[] lanes = {
                (200, -1),
                (325, -1),
                (460, 1),
                (600, 1)
            };

            var availableLanes = new List<int>();
            for (int i = 0; i < lanes.Length; i++)
            {
                if (!laneOccupied[i])
                    availableLanes.Add(i);
            }

            if (availableLanes.Count == 0) return;

            int laneIndex = availableLanes[random.Next(availableLanes.Count)];
            var lane = lanes[laneIndex];
            int posY = (lane.dirY == -1) ? 700 : -100;
            int type = random.Next(0, 5);

            var enemy = EnemyFactory.CreateEnemy(lane.x, posY, type, laneIndex, lane.dirY);
            enemyList.Add(enemy);
            laneOccupied[laneIndex] = true;
        }

        public void ReturnLifePowerUp(LifePowerUp powerUp)
        {
            activePowerUps.Remove(powerUp);
            powerUpPool.Return(powerUp);
        }

        public void Render()
        {
            Engine.Clear();
            Engine.Draw(fondo, 0, 0);

            if (p1Active) player1.Render();
            if (p2Active) player2.Render();

            foreach (var powerUp in activePowerUps)
            {
                powerUp.Render();
            }

            foreach (BaseEnemy e in enemyList)
            {
                e.Render();
            }

            // UI
            Engine.DrawText($"Player 1 score: {GameManager.Instance.GetScore(1)}", 20, 40, 255, 255, 255, scoreFont);
            Engine.DrawText($"Player 2 score: {GameManager.Instance.GetScore(2)}", 20, 60, 255, 255, 0, scoreFont);
            Engine.DrawText($"Player 1 lives: {(p1Active ? player1.Lives.ToString() : "0")}", 665, 580, 255, 255, 255, scoreFont);
            Engine.DrawText($"Player 2 lives: {(p2Active ? player2.Lives.ToString() : "0")}", 665, 600, 255, 255, 0, scoreFont);

            Engine.Show();
        }
    }
}
