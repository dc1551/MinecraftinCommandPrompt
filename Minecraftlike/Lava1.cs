using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftlike
{
    internal class Lava1 : Lava
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
                    if (i < 4)
                    {
                        for (int j = 0; j < m; j++)
                        {
                            block[h, i, j] = bSpace;
                        }
                    }
                    if (i != m)
                    {
                        block[h, 4, i] = bTop;

                    }
                }
            }
            ModifyBlock();
        }
        public override void ModifyBlock()
        {
        }
    }
}
