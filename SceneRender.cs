using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace
{
    class SceneRender
    {
        private readonly GameSetting _gameSetting;
        private readonly char _borderOut = '|';
        private readonly char _borderInside = '~';
        private readonly char _borderBottom = 'П';
        private readonly byte _playerInformationRight = 55;

        public const byte ScreenWidth = 46;
        public const byte ScreenHeight = 40;
        public const byte BorderWidth = 3;
        public const byte BorderBottomHeight = 1;

        private char[,] _screen;
          
        public SceneRender(GameSetting gameSetting)
        {
            _gameSetting = gameSetting;

            Console.WindowHeight = ScreenHeight + 5;
            Console.WindowWidth = ScreenWidth + ScreenWidth;
            Console.BufferHeight = ScreenHeight + 5;
            Console.BufferWidth = ScreenWidth + ScreenWidth;

            Console.CursorVisible = false;
            _screen = new char[ScreenHeight, ScreenWidth];
            ScreenClear();
        }

        public void ScreenClear()
        {
            for (int y = 0; y < ScreenHeight; y++)
            {
                for (int x = 0; x < ScreenWidth; x++)
                {
                    _screen[y, x] = ' ';
                }
            }
        }

        public void Render(Scene scene)
        {
            EnterScore();
            EnterPlayerHealth();
            AddPlayerGameItems(scene.Scores);
            AddPlayerGameItems(scene.PlayerHealths);
            AddScreenCar(scene.PlayerCar);
            AddScreenCar(scene.CarEnemies);
            AddScreenCar(scene.FastCarEnemies);
            AddScreenStaticItem();
            StringBuilder builderScreen = new StringBuilder();
            for (int y = 0; y < ScreenHeight; y++)
            {
                for (int x = 0; x < ScreenWidth; x++)
                {
                    builderScreen.Append(_screen[y, x]);
                }
                builderScreen.Append(Environment.NewLine);
            }

            Console.WriteLine(builderScreen.ToString());

            Console.SetCursorPosition(0, 0);
        }

        private void AddScreenStaticItem()
        {
            for (int x = 0; x < ScreenWidth; x++)
            {
                _screen[ScreenHeight - 1, x] = _borderBottom;
            }
            for (int y = 0; y < ScreenHeight - 1; y++)
            {
                _screen[y, 0] = _borderOut;
                _screen[y, 1] = _borderOut;
                _screen[y, 2] = _borderInside;
                _screen[y, ScreenWidth - 3] = _borderInside;
                _screen[y, ScreenWidth - 2] = _borderOut;
                _screen[y, ScreenWidth - 1] = _borderOut;
            }
        }

        public void AddSpriteLineWay(char[] sprite)
        {
            for (int y = 0; y < sprite.Length; y++)
            {
                _screen[y, SceneRender.ScreenWidth / 2] = sprite[y];
            }
        }

        private void AddScreenCar(GameObjectCar gameObject)
        {
            int i = 0;
            for (int y = gameObject.YCoordinate; y < gameObject.YCoordinate + GameObjectCar.CarHeight; y++)
            {
                for (int x = gameObject.XCoordinate; x < gameObject.XCoordinate + GameObjectCar.CarWidth; x++)
                {
                    if (i >= gameObject.FigureCar.Length)
                    {
                        return;
                    }
                    AddSceneItem(y, x, gameObject.FigureCar[i]);
                    i++;
                }
            }
        }

        private void AddScreenCar(List<FastCarEnemy> fastCarEnemies)
        {
            for (int i = 0; i < fastCarEnemies.Count; i++)
            {
                AddScreenCar(fastCarEnemies[i]);
            }
        }

        private void AddScreenCar(List<CarEnemy> carEnemies)
        {
            for (int i = 0; i < carEnemies.Count; i++)
            {
                AddScreenCar(carEnemies[i]);
            }
        }

        private void AddSceneItem(int y, int x, char item)
        {
            if ((0 <= y && ScreenHeight > y) && (0 < x && ScreenWidth > x))
            {
                _screen[y, x] = item;
            }
        }

        private void AddPlayerGameItems(List<Score> score)
        {
            for (int indexScore = 0; indexScore < score.Count; indexScore++)
            {
                for (int i = 0; i < score[indexScore].Figure.Length; i++)
                {
                    AddSceneItem(score[indexScore].Figure[i].YCoordinate, score[indexScore].Figure[i].XCoordinate, score[indexScore].Figure[i].Item);
                }
            }
        }

        private void AddPlayerGameItems(List<PlayerHealth> playerHealths)
        {
            for (int indexScore = 0; indexScore < playerHealths.Count; indexScore++)
            {
                for (int i = 0; i < playerHealths[indexScore].Figure.Length; i++)
                {
                    AddSceneItem(playerHealths[indexScore].Figure[i].YCoordinate, playerHealths[indexScore].Figure[i].XCoordinate, playerHealths[indexScore].Figure[i].Item);
                }
            }
        }

        public static void GameOver()
        {
            int Width = (ScreenWidth / 2) - ("     Game Over     ".Length / 2);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(Width, ScreenHeight / 2 - 5);
            Console.WriteLine("                        ");
            Console.CursorLeft = Width;
            Console.WriteLine("     Game Over     ");
            Console.CursorLeft = Width;
            Console.WriteLine("                        ");
            if (TabelScores.IndexNewScore == -1)
            {
                Console.CursorLeft = (ScreenWidth / 2) - ($"     Score {TabelScores.score}     ".Length / 2);
                Console.WriteLine($"     Score {TabelScores.score}        ");
            }
            else
            {
                Console.CursorLeft = (ScreenWidth / 2) - ($"     New top score {TabelScores.score}     ".Length / 2);
                Console.WriteLine($"     New top score {TabelScores.score}     ");
            }
            Console.CursorLeft = Width;
            Console.WriteLine("                        ");
            Console.ResetColor();
        }

        public void EnterScore()
        {
            Console.CursorTop = 13;
            Console.CursorLeft = _playerInformationRight;
            Console.Write($"Score {TabelScores.score}   ");
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
        }
    
        public void EnterPlayerHealth()
        {
            Console.CursorTop = 15;
            Console.CursorLeft = _playerInformationRight;
            Console.Write($"PH {PlayerHealth.PH} ");
            Console.CursorTop = 0;
            Console.CursorLeft = 0;
        }

        public void EnterTopScores()
        {
            Console.CursorTop = 3;
            Console.CursorLeft = _playerInformationRight;
            Console.WriteLine("Top scores");

            int[] scores = TabelScores.ParseTableScoreToArray();
            for (int indexS = 0, CursorTop = 5; indexS < scores.Length; indexS++, CursorTop++)
            {
                Console.CursorTop = CursorTop;
                Console.CursorLeft = _playerInformationRight;
                if (TabelScores.IndexNewScore != -1 && TabelScores.IndexNewScore == indexS)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                }
                Console.WriteLine((indexS + 1).ToString() + "   " + scores[indexS].ToString());
                Console.ResetColor();
            }

            Console.CursorTop = 11;
            Console.CursorLeft = _playerInformationRight;
            Console.WriteLine("----------");

            Console.CursorTop = 0;
            Console.CursorLeft = 0;
        }
    }
}




/*                         
 *
 *               
 *                01234   
 *         // ||~ 0/A\0
 *         // ||~ ||H||
 *         // ||~ |[_]|
 *         // ||~ 0--+0
 *            
 *            
 *         // ||~ 0^--0
 *         // ||~ ||H||
 *         // ||~ |\_/|
 *         // ||~ 0<_>0
 *            
 *                
 *         // ||~ 0^--0
 *         // ||~ |[~]|
 *         // ||~ 0\_/0
 *         // ||~ \V_V/
 *            
 *            
 *            
 *            
 *            
 */