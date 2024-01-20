using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftlike
{
    internal class Water4 : Water
    {
        public override void MakeBlock(int n)
        {
            int m = n - 1;
            int p = m;
            bSizeZ = p; bSizeY = n; bSizeX = m;
            block = new string[p, n, m];
            for (int h = 0; h < p; h++)
            {
                for (int i = 0; i < n; i++)
                {
                    if (i > 1 && i < m)
                    {
                        if (h == 0 || h == p - 1)
                        {
                            block[h, i, 0] = bLeft;
                            for (int j = 1; j < m - 1; j++)
                            {
                                block[h, i, j] = bSpace;
                            }
                            block[h, i, m - 1] = bRight;
                        }
                        else
                        {
                            for (int j = 0; j < m; j++)
                            {
                                block[h, i, j] = bSpace;
                            }
                        }
                    }
                    if (i < 1)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            block[h, i, j] = bSpace;
                        }
                    }
                    if (i != m)
                    {
                        block[h, 1, i] = bTop;
                        block[h, m, i] = bBottom;

                    }
                }
            }
            ModifyBlock();
        }
        public override void ModifyBlock()
        {
            for (int h = 0; h < bSizeZ; h++)
            {
                for (int i = 2; i < bSizeY - 1; i++)
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
