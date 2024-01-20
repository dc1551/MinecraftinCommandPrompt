using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftlike
{
    internal class Iron : Ore
    {
        public override void BlockColor(int color)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
        }
    }
}
