using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftlike
{
    internal class Grass : Dirt
    {
        public Grass() 
        {
            bTop = "MM";
            bSpecialA = "MM";
        }
        public override void BlockColor(int color)
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        public override void ModifyBlock()
        {
            for (int h = 0; h < bSizeZ; h++) {
                for (int i = 0; i < bSizeX; i++)
                {
                    block[h, 1, i] = bSpecialA;
                }
            }
        }
    }
}
