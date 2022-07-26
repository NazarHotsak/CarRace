using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace
{
    class Score : PlayerGameItems
    {
        public Score()
        {
            Figure = new GameObjectPlace[2]
            {
                new GameObjectPlace(){ Item = 'S', YCoordinate = 0},
                new GameObjectPlace(){ Item = 'S', YCoordinate = 0}
            };
        }

        public bool DoesPlayerAddScore(GameObjectCar gameObjectCar)
        {
            if (Equals(gameObjectCar))
            {
                TabelScores.AddScore();
                return true;
            }
            return false;
        }
    }
}
