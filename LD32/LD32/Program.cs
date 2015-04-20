using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LD32
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            while (true)
            {
                GameInstance gameInstance = new GameInstance(i);
                Console.WriteLine(GameInstance.Kills);
                i = GameInstance.Kills;
            }
        }
    }
}
