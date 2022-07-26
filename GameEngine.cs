using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace
{
    class GameEngine
    {
        private bool _isGameOver;
        private Random _random;
        private GameSetting _gameSetting;
        private Scene _scene;
        private SceneRender _sceneRender;
        private Sprite _sprite;
        private static GameEngine _instance;
        private int _countFPS = 0;
        private TabelScores _tabelScores;

        private readonly int _FPS = 50;

        public GameEngine()
        {

        }

        private GameEngine(GameSetting gameSetting, Sprite sprite)
        {
            _isGameOver = true;
            _random = new Random();
            _gameSetting = gameSetting;
            _scene = Scene.GetInstance(gameSetting);
            _sceneRender = new SceneRender(gameSetting);
            _sprite = sprite;
            _tabelScores = new TabelScores();
        }

        public static GameEngine GetGameEngine(GameSetting gameSetting, Sprite sprite)
        {
            if (_instance == null)
            {
                _instance = new GameEngine(gameSetting, sprite);
            }

            return _instance;
        }

        public void Run()
        {
            int Score = 0;

            int timeFastCar = 0;
            int newFastCar = 80;
            int randomNewFastCar = 300;

            int timeCarEnemy = 0;
            int newCarEnemy = 20;
            int randomNewCarEnemy = 180;

            int timePH = 0;
            int newPH = 500;

            int timeComplexity = 0;

            int newLevel = 0;


            Thread.Sleep(2000);
            _sceneRender.EnterTopScores();
            do
            {
                _sceneRender.ScreenClear();
                _sceneRender.AddSpriteLineWay(SpriteChangeLineWay());
                _sceneRender.Render(_scene);

                if (Score == 80)
                {
                    AddScore();
                    Score = 0;
                }
                Score++;

                if (PlayerHealth.PH < 10)
                {
                    if (timePH == newPH)
                    {
                        AddPlayerHealth();
                        timePH = 0;
                        if (PlayerHealth.PH == 1)
                        {
                            newPH = 10;
                        }
                        else if (PlayerHealth.PH > 5)
                        {
                            newPH = _random.Next(500, 1000);
                        }
                        else 
                        {
                            newPH = _random.Next(1000, 1500);
                        }
                    }
                    timePH++;
                }

                if (timeFastCar == newFastCar)
                {
                    AddFastCarEnemy();
                    newFastCar = _random.Next(50, randomNewFastCar);
                    timeFastCar = 0;
                }
                timeFastCar++;

                if (timeCarEnemy == newCarEnemy)
                {
                    AddCarEnemy();
                    newCarEnemy = _random.Next(1, randomNewCarEnemy);
                    timeCarEnemy = 0;
                }
                timeCarEnemy++;

                if (timeComplexity == 1000 && newLevel < 7)
                {
                    randomNewFastCar -= 12;
                    randomNewCarEnemy -= 15;
                    timeComplexity = 0;
                    newLevel++;
                }
                timeComplexity++;

                if (ChangeEachFrame(7))
                {
                    MoveCarEnemy();
                }

                if (ChangeEachFrame(6))
                {
                    MovePlayerHealth();
                    MoveScore();
                }

                if (ChangeEachFrame(2))
                {
                    MoveFastCarEnemy();
                }

                if (ChangeEachFrame(4))
                {
                    _sprite.ChangeSprateLineWay++;
                }

                if (_countFPS == _FPS)
                {
                    _countFPS = 0;
                }
                _countFPS++;

                if (PlayerHealth.PH <= 0)
                {
                    _tabelScores.UpdateTableScore();
                    _sceneRender.EnterTopScores();
                    SceneRender.GameOver();
                    _isGameOver = false;
                    _sceneRender.EnterScore();
                    _sceneRender.EnterPlayerHealth();
                }

                Thread.Sleep(20);

            } while (_isGameOver);

            Console.SetCursorPosition(0, 40);

            Console.ReadKey(true);
        }

        private char[] SpriteChangeLineWay()
        {
            if (_sprite.ChangeSprateLineWay == 4)
            {
                _sprite.ChangeSprateLineWay = 1;
            }
            return _sprite.ChangeSprateLineWay switch
            {
                1 => _sprite.SprateOneLineWay,
                2 => _sprite.SprateTwoLineWay,
                3 => _sprite.SprateThreeLineWay
            };
        }

        private void AddFastCarEnemy()
        {
            FastCarEnemy fastCarEnemy = new FastCarEnemy()
            {
                FigureCar = _gameSetting.FigureFastCarEnemy,
                YCoordinate = _gameSetting.YCoordinateFastCarEnemy,
                XCoordinate = _scene.PlayerCar.XCoordinate
            };

            _scene.FastCarEnemies.Add(fastCarEnemy);
        }

        private void AddCarEnemy()
        {
            CarEnemy carEnemy = new CarEnemy()
            {
                FigureCar = _gameSetting.FigureCarEnemy,
                YCoordinate = _gameSetting.YCoordinateCarEnemy,
                XCoordinate = _random.Next(SceneRender.BorderWidth, SceneRender.ScreenWidth - SceneRender.BorderWidth - GameObjectCar.CarWidth),
                movementCat = RandomMovement()     
            };

            _scene.CarEnemies.Add(carEnemy);
        }

        private void AddScore()
        {
            Score score = new Score();
            score.Figure[0].XCoordinate = _random.Next(SceneRender.BorderWidth, SceneRender.ScreenWidth - SceneRender.BorderWidth - Score.ItemWidth);
            score.Figure[1].XCoordinate = score.Figure[0].XCoordinate + 1;
            _scene.Scores.Add(score);
        }

        private void AddPlayerHealth()
        {
            PlayerHealth playerHealth = new PlayerHealth();
            playerHealth.Figure[0].XCoordinate = _random.Next(SceneRender.BorderWidth, SceneRender.ScreenWidth - SceneRender.BorderWidth - Score.ItemWidth);
            playerHealth.Figure[1].XCoordinate = playerHealth.Figure[0].XCoordinate + 1;
            _scene.PlayerHealths.Add(playerHealth);
        }

        private void MoveScore()
        {
            int indexScore = 0;
            while (indexScore < _scene.Scores.Count)
            {

                if (_scene.Scores[indexScore].DoesPlayerAddScore(_scene.PlayerCar))
                {
                    _scene.Scores.RemoveAt(indexScore);
                    continue;
                }


                if (_scene.Scores[indexScore].Figure[0].YCoordinate >= SceneRender.ScreenHeight - SceneRender.BorderBottomHeight)
                {
                    _scene.Scores.RemoveAt(indexScore);
                    continue;
                }

                for (int i = 0; i < _scene.Scores[indexScore].Figure.Length; i++)
                {
                    _scene.Scores[indexScore].Figure[i].YCoordinate++;
                }

                indexScore++;
            }
        }

        private void MovePlayerHealth()
        {

            int indexPH = 0;
            while (indexPH < _scene.PlayerHealths.Count)
            {
                if (_scene.PlayerHealths[indexPH].DoesPlayerAddPH(_scene.PlayerCar))
                {
                    _scene.PlayerHealths.RemoveAt(indexPH);
                    continue;
                }

                if (_scene.PlayerHealths[indexPH].Figure[0].YCoordinate >= SceneRender.ScreenHeight - SceneRender.BorderBottomHeight)
                {
                    _scene.PlayerHealths.RemoveAt(indexPH);
                    continue;
                }

                for (int i = 0; i < _scene.PlayerHealths[indexPH].Figure.Length; i++)
                {
                    _scene.PlayerHealths[indexPH].Figure[i].YCoordinate++;
                }

                indexPH++;
            }
        }

        private MovementCat RandomMovement()
        {
            return _random.Next(0, 2) switch
            {
                0 => MovementCat.LeftBotton,
                1 => MovementCat.RightBotton,
                _ => MovementCat.RightBotton,
            };
        }

        private void MoveFastCarEnemy()
        {
            if (_scene.FastCarEnemies.Count == 0)
            {
                return;
            }

            int i = 0;
            while (i < _scene.FastCarEnemies.Count)
            {
                _scene.FastCarEnemies[i].YCoordinate++;

                if (DoesEnemyCarDelete(_scene.FastCarEnemies[i]))
                {
                    _scene.FastCarEnemies.RemoveAt(i);
                    continue;
                }

                for (int index = 0; index < _scene.CarEnemies.Count; index++)
                {
                    if (_scene.FastCarEnemies[i].Equals(_scene.CarEnemies[index]))
                    {
                        _scene.CarEnemies[index].shake = true;
                    }
                }

                i++;
            }
        }

        private void MoveCarEnemy()
        {
            if (_scene.CarEnemies.Count == 0)
            {
                return;
            }

            int i = 0;
            while (i < _scene.CarEnemies.Count)
            {
                _scene.CarEnemies[i].IsChangeMovement(_scene);

                if (_scene.CarEnemies[i].shake)
                {
                    _scene.CarEnemies[i].ShakeCar();
                }

                if (_scene.CarEnemies[i].movementCat == MovementCat.LeftBotton)
                {
                    _scene.CarEnemies[i].XCoordinate--;
                }
                else
                {
                    _scene.CarEnemies[i].XCoordinate++;
                }

                _scene.CarEnemies[i].YCoordinate++;

                if (DoesEnemyCarDelete(_scene.CarEnemies[i]))
                {
                    _scene.CarEnemies.RemoveAt(i);
                    continue;
                }

                i++;
            }
        }

        private bool DoesEnemyCarDelete(GameObjectCar gameObjectCar)
        {
            if (gameObjectCar.YCoordinate >= SceneRender.ScreenHeight - SceneRender.BorderBottomHeight)
            {
                return true;
            }

            if (gameObjectCar.Equals(_scene.PlayerCar))
            {
                PlayerHealth.ReducePH();
                return true;
            }

            return false;
        } 

        public void MovePlayerCarLeft()
        {
            if (_scene.PlayerCar.XCoordinate > 3)
            {
                _scene.PlayerCar.XCoordinate--;
            }
            PlayerDeleteEnemyCar();
            AddPlayerGameItem();
        }

        public void MovePlayerCarRight()
        {
            if (SceneRender.ScreenWidth - (SceneRender.BorderWidth + GameObjectCar.CarWidth) > _scene.PlayerCar.XCoordinate)
            {
                _scene.PlayerCar.XCoordinate++;
            }
            PlayerDeleteEnemyCar();
            AddPlayerGameItem();
        }

        public void MovePlayerCarTop()
        {
            if (_scene.PlayerCar.YCoordinate > 0)
            {
                _scene.PlayerCar.YCoordinate--;
            }
            PlayerDeleteEnemyCar(); 
            AddPlayerGameItem();
        }

        public void MovePlayerCarBotton()
        {
            if (SceneRender.ScreenHeight - (SceneRender.BorderBottomHeight + GameObjectCar.CarHeight) > _scene.PlayerCar.YCoordinate)
            {
                _scene.PlayerCar.YCoordinate++;
            }
            PlayerDeleteEnemyCar();
            AddPlayerGameItem();
        }

        private void PlayerDeleteEnemyCar()
        {
            for (int i = 0; i < _scene.FastCarEnemies.Count; i++)
            {
                if (_scene.PlayerCar.Equals(_scene.FastCarEnemies[i]))
                {
                    PlayerHealth.ReducePH();
                    _scene.FastCarEnemies.RemoveAt(i);
                }
            }

            for (int i = 0; i < _scene.CarEnemies.Count; i++)
            {
                if (_scene.PlayerCar.Equals(_scene.CarEnemies[i]))
                {
                    PlayerHealth.ReducePH();
                    _scene.CarEnemies.RemoveAt(i);
                }
            }
        } 

        private void AddPlayerGameItem()
        {
            for (int i = 0; i < _scene.Scores.Count; i++)
            {
                if (_scene.Scores[i].DoesPlayerAddScore(_scene.PlayerCar))
                {
                    _scene.Scores.RemoveAt(i);
                    continue;
                }
            }

            for (int i = 0; i < _scene.PlayerHealths.Count; i++)
            {
                if (_scene.PlayerHealths[i].DoesPlayerAddPH(_scene.PlayerCar))
                {
                    _scene.PlayerHealths.RemoveAt(i);
                    continue;
                }
            }
        }

        private bool ChangeEachFrame(int eachFrame)
        {
            if (_countFPS % eachFrame == 0)
            {
                return true;
            }
            return false;
        }

    }
}






