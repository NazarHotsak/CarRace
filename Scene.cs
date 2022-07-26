using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace
{
    class Scene
    {
        public PlayerCar PlayerCar;

        public List<FastCarEnemy> FastCarEnemies;

        public List<CarEnemy> CarEnemies;

        public List<Score> Scores;

        public List<PlayerHealth> PlayerHealths;

        private static Scene _instance;

        public Scene()
        {

        }

        private Scene(GameSetting gameSetting)
        {
            PlayerCar = new PlayerCar() { FigureCar = gameSetting.FigurePlayerCar, XCoordinate = gameSetting.XCoordinatePlayerCar, YCoordinate = gameSetting.YCoordinatePlayerCar };
            FastCarEnemies = new List<FastCarEnemy>();
            CarEnemies = new List<CarEnemy>();
            Scores = new List<Score>();
            PlayerHealths = new List<PlayerHealth>();
        }

        public static Scene GetInstance(GameSetting gameSetting)
        {
            if (_instance == null)
            {
                _instance = new Scene(gameSetting);
            }
            return _instance;
        }
    }
}








