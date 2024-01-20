using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftlike
{
    internal class Lava : Clear
    {
        public Lava()
        {
            bTop = "__";
            bSpecialA = ".*";
            bSpecialB = "*.";
        }
        public override void BlockColor(int color)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        public override void ModifyBlock()
        {
            for (int h = 0; h < bSizeZ; h++)
            {
                for (int i = 1; i < bSizeY - 1; i++)
                {
                    int n;
                    if (h == 0 || h == bSizeZ - 1)
                    {
                        n = 1;
                    }
                    else
                    {
                        n = 0;
                    }
                    for (int j = n; j < bSizeX - n; j++)
                    {

                        int chance = random.Next(4);
                        if (chance == 0)
                        {
                            block[h, i, j] = bSpecialA;
                        }
                        else if (chance > 1)
                        {
                            block[h, i, j] = bSpecialB;
                        }
                    }
                }
            }
        }
    }
}
