using System;
using System.Collections.Generic;
using MyGame;

namespace MyGame
{
    public class LevelController
    {
      
        private int p1Lives = 3;
        private int p2Lives = 3;
        private bool p1Active = true;
        private bool p2Active = true;
        private Vector2 p1StartPos = new Vector2(50, 200);
        private Vector2 p2StartPos = new Vector2(50, 300);

        private List<BaseEnemy> enemyList = new List<BaseEnemy>();
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
        public List<BaseEnemy> EnemyList => enemyList;
        private Font scoreFont;
    

        public void InitializeLevel()
        {
            // Crear jugadores y asignar eventos
            player1 = new Player(p1StartPos.x, p1StartPos.y);
            player1.OnCollision += Player_OnCollision;

            player2 = new Player2(p2StartPos.x, p2StartPos.y);
            player2.OnCollision += Player_OnCollision;

            scoreFont = Engine.LoadFont("assets/Font/8bitOperatorPlus-Regular.ttf", 20);
        }

        private void Player_OnCollision(object sender, EventArgs e)
        {
            if (sender == player1 && p1Active)
                HandlePlayerDeath(1);
            else if (sender == player2 && p2Active)
                HandlePlayerDeath(2);
        }

        private void HandlePlayerDeath(int playerId)
        {
            if (playerId == 1)
            {
                p1Lives--;
                if (p1Lives > 0)
                {
                    // Respawnear jugador 1
                    player1.Transform.Position = p1StartPos;
                }
                else
                {
                    // Desactivar jugador 1
                    p1Active = false;
                }
            }
            else // jugador 2
            {
                p2Lives--;
                if (p2Lives > 0)
                {
                    player2.Transform.Position = p2StartPos;
                }
                else
                {
                    p2Active = false;
                }
            }

            // Si ambos están inactivos, pasar a pantalla de derrota
            if (!p1Active && !p2Active)
            {
                GameManager.Instance.ChangeGameStatus(gameStatus.lose);
            }
        }

        public void Update()
        {
            // Actualizar solo jugadores activos
            if (p1Active) player1.Update();
            if (p2Active) player2.Update();

            // Actualizar enemigos
            foreach (BaseEnemy e in enemyList) e.Update();

            // Spawn de enemigos
            spawnTimer++;
            if (spawnTimer >= 70)
            {
                SpawnRandomEnemy();
                spawnTimer = 0;
            }

            // Limpiar enemigos fuera de pantalla
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
                if (!laneOccupied[i]) availableLanes.Add(i);

            if (availableLanes.Count == 0) return;

            int laneIndex = availableLanes[random.Next(availableLanes.Count)];
            var lane = lanes[laneIndex];
            int posY = (lane.dirY == -1) ? 700 : -100;
            int type = random.Next(0, 5);

            enemyList.Add(EnemyFactory.CreateEnemy(lane.x, posY, type, laneIndex, lane.dirY));
            laneOccupied[laneIndex] = true;
        }

        public void Render()
        {
            Engine.Clear();

            // Dibujar fondo
            Engine.Draw(fondo, 0, 0);

            // Render solo jugadores activos
            if (p1Active) player1.Render();
            if (p2Active) player2.Render();

            // Render enemigos
            foreach (BaseEnemy e in enemyList) e.Render();

            // Mostrar puntajes y vidas
            Engine.DrawText("Player 1 score: " + GameManager.Instance.GetScore(1), 20, 40, 255, 255, 255, scoreFont);
            Engine.DrawText("Player 2 score: " + GameManager.Instance.GetScore(2), 20, 60, 255, 255, 0, scoreFont);
            Engine.DrawText($"Player 1 lives: {p1Lives}", 665, 580, 255, 255, 255, scoreFont);
            Engine.DrawText($"Player 2 lives: {p2Lives}", 665, 600, 255, 255, 0, scoreFont);

            Engine.Show();
        }
    }
}


