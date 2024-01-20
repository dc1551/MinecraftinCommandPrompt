using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftlike
{
    public class Dirt : Block
    {
        public Dirt()
        {
            bSpecialA = "- ";
            bSpecialB = "--";
            bSpecialC = "_|";
            bSpecialD = ". ";
        }
        public override void BlockColor(int color)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
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
                        switch (chance)
                        {
                            case 0:
                                block[h, i, j] = bSpecialA;
                                break;
                            case 1:
                                block[h, i, j] = bSpecialB;
                                break;
                            case 2:
                                block[h, i, j] = bSpecialC;
                                break;
                            case 3:
                                block[h, i, j] = bSpecialD;
                                break;

                        }
                    }
                }
            }
        }
    }
}
