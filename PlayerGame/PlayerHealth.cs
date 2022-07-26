using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace
{
    internal class PlayerHealth : PlayerGameItems
    {
        public static int PH { get; private set; } = 10;

        public static void ReducePH()
        {
            if (PH > 0)
            {
                PH--;
            }
        }

        public bool DoesPlayerAddPH(GameObjectCar gameObjectCar)
        {
            if (Equals(gameObjectCar))
            {
                PH += 1;
                return true;
            }
            return false;
        }

        public PlayerHealth()
        {
            Figure = new GameObjectPlace[2]
            {
                new GameObjectPlace(){ Item = 'P', YCoordinate = 0},
                new GameObjectPlace(){ Item = 'H', YCoordinate = 0}
            };
        }
    }
}
