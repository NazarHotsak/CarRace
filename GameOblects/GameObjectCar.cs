using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace
{
    abstract class GameObjectCar
    {
        public const byte CarWidth = 5;
        public const byte CarHeight = 4;

        public int XCoordinate;
        public int YCoordinate { get; set; }
        public char[]? FigureCar { get; set; }

        public bool Equals(GameObjectCar gameObject)
        {
            for (int thisY = this.YCoordinate; thisY < this.YCoordinate + CarHeight; thisY++)
            {
                for (int thisX = XCoordinate; thisX < XCoordinate + CarWidth; thisX++)
                {
                    for (int y = gameObject.YCoordinate; y < gameObject.YCoordinate + CarHeight; y++)
                    {
                        for (int x = gameObject.XCoordinate; x < gameObject.XCoordinate + CarWidth; x++)
                        {
                            if (thisY == y && thisX == x)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}





