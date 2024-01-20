using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftlike
{
    public abstract class Block
    {
        //each of the strings is used for building the block, they are two characters long to keep blocks square shaped, names are self explanatory, except for bSpecial variants, those are for the inside of blocks to add more textures, they usually override bSpace in modify block
        public string bTop = "**";
        public string bBottom = "**";
        public string bLeft = "||";
        public string bRight = "||";
        public string bSpace = "  ";
        public string bSpecialA = "  ";
        public string bSpecialB = "  ";
        public string bSpecialC = "  ";
        public string bSpecialD = "  ";
        public string bSpecialE = "  ";
        public string[,,] block;
        public int bSizeY = 5;
        public int bSizeX = 4;
        public int bSizeZ = 4;
        public Random random = new Random();

        /// <summary>
        /// This constructs blocks
        /// </summary>
        /// <param name="n"></param>
        public virtual void MakeBlock (int n)
        {
            int m = n - 1; //since blocks need to be two characters wide horizontally to be cubes,they also must be 1 shorter on their Z and X axises to account for it
            int p = m;
            bSizeZ = p; bSizeY = n; bSizeX = m;
            block = new string[p,n,m];
            for (int h = 0; h < p; h++) //gets the z coordinate
            {
                for (int i = 0; i < n; i++) //gets the y
                {
                    if (i > 0 && i < m) //checks to see if it is in range for the x
                    {
                        if (h == 0 || h == p-1) //checks to see if it's one of the edges, since it's 3D, it needs to have them be sepearate from the middle
                        {
                            block[h, i, 0] = bLeft;
                            for (int j = 1; j < m - 1; j++)
                            {
                                block[h, i, j] = bSpace;
                            }
                            block[h, i, m - 1] = bRight;
                        }
                        else //otherwise fill in the middle normally
                        {
                            for (int j = 0; j < m; j++)
                            {
                                block[h, i, j] = bSpace;
                            }
                        }
                    }
                    if (i != m) //checks to see if it's in range
                    {
                        block[h, 0, i] = bTop;
                        block[h, m, i] = bBottom;

                    }
                }
            }
            ModifyBlock();
        }

        /// <summary>
        /// Just used for modifying the block in other block subclasses, useless here just a template since bSpecialA is the same as bSpace
        /// </summary>
        public virtual void ModifyBlock()
        {
            for (int h = 0; h < bSizeZ; h++)
            {
                for (int i = 1; i < bSizeY - 1; i++)
                {
                    for (int j = 1; j < bSizeX - 1; j++)
                    {
                        block[h, i, j] = bSpecialA;
                    }
                }
            }
        }

        /// <summary>
        /// First of the block line methods, all pretty similar, main difference is for loop and x vs z, blocks have to be written one line at a time since console doesn't allow for insertion of characters back into window without a bunch of fiddling around
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public virtual string BlockLineNorth(int n)
        {
            string str = ""; //first creates an empty string
            for (int i = 0; i < bSizeX; i++) 
            {
                BlockColor(i); //allows colour to change adaptively, currently not implemented 
                str += (block[0,n,i]); //fills string with appropriate strings from block array
            }
            return str;
        }
        public virtual string BlockLineSouth(int n)
        {
            string str = "";
            for (int i = bSizeX-1; i >= 0; i--)
            {
                BlockColor(i);
                str += (block[0, n, i]);
            }
            return str;
        }
        public virtual string BlockLineWest(int n)
        {
            string str = "";
            for (int i = 0; i < bSizeZ; i++)
            {
                BlockColor(i);
                str += (block[i, n, 0]);
            }
            return str;
        }
        public virtual string BlockLineEast(int n)
        {
            string str = "";
            for (int i = bSizeZ-1; i >= 0; i--)
            {
                BlockColor(i);
                str += (block[i, n, 0]);
            }
            return str;
        }
        public virtual string BlockLineTop(int n)
        {
            string str = "";
            for (int i = 0; i < bSizeX; i++)
            {
                BlockColor(i);
                str += (block[n, 0, i]);
            }
            return str;
        }
        public virtual string BlockLineBottom(int n)
        {
            string str = "";
            for (int i = 0; i < bSizeX; i++)
            {
                BlockColor(i);
                str += (block[n, bSizeY-1, i]);
            }
            return str;
        }
        /// <summary>
        /// Again just used by subclasses 
        /// </summary>
        /// <param name="color"></param>
        public virtual void BlockColor (int color)
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
