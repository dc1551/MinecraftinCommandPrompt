using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftlike
{
    internal class World
    {
        public Block[,,] blocks;
        public int blockSize;
        public Random random = new Random();
        public World(int blockSize, int worldSize)
        {
            this.blockSize = blockSize;
            WorldGenerator(worldSize);
        }
        /// <summary>
        /// Used for making the world intially
        /// </summary>
        /// <param name="worldSize"></param>
        public void WorldGenerator(int worldSize)
        {
            blocks = new Block[worldSize, worldSize, worldSize];
            for (int k = 0; k < worldSize; k++)
            {
                for (int i = 0; i < worldSize; i++)
                {
                    for (int j = 0; j < worldSize; j++)
                    {
                        int chance = random.Next(1000);
                        if (i < (worldSize / 2) - (worldSize / 5) - 2) //this ensures a good portion of the world starts off as air
                        {
                            Air air = new Air();
                            air.MakeBlock(blockSize);
                            blocks[k, i, j] = air;
                        }
                        else //rest of the world is in here because it has random elements
                        {
                            int caveCount = 1;
                            int oreCount = 1;
                            //this section checks for adjacent blocks of the same type to increase the likelihood of another block of the same type spawning
                            for (int z = k - 1; z < k + 1; z++)
                            {
                                for (int y = i - 1; y < i + 1; y++)
                                {
                                    for (int x = j - 1; x < j + 1; x++)
                                    {
                                        if ((z == k && y == i && x == j) || z < 0 || y < 0 || x < 0 || z >= worldSize || y >= worldSize || x >= worldSize || (z == k && y == i && x > j - 1))
                                        {

                                        }
                                        else
                                        {
                                            if (blocks[z, y, x].GetType().IsSubclassOf(typeof(Clear)))
                                            {
                                                caveCount = caveCount * 3 / 2 + 11;
                                            }
                                            else if (blocks[z, y, x].GetType() == typeof(Ore))
                                            {
                                                oreCount = oreCount * 7 / 6 + 5;
                                            }
                                        }
                                    }
                                }
                            }
                            if (i < (worldSize / 2) - (worldSize / 5) - 1) //this layer can be grass, air or water, it's 1 block thick
                            {
                                if (chance < 15)
                                {
                                    WaterSource water = new WaterSource();
                                    water.MakeBlock(blockSize);
                                    blocks[k, i, j] = water;
                                }
                                else if (chance < 949 - caveCount / 2)
                                {
                                    Grass grass = new Grass();
                                    grass.MakeBlock(blockSize);
                                    blocks[k, i, j] = grass;
                                }
                                else
                                {
                                    Air air = new Air();
                                    air.MakeBlock(blockSize);
                                    blocks[k, i, j] = air;
                                }
                            }
                            else if (i < (worldSize / 2) - (worldSize / 5)) //this layer can be dirt (if block above is grass), grass, or water, also 1 block thick
                            {
                                if (chance < 5)
                                {
                                    WaterSource water = new WaterSource();
                                    water.MakeBlock(blockSize);
                                    blocks[k, i, j] = water;
                                }
                                else
                                {
                                    if (blocks[k, i - 1, j].GetType() == typeof(Grass))
                                    {
                                        Dirt dirt = new Dirt();
                                        dirt.MakeBlock(blockSize);
                                        blocks[k, i, j] = dirt;
                                    }
                                    else
                                    {
                                        Grass grass = new Grass();
                                        grass.MakeBlock(blockSize);
                                        blocks[k, i, j] = grass;
                                    }
                                }
                            }
                            else if (i < (worldSize / 2) - (worldSize / 7) || i < 8) //this layer can be dirt or water
                            {
                                if (chance < 5)
                                {
                                    WaterSource water = new WaterSource();
                                    water.MakeBlock(blockSize);
                                    blocks[k, i, j] = water;
                                }
                                else
                                {
                                    Dirt dirt = new Dirt();
                                    dirt.MakeBlock(blockSize);
                                    blocks[k, i, j] = dirt;
                                }
                            }
                            else if (i == worldSize - 1) //this ensures the last layer of the world is bedrock
                            {
                                Bedrock bedrock = new Bedrock();
                                bedrock.MakeBlock(blockSize);
                                blocks[k, i, j] = bedrock;
                            }
                            else //this is everything between bedrock and dirt, can be stone, air, water, or iron, can also be other ores and lava once added
                            {

                                if (chance < 10 + oreCount * 10)
                                {
                                    Iron iron = new Iron();
                                    iron.MakeBlock(blockSize);
                                    blocks[k, i, j] = iron;
                                }
                                else if (chance < 50 + caveCount)
                                {
                                    Air air = new Air();
                                    air.MakeBlock(blockSize);
                                    blocks[k, i, j] = air;
                                }
                                else if (chance < 5 + caveCount / 2)
                                {
                                    WaterSource water = new WaterSource();
                                    water.MakeBlock(blockSize);
                                    blocks[k, i, j] = water;
                                }
                                else
                                {
                                    Stone stone = new Stone();
                                    stone.MakeBlock(blockSize);
                                    blocks[k, i, j] = stone;
                                }
                            }
                        }
                    }
                }
            }
            blocks = AddTrees(blocks);
            blocks = WorldRefresh(blocks);
        }
        /// <summary>
        /// Adds in trees to the world
        /// </summary>
        /// <param name="blocks"></param>
        /// <returns></returns>
        public Block[,,] AddTrees(Block[,,] blocks)
        {
            for (int k = 4; k < blocks.GetLength(0) - 4; k++)
            {
                for (int i = 0; i < blocks.GetLength(1); i++)
                {
                    for (int j = 4; j < blocks.GetLength(2) - 4; j++)
                    {
                        int chance = random.Next(1000);
                        if (i < (blocks.GetLength(1) / 2) - (blocks.GetLength(1) / 5) + 1) //makes sure trees only end up on the topmost layer
                        {
                            if (chance < 20)
                            {
                                if (blocks[k, i + 1, j].GetType() == typeof(Grass)) //trees only spawn on grass
                                {
                                    bool treeClose = true;
                                    //checks for any nearby trees
                                    for (int z = k - 4; z < k + 5; z++)
                                    {
                                        for (int y = i - 1; y < i + 2; y++)
                                        {
                                            for (int x = j - 4; x < j + 5; x++)
                                            {
                                                if (blocks[k, i, j].GetType() == typeof(Wood))
                                                {
                                                    treeClose = false;
                                                }
                                            }
                                        }
                                    }
                                    if (treeClose) // if there isn't, generate the trees
                                    {
                                        for (int z = k - 2; z < k + 3; z++)
                                        {
                                            for (int y = i - 6; y < i + 1; y++)
                                            {
                                                for (int x = j - 2; x < j + 3; x++)
                                                {
                                                    if (y < i - 4)
                                                    {
                                                        if (z > k - 2 && z < k + 2 && x > j - 2 && x < j + 2)
                                                        {
                                                            if ((z == k - 1 && x == j - 1) || (z == k - 1 && x == j + 1) || (z == k + 1 && x == j - 1) || (z == k + 1 && x == j + 1))
                                                            {

                                                            }
                                                            else
                                                            {
                                                                Leaves leaves = new Leaves();
                                                                leaves.MakeBlock(blockSize);
                                                                blocks[z, y, x] = leaves;
                                                            }
                                                        }
                                                    }
                                                    else if (y < i - 2)
                                                    {
                                                        if ((z == k - 2 && x == j - 2) || (z == k - 2 && x == j + 2) || (z == k + 2 && x == j - 2) || (z == k + 2 && x == j + 2))
                                                        {
                                                        }
                                                        else
                                                        {
                                                            Leaves leaves = new Leaves();
                                                            leaves.MakeBlock(blockSize);
                                                            blocks[z, y, x] = leaves;
                                                        }
                                                    }
                                                    if (z == k && x == j && y > i - 6)
                                                    {
                                                        Wood wood = new Wood();
                                                        wood.MakeBlock(blockSize);
                                                        blocks[z, y, x] = wood;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return blocks;
        }
        /// <summary>
        /// Currently used to update water and lava
        /// </summary>
        /// <param name="blocks"></param>
        /// <returns></returns>
        public Block[,,] WorldRefresh(Block[,,] blocks)
        {
            for (int n = 0; n < blocks.GetLength(1) / 8; n++) //number of times this runs for is arbitrary, needs more research to find the best number, does need to be ran multiple times however no matter what
            {
                blocks = WaterMove(blocks);
                blocks = LavaMove(blocks);
            }
            return blocks;
        }
        /// <summary>
        /// Moves the water
        /// </summary>
        /// <param name="blocks"></param>
        /// <returns></returns>
        public Block[,,] WaterMove(Block[,,] blocks)
        {
            for (int k = 0; k < blocks.GetLength(0); k++)
            {
                for (int i = 0; i < blocks.GetLength(1); i++)
                {
                    for (int j = 0; j < blocks.GetLength(2); j++)
                    {
                        if (blocks[k, i, j].GetType() == typeof(WaterFall))
                        {
                            //this checks to make sure the falling water is still connected to a non falling source of water
                            bool sourceCheck = false;
                            for (int y = 0; y < i; y++)
                            {
                                if (blocks[k, i - y, j].GetType().IsSubclassOf(typeof(Water)) && blocks[k, i - y, j].GetType() != typeof(WaterFall))
                                {
                                    sourceCheck = true;
                                    break;
                                }
                                if (!blocks[k, i - y, j].GetType().IsSubclassOf(typeof(Water)))
                                {
                                    break;
                                }
                            }
                            if (!sourceCheck) //if it isn't replace it with air
                            {
                                Air air = new Air();
                                air.MakeBlock(blockSize);
                                blocks[k, i, j] = air;
                            }
                        }
                        if (blocks[k, i, j].GetType().IsSubclassOf(typeof(Water)))
                        {
                            if (!blocks[k, i + 1, j].GetType().IsSubclassOf(typeof(Clear))) //makes sure the block below is solid
                            {
                                int[] offsets = { -1, 0, 1 };
                                //checks to see if there are nearby lava blocks
                                foreach (int offsetX in offsets)
                                {
                                    foreach (int offsetY in offsets)
                                    {
                                        if (offsetX == 0 && offsetY == 0)
                                            continue;

                                        int newK = k + offsetX;
                                        int newJ = j + offsetY;

                                        if (newK >= 0 && newK < blocks.GetLength(0) && newJ >= 0 && newJ < blocks.GetLength(2) && blocks[newK, i, newJ].GetType().IsSubclassOf(typeof(Clear)) && !blocks[newK, i, newJ].GetType().IsSubclassOf(typeof(Water)))
                                        {
                                            if (blocks[newK,i,newJ].GetType().IsSubclassOf(typeof(Lava)) && blocks[k, i, j].GetType() != typeof(Water1)) //if the block the lava is touching isn't the lowest water, make it stone
                                            {
                                                Stone stone = new Stone();
                                                stone.MakeBlock(blockSize);
                                                blocks[newK, i, newJ] = stone;
                                            }
                                            else if (blocks[k, i, j].GetType() == typeof(WaterSource))
                                            {
                                                bool newSource = false;
                                                //checks for other source blocks
                                                int[] offsetsCheck = { -1, 1};
                                                foreach (int offsetCheckX in offsetsCheck)
                                                {
                                                    foreach (int offsetCheckY in offsetsCheck)
                                                    {
                                                        if (newK + offsetCheckX >= 0 && newK + offsetCheckX < blocks.GetLength(0) && newJ + offsetCheckY >= 0 && newJ + offsetCheckY < blocks.GetLength(2) && blocks[newK + offsetCheckX, i, newJ + offsetCheckY].GetType() == typeof(WaterSource))
                                                        {
                                                            if (k == newK + offsetCheckX && j == newJ + offsetCheckY)
                                                            {
                                                            }
                                                            else
                                                            {
                                                                newSource = true; 
                                                                break;
                                                            }
                                                        }
                                                    }
                                                    if (newSource)
                                                    {
                                                        break;
                                                    }
                                                }
                                                if (newSource)
                                                {
                                                    WaterSource water = new WaterSource();
                                                    water.MakeBlock(blockSize);
                                                    blocks[newK, i, newJ] = water;
                                                }
                                                else
                                                {
                                                    Water4 water = new Water4();
                                                    water.MakeBlock(blockSize);
                                                    blocks[newK, i, newJ] = water;
                                                }
                                            }
                                            else if (blocks[k, i, j].GetType() == typeof(Water4))
                                            {
                                                Water3 water = new Water3();
                                                water.MakeBlock(blockSize);
                                                blocks[newK, i, newJ] = water;
                                            }
                                            else if (blocks[k, i, j].GetType() == typeof(Water3))
                                            {
                                                Water2 water = new Water2();
                                                water.MakeBlock(blockSize);
                                                blocks[newK, i, newJ] = water;
                                            }
                                            else if (blocks[k, i, j].GetType() == typeof(Water2))
                                            {
                                                Water1 water = new Water1();
                                                water.MakeBlock(blockSize);
                                                blocks[newK, i, newJ] = water;
                                            }
                                            else if(blocks[k, i, j].GetType() == typeof(WaterFall))
                                            {
                                                Water4 water = new Water4();
                                                water.MakeBlock(blockSize);
                                                blocks[newK, i, newJ] = water;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int y = 1; y < (blocks.GetLength(1) - i); y++)
                                {

                                    if (blocks[k, i + y, j].GetType().IsSubclassOf(typeof(Clear)))
                                    {
                                        if (blocks[k, i + y, j].GetType().IsSubclassOf(typeof(Lava))) //replaces lava below it with stone
                                        {
                                            Stone stone= new Stone();
                                            stone.MakeBlock(blockSize);
                                            blocks[k, i + y, j] = stone;
                                        }
                                        else
                                        {
                                            WaterFall water = new WaterFall();
                                            water.MakeBlock(blockSize);
                                            blocks[k, i + y, j] = water;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return blocks;
        }
        /// <summary>
        /// Functionally very similar to the watermove, just doesn't duplicate source blocks
        /// </summary>
        /// <param name="blocks"></param>
        /// <returns></returns>
        public Block[,,] LavaMove(Block[,,] blocks)
        {
            for (int k = 0; k < blocks.GetLength(0); k++)
            {
                for (int i = 0; i < blocks.GetLength(1); i++)
                {
                    for (int j = 0; j < blocks.GetLength(2); j++)
                    {
                        if (blocks[k, i, j].GetType() == typeof(LavaFall))
                        {
                            bool sourceCheck = false;
                            for (int y = 0; y < i; y++)
                            {
                                if (blocks[k, i - y, j].GetType().IsSubclassOf(typeof(Lava)) && blocks[k, i - y, j].GetType() != typeof(LavaFall))
                                {
                                    sourceCheck = true;
                                    break;
                                }
                                if (!blocks[k, i - y, j].GetType().IsSubclassOf(typeof(Lava)))
                                {
                                    break;
                                }
                            }
                            if (!sourceCheck)
                            {
                                Air air = new Air();
                                air.MakeBlock(blockSize);
                                blocks[k, i, j] = air;
                            }
                        }
                        if (blocks[k, i, j].GetType().IsSubclassOf(typeof(Lava)))
                        {
                            if (!blocks[k, i + 1, j].GetType().IsSubclassOf(typeof(Clear)))
                            {
                                int[] offsets = { -1, 0, 1 };

                                foreach (int offsetX in offsets)
                                {
                                    foreach (int offsetY in offsets)
                                    {
                                        if (offsetX == 0 && offsetY == 0)
                                            continue;

                                        int newK = k + offsetX;
                                        int newJ = j + offsetY;

                                        if (newK >= 0 && newK < blocks.GetLength(0) && newJ >= 0 && newJ < blocks.GetLength(2) && blocks[newK, i, newJ].GetType().IsSubclassOf(typeof(Clear)) && !blocks[newK, i, newJ].GetType().IsSubclassOf(typeof(Lava)))
                                        {
                                            if (blocks[newK, i, newJ].GetType().IsSubclassOf(typeof(Water)) && blocks[k, i, j].GetType() != typeof(Lava1))
                                            {
                                                Stone stone = new Stone();
                                                stone.MakeBlock(blockSize);
                                                blocks[newK, i, newJ] = stone;
                                            }
                                            else if (blocks[k, i, j].GetType() == typeof(LavaSource))
                                            {
                                                Lava4 lava = new Lava4();
                                                lava.MakeBlock(blockSize);
                                                blocks[newK, i, newJ] = lava;
                                            }
                                            else if (blocks[k, i, j].GetType() == typeof(Lava4))
                                            {
                                                Lava3 lava = new Lava3();
                                                lava.MakeBlock(blockSize);
                                                blocks[newK, i, newJ] = lava;
                                            }
                                            else if (blocks[k, i, j].GetType() == typeof(Lava3))
                                            {
                                                Lava2 lava = new Lava2();
                                                lava.MakeBlock(blockSize);
                                                blocks[newK, i, newJ] = lava;
                                            }
                                            else if (blocks[k, i, j].GetType() == typeof(Lava2))
                                            {
                                                Lava1 lava = new Lava1();
                                                lava.MakeBlock(blockSize);
                                                blocks[newK, i, newJ] = lava;
                                            }
                                            else if (blocks[k, i, j].GetType() == typeof(LavaFall))
                                            {
                                                Lava4 lava = new Lava4();
                                                lava.MakeBlock(blockSize);
                                                blocks[newK, i, newJ] = lava;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                for (int y = 1; y < (blocks.GetLength(1) - i); y++)
                                {

                                    if (blocks[k, i + y, j].GetType().IsSubclassOf(typeof(Clear)))
                                    {
                                        if (blocks[k, i + y, j].GetType().IsSubclassOf(typeof(Water)))
                                        {
                                            Stone stone = new Stone();
                                            stone.MakeBlock(blockSize);
                                            blocks[k, i + y, j] = stone;
                                        }
                                        else
                                        {
                                            LavaFall lava = new LavaFall();
                                            lava.MakeBlock(blockSize);
                                            blocks[k, i + y, j] = lava;
                                        }
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return blocks;
        }
        /// <summary>
        /// Used for both placing and breaking blocks despite what name implies
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="z"></param>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <param name="blockType"></param>
        /// <returns></returns>
        public Block[,,] PlaceBlock(Block[,,] blocks, int z, int y, int x, int blockType)
        {
            if (!blocks[z, y, x].GetType().IsSubclassOf(typeof(Unbreakable))) //makes sure it isn't bedrock
            {
                switch (blockType)
                {
                    case 0:
                        Dirt dirt = new Dirt();
                        dirt.MakeBlock(blockSize);
                        blocks[z,y,x] = dirt;
                        break;
                    case 1:
                        Grass grass = new Grass();
                        grass.MakeBlock(blockSize);
                        blocks[z,y,x] = grass;
                        break;
                    case 2:
                        Stone stone = new Stone();
                        stone.MakeBlock(blockSize);
                        blocks[z, y, x] = stone;
                        break;
                    case 3:
                        WaterSource water = new WaterSource();
                        water.MakeBlock(blockSize);
                        blocks[z, y, x] = water;
                        break;
                    case 4:
                        LavaSource lava = new LavaSource();
                        lava.MakeBlock(blockSize);
                        blocks[z, y, x] = lava;
                        break;
                    case 99: //this is the case for breaking blocks
                        Air air = new Air();
                        air.MakeBlock(blockSize);
                        blocks[z, y, x] = air;
                        break;

                }
            }
            blocks = WorldRefresh(blocks);
            return blocks;
        }
        /// <summary>
        /// just used for intializing the main's blocks array
        /// </summary>
        /// <returns></returns>
        public Block[,,] GetWorld()
        {
            return blocks;
        } 
    }
}
