using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public class LevelController
    {
        private List<Enemy> enemyList = new List<Enemy>();
        private Image fondo = Engine.LoadImage("assets/street.png");
        private Player player1;
        private Player2 player2;

        private Dictionary<int, bool> laneOccupied = new Dictionary<int, bool>
    {
        { 0, false },
        { 1, false },
        { 2, false },
        { 3, false }
    };

        private Random random = new Random();
        private int spawnTimer = 0;
        public List<Enemy> EnemyList => enemyList;
        private Font scoreFont;

        public void InitializeLevel()
        {
            player1 = new Player(50, 200);
            player1.OnCollision += Player_OnCollision;

            player2 = new Player2(50, 300);
            player2.OnCollision += Player_OnCollision;

            scoreFont = Engine.LoadFont("assets/Font/8bitOperatorPlus-Regular.ttf", 32);
        }

        private void Player_OnCollision(object sender, EventArgs e)
        {
            GameManager.Instance.ChangeGameStatus(gameStatus.lose);
        }

        public void Update()
        {
            player1.Update();
            player2.Update();

            foreach (Enemy e in enemyList) e.Update();

            spawnTimer++;

            if (spawnTimer >= 70)
            {
                SpawnRandomEnemy();
                spawnTimer = 0;
            }

            for (int i = enemyList.Count - 1; i >= 0; i--)
            {
                if (enemyList[i].IsOffScreen())
                {
                    laneOccupied[enemyList[i].laneIndex] = false;
                    enemyList.RemoveAt(i);
                }
            }
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

            enemyList.Add(new Enemy(lane.x, posY, type, laneIndex));
            laneOccupied[laneIndex] = true;
        }

        public void Render()
        {
            Engine.Clear();
            Engine.Draw(fondo, 0, 0);
            player1.Render();
            player2.Render();

            foreach (Enemy e in enemyList) e.Render();

            Engine.DrawText("Player 1 score: " + GameManager.Instance.GetScore(), 20, 20, 255, 255, 255, scoreFont);
            Engine.DrawText("Player 2 score: " + GameManager.Instance.GetScore(), 20, 60, 255, 255, 0, scoreFont);

            Engine.Show();
        }

    }

}

