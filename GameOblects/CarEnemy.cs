using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace
{
    internal class CarEnemy : GameObjectCar
    {
        public MovementCat movementCat;
        public bool shake = false;

        private readonly byte _movementBottom = 1;
        private readonly byte _carLeftRight = 1;

        public void ShakeCar()
        {
            if (movementCat == MovementCat.RightBotton)
            {
                movementCat = MovementCat.LeftBotton;
            }
            else
            {
                movementCat = MovementCat.RightBotton;
            }
            shake = false;
        }

        public void IsChangeMovement(Scene scene)
        {
            if (СonfrontationWithBorders())
            {
                return;
            }

            if (movementCat == MovementCat.RightBotton)
            {
                DoesChangeMovementOfCar(scene, MovementCat.RightBotton);
            }
            else
            {
                DoesChangeMovementOfCar(scene, MovementCat.LeftBotton);
            }
        }

        private bool СonfrontationWithBorders()
        {
            if (SceneRender.BorderWidth >= this.XCoordinate)
            {
                this.movementCat = MovementCat.RightBotton;
                return true;
            }
            else if (this.XCoordinate + GameObjectCar.CarWidth >= SceneRender.ScreenWidth - SceneRender.BorderWidth)
            {
                this.movementCat = MovementCat.LeftBotton;
                return true;
            }

            return false;
        }

        private void DoesChangeMovementOfCar(Scene scene, MovementCat movement)
        {
            for (int i = 0; i < scene.FastCarEnemies.Count; i++)
            { 
                CheckCar(scene.FastCarEnemies[i], movement);
            }

            for (int i = 0; i < scene.CarEnemies.Count; i++)
            {
                CheckCar(scene.CarEnemies[i], movement);
            }
        }

        private void CheckCar(GameObjectCar gameObjectCar, MovementCat movement)
        {
            if (EqualsCarRight(gameObjectCar) && MovementCat.RightBotton == movement)
            {
                movementCat = MovementCat.LeftBotton;
                if (gameObjectCar is CarEnemy)
                {
                    CarEnemy carEnemy = (CarEnemy)gameObjectCar;
                    carEnemy.movementCat = MovementCat.RightBotton;
                }
                return;
            }
            else if (EqualsCarLeft(gameObjectCar))
            {
                movementCat = MovementCat.RightBotton;
                if (gameObjectCar is CarEnemy)
                {
                    CarEnemy carEnemy = (CarEnemy)gameObjectCar;
                    carEnemy.movementCat = MovementCat.LeftBotton;
                }
                return;
            }
        }

        private bool EqualsCarRight(GameObjectCar gameObject)
        {
            for (int thisY = this.YCoordinate + _movementBottom; thisY <= this.YCoordinate + GameObjectCar.CarHeight + _movementBottom; thisY++)
            {
                for (int y = gameObject.YCoordinate; y <= gameObject.YCoordinate + GameObjectCar.CarHeight; y++)
                {
                    if (thisY == y && (XCoordinate + GameObjectCar.CarWidth  == gameObject.XCoordinate 
                        || XCoordinate + _carLeftRight + GameObjectCar.CarWidth == gameObject.XCoordinate))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool EqualsCarLeft(GameObjectCar gameObject)
        {
            for (int thisY = this.YCoordinate + _movementBottom; thisY <= this.YCoordinate + GameObjectCar.CarHeight + _movementBottom; thisY++)
            {
                for (int y = gameObject.YCoordinate; y <= gameObject.YCoordinate + GameObjectCar.CarHeight; y++)
                {
                    if (thisY == y && (XCoordinate == gameObject.XCoordinate + GameObjectCar.CarWidth 
                        || XCoordinate - _carLeftRight == gameObject.XCoordinate + GameObjectCar.CarWidth))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
