namespace CarRace
{
    internal class PlayerGameItems
    {
        public GameObjectPlace[] Figure;
        public const byte ItemWidth = 2;

        public bool Equals(GameObjectCar gameObject)
        {
            for (int thisY = Figure[0].YCoordinate; thisY < Figure[0].YCoordinate + 1; thisY++)
            {
                for (int thisX = Figure[0].XCoordinate; thisX < Figure[0].XCoordinate + ItemWidth; thisX++)
                {
                    for (int y = gameObject.YCoordinate; y < gameObject.YCoordinate + GameObjectCar.CarHeight; y++)
                    {
                        for (int x = gameObject.XCoordinate; x < gameObject.XCoordinate + GameObjectCar.CarWidth; x++)
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
