using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGame
{
    public enum gameStatus
    {
        menu, game, pause, win, lose
    }

    public class GameManager
    {
        private static GameManager instance;
        private gameStatus gameStage = gameStatus.menu;    // 0-Menu     1-Game     2-Pause     3-Win    4-Lose

        private Image mainMenu = Engine.LoadImage("assets/MainMenu.png");
        private Image loseScreen = Engine.LoadImage("assets/Lose.png");
        private Image winScreen = Engine.LoadImage("assets/Win.png");
        private Image pause = Engine.LoadImage("assets/pause.png");
        private LevelController levelController;

        private Font defaultFont;
        public LevelController LevelController => levelController;

        public void ResetGame()
        {
            player1Score = 0;
            player2Score = 0;
            levelController = new LevelController(); // Reinicia el nivel
            levelController.InitializeLevel();
            ChangeGameStatus(gameStatus.game);
        }

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GameManager();
                }
                return instance;
            }
        }

        public void Initialize()
        {
            levelController = new LevelController();
            levelController.InitializeLevel();
            defaultFont = Engine.LoadFont("assets/Font/8bitOperatorPlus-Regular.ttf", 72);

        }

        private bool pKeyPressedLastFrame = false;

        public void Update()
        {
            bool pKeyNow = Engine.GetKey(Engine.KEY_P);

            if (pKeyNow && !pKeyPressedLastFrame)
            {
                if (gameStage == gameStatus.game)
                {
                    gameStage = gameStatus.pause;
                }
                else if (gameStage == gameStatus.pause)
                {
                    gameStage = gameStatus.game;
                }
            }

            pKeyPressedLastFrame = pKeyNow;

            switch (gameStage)
            {
                case gameStatus.menu:
                    if (Engine.GetKey(Engine.KEY_ESP))
                        gameStage = gameStatus.game;
                    break;

                case gameStatus.pause:
                    break;

                case gameStatus.game:
                    levelController.Update();
                    break;

                case gameStatus.win:
                    if (Engine.GetKey(Engine.KEY_R))
                    {
                        ResetGame();
                    }
                    break;

                case gameStatus.lose:
                    if (Engine.GetKey(Engine.KEY_R))
                    {
                        ResetGame();
                    }
                    break;
            }
        }


        public void Render()
        {
            switch (gameStage)
            {
                case gameStatus.menu:
                    Engine.Clear();
                    Engine.Draw(mainMenu, 0, 0);
                    Engine.Show();
                    break;

                case gameStatus.game:
                    levelController.Render();
                    break;

                case gameStatus.pause:
                    levelController.Render();
                    //Engine.Clear();
                    Engine.Draw(pause, 370, 150);
                    Engine.DrawText("PAUSA", 310, 250, 255, 255, 255, defaultFont);
                    Engine.Show();
                    break;

                case gameStatus.win:
                    Engine.Clear();
                    Engine.Draw(winScreen, 0, 0);
                    Engine.Show();
                    break;

                case gameStatus.lose:
                    Engine.Clear();
                    Engine.Draw(loseScreen, 0, 0);
                    Engine.Show();
                    break;
            }
        }

        public void ChangeGameStatus(gameStatus status)
        {
            gameStage = status;
        }


        private int player1Score = 0;
        private int player2Score = 0;
        public void AddPoint(int playerId)
        {
            if (playerId == 1)
                player1Score++;

            else if (playerId == 2)
                player2Score++;

            if (player1Score >= 5 || player2Score >= 5)
            {
                ChangeGameStatus(gameStatus.win);
            }
        }

        public int GetScore(int playerId)
        {
            return playerId == 1 ? player1Score : player2Score;
        }
    }
}
