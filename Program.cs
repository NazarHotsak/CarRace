using System;
using System.Threading;


namespace CarRace
{
    class Program
    {
        static UIСontrol UIСontrol;

        static GameSetting gameSetting;

        static Sprite sprite;

        static GameEngine engine;

        static void Main()
        {
            Initialize();

            engine.Run();
        }

        public static void Initialize()
        {
            CreateTableScoresTxt();

            gameSetting = new GameSetting();

            sprite = new Sprite();

            engine = GameEngine.GetGameEngine(gameSetting, sprite);

            UIСontrol = new UIСontrol(engine.MovePlayerCarLeft, engine.MovePlayerCarRight, engine.MovePlayerCarTop, engine.MovePlayerCarBotton);

            Thread uIcontrol = new Thread(UIСontrol.MovePlayerCar);

            uIcontrol.Start();
        }

        public static void CreateTableScoresTxt()
        {
            string isClear;
            if (File.Exists(TabelScores.pathScores) == false)
            {
                FileStream fileStream = new FileStream(TabelScores.pathScores, FileMode.CreateNew);
                fileStream.Dispose();

                using (StreamWriter streamWriter = new StreamWriter(TabelScores.pathScores, false))
                {
                    streamWriter.Write(TabelScores.firstScoresOfPlayer);
                }
            }
            else
            {
                using (StreamReader streamReader = new StreamReader(TabelScores.pathScores))
                {
                    isClear = streamReader.ReadToEnd();
                }
                if (isClear.Length == 0)
                {
                    using (StreamWriter streamWriter = new StreamWriter(TabelScores.pathScores, false))
                    {
                        streamWriter.Write(TabelScores.firstScoresOfPlayer);
                    }
                }
            }
        }
    }
}





/*
 * 
   #########################
   #   |   |       |   |   #
   #       |   |   |       #
   #   |   |       |   |   #
   #       |   |   |       #
   #   |   |       |   |   #
   #       |   |   |       #
   #   |   |       |   |   #
   #       |   |   |       #
   #   |   |       |   |   #
   #       |   |   |       #
   #   |   |       |   |   #
   #       |   |   |       #
   #   |   |       |   |   #
   #       |   |   |       #
   #   |   |       |   |   #
   #       |   |   |       #
   # 0 |   |       |   |   #
   #000    |   |   |       #
   # 0 |   |       |   |   #
   #000    |   |   |       #
   #########################

 * 
 */




