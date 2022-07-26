using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRace
{
    internal class UIСontrol
    {
        public delegate void MovePlayerCarLeft();
        public delegate void MovePlayerCarRight();
        public delegate void MovePlayerCarTop();
        public delegate void MovePlayerCarButtom();
        public MovePlayerCarLeft _left;
        public MovePlayerCarRight _right;
        public MovePlayerCarTop _top;
        public MovePlayerCarButtom _buttom;

        public UIСontrol(MovePlayerCarLeft left, MovePlayerCarRight right, MovePlayerCarTop top, MovePlayerCarButtom buttom)
        {
            _left = left;
            _right = right;
            _top = top;
            _buttom = buttom;
        }

        public void MovePlayerCar()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            while (true)
            {
                if (key.Key.Equals(ConsoleKey.A))
                {
                    _left();
                }
                else if (key.Key.Equals(ConsoleKey.D))
                {
                    _right();
                }
                else if (key.Key.Equals(ConsoleKey.W))
                {
                    _top();
                }
                else if (key.Key.Equals(ConsoleKey.S))
                {
                    _buttom();
                }

                key = Console.ReadKey(true);
            }
        }
    }
}
