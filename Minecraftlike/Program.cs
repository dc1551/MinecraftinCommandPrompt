/*
 * If comment is present, expect complicated method
 * If not expect it to be pretty simple
 * Notes are also placed, can be ignored if you're not me
 * Also this main is a mess and desperately needs a cleanup but eh
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftlike
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*(@"||\\  //||  ======  ||\\  ||  ||====   _-_  ||#_    //\\    ||====  ====== " + "\n" +
               @"|| \\// ||    ||    || \\ ||  ||==    ((    ||_#   //==\\   ||==      ||" + "\n" +
               @"||  \/  ||  ======  ||  \\||  ||====   -_-  ||\\  //    \\  ||        ||" + "\n" +*/

            //Title Screen *may want to add in splash text like minecraft has that's random
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(@"||\\      //||  ======  ||\\    ||  ||=====     ====    ||===_       //\\      ||=====  ========" + "\n" +
                          @"|| \\    // ||    ||    || \\   ||  ||        /|    ==  ||   _|     //  \\     ||          ||   " + "\n" +
                          @"||  \\  //  ||    ||    ||  \\  ||  ||===    ||         ||===      //====\\    ||===       ||   " + "\n" +
                          @"||   \\//   ||    ||    ||   \\ ||  ||        \|    ==  ||  \\    //      \\   ||          ||   " + "\n" +
                          @"||    \/    ||  ======  ||    \\||  ||=====     ====    ||   \\  //        \\  ||          ||   " + "\n\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(@"  /\   _--  _-_ --- ---   |--- |'\  ---  ----- --- ----- ---  --  |\  |" + "\n" +
                          @" /--\  --- |     |   |    |--  |  |  |     |    |    |    |  |  | | \ |"  + "\n" +
                          @"/    \ __-  -_- --- ---   |--- |,/  ---    |   ---   |   ---  --  |  \|" + "\n\n");
            Console.Write("Press any key to continue");
            Console.ReadKey();
            Console.Clear();
            //Console.WriteLine("Enter how big you want the blocks to be or 0 for the default");
            //string input = Console.ReadLine();
            string input = "5";
            if(int.TryParse(input, out int blockSize)) {
                //Console.WriteLine("Enter how big you want the world");
                //input = Console.ReadLine();
                input = "60";
                if (int.TryParse(input, out int worldSize))
                {
                    //*may want to add a loading screen here
                    World newWorld = new World(blockSize, worldSize); //makes the new world

                    Block[,,] blocks = newWorld.GetWorld(); //transfer it to main

                    int player1Z = worldSize / 2; //makes sure player starts in the middle of the world
                    int player1X = worldSize / 2;
                    int player1Y = UpdateY(blocks, player1Z, 0, player1X);

                    Console.Clear();

                    PrintBlocksNorth(blocks, blockSize, player1Z, player1Y, player1X);
                    PrintPlayer(blockSize);

                    bool check = true;
                    int currentDirection = 0;
                    int upDown = 1;
                    int blockType = 0;
                    while (check) //runs until the player exits *not yet implented
                    {
                        player1Y = UpdateY(blocks,player1Z,player1Y,player1X); //runs at the start to make sure player's y coordinate is always accurate
                        Console.SetCursorPosition(0, Console.WindowHeight-blockSize - 1); //ensures text doesn't run off screen\

                        Console.ForegroundColor = ConsoleColor.White; //important since block change the foreground colour *may need to add another one here for background colour
                        Console.Write("\n" + currentDirection + " Enter the direction you'd like to move in. \n'W' or Up arrow key for forward, 'S' or Down arrow key for back, 'A' or Left arrow key to move left, 'D' or Right arrow key to move right, 'Q' to turn left, 'E' to turn right, 'Z' to look down, 'X' to look up \nPress 'F' to pull up the dig menu, Press 'R' to pull up the block placement menu");
                        ConsoleKeyInfo keyPressed = Console.ReadKey(); //user input
                        if (keyPressed.Key == ConsoleKey.UpArrow || keyPressed.Key == ConsoleKey.W) //this is used for moving forward through the array, essentialy changing what layer you're on
                        {
                            switch (currentDirection)
                            {
                                case 0:
                                    player1Z--;
                                    if (player1Z >= 0)
                                    {
                                        player1Z = MoveCheck(blocks, player1Z, player1Y, player1X, 2);
                                        PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                                    }
                                    else
                                    {
                                        player1Z++;
                                        WorldLimit();
                                    }
                                    break;
                                case 1:
                                    player1X--;
                                    if (player1X >= 0)
                                    {
                                        player1X = MoveCheck(blocks, player1Z, player1Y, player1X, 3);
                                        PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                                    }
                                    else
                                    {
                                        player1X++;
                                        WorldLimit();
                                    }
                                    break;
                                case 2:
                                    player1Z++;
                                    if (player1Z < worldSize)
                                    {
                                        player1Z = MoveCheck(blocks, player1Z, player1Y, player1X, 0);
                                        PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                                    }
                                    else
                                    {
                                        player1Z--;
                                        WorldLimit();
                                    }
                                    break;
                                case 3:
                                    player1X++;
                                    if (player1X < worldSize)
                                    {
                                        player1X = MoveCheck(blocks, player1Z, player1Y, player1X, 1);
                                        PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                                    }
                                    else
                                    {
                                        player1X--;
                                        WorldLimit();
                                    }
                                    break;
                            }

                        }
                        else if (keyPressed.Key == ConsoleKey.DownArrow || keyPressed.Key == ConsoleKey.S) //this is used for moving forward through the array, essentialy changing what layer you're on
                        {
                            switch (currentDirection)
                            {
                                case 0:
                                    player1Z++;
                                    if (player1Z < worldSize)
                                    {
                                        player1Z = MoveCheck(blocks, player1Z, player1Y, player1X, 0);
                                        PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                                    }
                                    else
                                    {
                                        player1Z--;
                                        WorldLimit();
                                    }
                                    break;
                                case 1:
                                    player1X++;
                                    if (player1X < worldSize)
                                    {
                                        player1X = MoveCheck(blocks, player1Z, player1Y, player1X, 1);
                                        PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                                    }
                                    else
                                    {
                                        player1X--;
                                        WorldLimit();
                                    }
                                    break;
                                case 2:
                                    player1Z--;
                                    if (player1Z >= 0)
                                    {
                                        player1Z = MoveCheck(blocks, player1Z, player1Y, player1X, 2);
                                        PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                                    }
                                    else
                                    {
                                        player1Z++;
                                        WorldLimit();
                                    }
                                    break;
                                case 3:
                                    player1X--;
                                    if (player1Z >= 0)
                                    {
                                        player1X = MoveCheck(blocks, player1Z, player1Y, player1X, 3);
                                        PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                                    }
                                    else
                                    {
                                        player1X++;
                                        WorldLimit();
                                    }
                                    break;
                            }

                        }
                        else if (keyPressed.Key == ConsoleKey.LeftArrow || keyPressed.Key == ConsoleKey.A) //for moving left, keeps you on the same layer
                        {
                            switch (currentDirection)
                            {
                                case 0:
                                    player1X--;
                                    if (player1X >= 0)
                                    {
                                        player1X = MoveCheck(blocks, player1Z, player1Y, player1X, 3);
                                        PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                                    }
                                    else
                                    {
                                        player1X++;
                                        WorldLimit();
                                    }
                                    break;
                                case 1:
                                    player1Z--;
                                    if (player1Z >= 0)
                                    {
                                        player1Z = MoveCheck(blocks, player1Z, player1Y, player1X, 2);
                                        PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                                    }
                                    else
                                    {
                                        player1Z++;
                                        WorldLimit();
                                    }
                                    break;
                                case 2:
                                    player1X++;
                                    if (player1X < worldSize)
                                    {
                                        player1X = MoveCheck(blocks, player1Z, player1Y, player1X, 1);
                                        PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                                    }
                                    else
                                    {
                                        player1X--;
                                        WorldLimit();
                                    }
                                    break;
                                case 3:
                                    player1Z++;
                                    if (player1Z < worldSize)
                                    {
                                        player1Z = MoveCheck(blocks, player1Z, player1Y, player1X, 0);
                                        PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                                    }
                                    else
                                    {
                                        player1Z--;
                                        WorldLimit();
                                    }
                                    break;
                            }
                        }
                        else if (keyPressed.Key == ConsoleKey.RightArrow || keyPressed.Key == ConsoleKey.D) //for moving right, keeps you on the same layer
                        {
                            switch (currentDirection)
                            {
                                case 0:
                                    player1X++;
                                    if (player1X < worldSize)
                                    {
                                        player1X = MoveCheck(blocks, player1Z, player1Y, player1X, 1);
                                        PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                                    }
                                    else
                                    {
                                        player1X--;
                                        WorldLimit();
                                    }
                                    break;
                                case 1:
                                    player1Z++;
                                    if (player1Z < worldSize)
                                    {
                                        player1Z = MoveCheck(blocks, player1Z, player1Y, player1X, 0);
                                        PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                                    }
                                    else
                                    {
                                        player1Z--;
                                        WorldLimit();
                                    }
                                    break;
                                case 2:
                                    player1X--;
                                    if (player1X >= 0)
                                    {
                                        player1X = MoveCheck(blocks, player1Z, player1Y, player1X, 3);
                                        PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                                    }
                                    else
                                    {
                                        player1X++;
                                        WorldLimit();
                                    }
                                    break;
                                case 3:
                                    player1Z--;
                                    if (player1Z >= 0)
                                    {
                                        player1Z = MoveCheck(blocks, player1Z, player1Y, player1X, 2);
                                        PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                                    }
                                    else
                                    {
                                        player1Z++;
                                        WorldLimit();
                                    }
                                    break;
                            }
                        }

                        else if (keyPressed.Key == ConsoleKey.Q) //turns you left, so counterclockwise
                        {
                            currentDirection--;
                            if (currentDirection < 0)
                            {
                                currentDirection = 3;
                            }
                            PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                        }
                        else if (keyPressed.Key == ConsoleKey.E) //turns you right, so clockwise
                        {
                            currentDirection++;
                            if (currentDirection > 3)
                            {
                                currentDirection = 0;
                            }
                            PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                        }
                        else if (keyPressed.Key == ConsoleKey.Z) //makes you look down
                        {
                            upDown--;
                            if (upDown < 0)
                            {
                                upDown = 0;
                            }
                            PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                        }
                        else if (keyPressed.Key == ConsoleKey.X) //makes you look up
                        {
                            upDown++;
                            if (upDown > 2)
                            {
                                upDown = 2;
                            }
                            PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                        }
                        else if (keyPressed.Key == ConsoleKey.F) //used for showing the text for the dig menu
                        {
                            for (int i = 1; i < 6; i++)
                            {
                                Console.SetCursorPosition(0, Console.WindowHeight - i);
                                Console.Write(new String(' ', Console.BufferWidth));
                            }
                            Console.SetCursorPosition(0, Console.WindowHeight - 5);
                            Console.Write("Your options are: 'Shift' + one of the following keys:  'Y': Above 'T': Top Left 'U': Top Right 'G': Bottom Left 'J': Bottom Right 'H': Below");
                            keyPressed = Console.ReadKey();
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        }
                        else if (keyPressed.Key == ConsoleKey.R) //used for showing the text for the build menu
                        {
                            for (int i = 1; i < 6; i++)
                            {
                                Console.SetCursorPosition(0, Console.WindowHeight - i);
                                Console.Write(new String(' ', Console.BufferWidth));
                            }
                            Console.SetCursorPosition(0, Console.WindowHeight - 5);
                            Console.Write("Your options are: '0': Dirt, '1': Grass, '2': Stone '3': Water '4': Lava \nAfter selecting your block type, your options are: 'Y': Above 'T': Top Left 'U': Top Right 'G': Bottom Left 'J': Bottom Right 'H': Below");
                            keyPressed = Console.ReadKey();
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                        }

                        if (keyPressed.Key == ConsoleKey.D0) //following numbers correspond to selecting a block
                        {
                            blockType = 0;
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            keyPressed = Console.ReadKey();
                        }
                        else if (keyPressed.Key == ConsoleKey.D1)
                        {
                            blockType = 1;
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            keyPressed = Console.ReadKey();
                        }
                        else if (keyPressed.Key == ConsoleKey.D2)
                        {
                            blockType = 2;
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            keyPressed = Console.ReadKey();
                        }
                        else if (keyPressed.Key == ConsoleKey.D3)
                        {
                            blockType = 3;
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            keyPressed = Console.ReadKey();
                        }
                        else if (keyPressed.Key == ConsoleKey.D4)
                        {
                            blockType = 4;
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            Console.Write(" ");
                            Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                            keyPressed = Console.ReadKey();
                        }
                        
                        if (keyPressed.Key == ConsoleKey.Y && (keyPressed.Modifiers & ConsoleModifiers.Shift) != 0) //digging up
                        {
                            blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y - 2, player1X, 99);
                            PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                        }
                        else if (keyPressed.Key == ConsoleKey.T && (keyPressed.Modifiers & ConsoleModifiers.Shift) != 0) //digging top left
                        {
                            switch (currentDirection)
                            {
                                case 0:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y - 1, player1X - 1, 99);
                                    break;
                                case 1:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z - 1, player1Y - 1, player1X, 99);
                                    break;
                                case 2:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y - 1, player1X + 1, 99);
                                    break;
                                case 3:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z + 1, player1Y - 1, player1X, 99);
                                    break;
                            }
                            PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                        }
                        else if (keyPressed.Key == ConsoleKey.U && (keyPressed.Modifiers & ConsoleModifiers.Shift) != 0) //digging top right
                        {
                            switch (currentDirection)
                            {
                                case 0:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y - 1, player1X + 1, 99);
                                    break;
                                case 1:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z + 1, player1Y - 1, player1X, 99);
                                    break;
                                case 2:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y - 1, player1X - 1, 99);
                                    break;
                                case 3:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z - 1, player1Y - 1, player1X, 99);
                                    break;
                            }
                            PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                        }
                        else if (keyPressed.Key == ConsoleKey.G && (keyPressed.Modifiers & ConsoleModifiers.Shift) != 0) //digging bottom left
                        {
                            switch (currentDirection)
                            {
                                case 0:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y, player1X - 1, 99);
                                    break;
                                case 1:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z - 1, player1Y, player1X, 99);
                                    break;
                                case 2:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y, player1X + 1, 99);
                                    break;
                                case 3:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z + 1, player1Y, player1X, 99);
                                    break;
                            }
                            PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                        }
                        else if (keyPressed.Key == ConsoleKey.J && (keyPressed.Modifiers & ConsoleModifiers.Shift) != 0) //digging bottom right
                        {
                            switch (currentDirection)
                            {
                                case 0:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y, player1X + 1, 99);
                                    break;
                                case 1:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z + 1, player1Y, player1X, 99);
                                    break;
                                case 2:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y, player1X - 1, 99);
                                    break;
                                case 3:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z - 1, player1Y, player1X, 99);
                                    break;
                            }
                            PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                        }
                        else if (keyPressed.Key == ConsoleKey.H && (keyPressed.Modifiers & ConsoleModifiers.Shift) != 0) //digging below
                        {
                            blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y + 1, player1X, 99);
                            PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                        }
                        else if (keyPressed.Key == ConsoleKey.Y) //placing block above
                        {
                            blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y - 2, player1X, blockType);
                            PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                        }
                        else if (keyPressed.Key == ConsoleKey.T) //placing block top left
                        {
                            switch (currentDirection)
                            {
                                case 0:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y - 1, player1X - 1, blockType);
                                    break;
                                case 1:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z - 1, player1Y - 1, player1X, blockType);
                                    break;
                                case 2:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y - 1, player1X + 1, blockType);
                                    break;
                                case 3:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z + 1, player1Y - 1, player1X, blockType);
                                    break;
                            }
                            PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                        }
                        else if (keyPressed.Key == ConsoleKey.U) //placing block top right
                        {
                            switch (currentDirection)
                            {
                                case 0:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y - 1, player1X + 1, blockType);
                                    break;
                                case 1:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z + 1, player1Y - 1, player1X, blockType);
                                    break;
                                case 2:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y - 1, player1X - 1, blockType);
                                    break;
                                case 3:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z - 1, player1Y - 1, player1X, blockType);
                                    break;
                            }
                            PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                        }
                        else if (keyPressed.Key == ConsoleKey.G)  //placing block bottom left
                        {
                            switch (currentDirection)
                            {
                                case 0:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y, player1X - 1, blockType);
                                    break;
                                case 1:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z - 1, player1Y, player1X, blockType);
                                    break;
                                case 2:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y, player1X + 1, blockType);
                                    break;
                                case 3:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z + 1, player1Y, player1X, blockType);
                                    break;
                            }
                            PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                        }
                        else if (keyPressed.Key == ConsoleKey.J)  //placing block bottom right
                        {
                            switch (currentDirection)
                            {
                                case 0:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y, player1X + 1, blockType);
                                    break;
                                case 1:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z + 1, player1Y, player1X, blockType);
                                    break;
                                case 2:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y, player1X - 1, blockType);
                                    break;
                                case 3:
                                    blocks = newWorld.PlaceBlock(blocks, player1Z - 1, player1Y, player1X, blockType);
                                    break;
                            }
                            PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                        }
                        else if (keyPressed.Key == ConsoleKey.H) //placing block below (at player's feet)
                        {
                            if (blocks[player1Z, player1Y - 2, player1X].GetType().IsSubclassOf(typeof(Clear)))
                            {
                                blocks = newWorld.PlaceBlock(blocks, player1Z, player1Y, player1X, blockType);
                            }
                            PrintDecider(blocks, blockSize, player1Z, player1Y, player1X, currentDirection, upDown);
                        }

                    }
                }
            }
            Console.ReadKey(); 
        }

        /// <summary>
        /// Super important method, keeps player from hovering in air
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="z"></param>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int UpdateY(Block[,,] blocks, int z, int y, int x)
        {
            bool check = true;
            while (check)
            {
                if (x >= 0 && x < blocks.GetLength(2) && y >= 0 && y+1 < blocks.GetLength(1) && z >= 0 && z < blocks.GetLength(0))
                {
                    if (!blocks[z, y, x].GetType().IsSubclassOf(typeof(Clear))) //if the block the player's head is in isn't clear, move up
                    {
                        y--;
                    }
                    else if (blocks[z, y + 1, x].GetType().IsSubclassOf(typeof(Clear)) && blocks[z, y, x].GetType().IsSubclassOf(typeof(Clear))) //if the block below the player is clear, and the current block they're in is as well, move down
                    {
                        y++;
                    }
                    else
                    {
                        check = false;
                    }
                }
                else //resets player to in range
                {
                    check = false;
                    if (y < 0)
                    {
                        y = 0;
                    }
                    else if (y >= blocks.GetLength(1))
                    {
                        y = blocks.GetLength(1) - 1;
                    }
                }
            }
            return y;
        }

        /// <summary>
        /// Also very important, keeps the player from ascending up ridiculous heights, also lets them autojump
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="z"></param>
        /// <param name="y"></param>
        /// <param name="x"></param>
        /// <param name="currentDirection"></param>
        /// <returns></returns>
        public static int MoveCheck(Block[,,] blocks, int z, int y, int x, int currentDirection)
        {
            switch (currentDirection)
            {
                case 0: //each case is the same just changes whether it affects x or z and if it does ++ or --
                    if (!blocks[z, y - 1, x].GetType().IsSubclassOf(typeof(Clear))) //if the direction the player is trying to go in has a block in their face, put them back
                    {
                        z--;
                    }
                    else if (!blocks[z, y - 2, x].GetType().IsSubclassOf(typeof(Clear)) && !blocks[z, y, x].GetType().IsSubclassOf(typeof(Clear))) //if the middle block is clear but the block above and below aren't, also put them back
                        {
                        z--;
                    }
                    return z;
                case 1:
                    if (!blocks[z, y - 1, x].GetType().IsSubclassOf(typeof(Clear)))
                    {
                        x--;
                    }
                    else if (!blocks[z, y - 2, x].GetType().IsSubclassOf(typeof(Clear)) && !blocks[z, y, x].GetType().IsSubclassOf(typeof(Clear)))
                    {
                        x--;
                    }
                    return x;
                case 2:
                    if (!blocks[z, y - 1, x].GetType().IsSubclassOf(typeof(Clear)))
                    {
                        z++;
                    }
                    else if (!blocks[z, y - 2, x].GetType().IsSubclassOf(typeof(Clear)) && !blocks[z, y, x].GetType().IsSubclassOf(typeof(Clear)))
                    {
                        z++;
                    }
                    return z;
                case 3:
                    if (!blocks[z, y - 1, x].GetType().IsSubclassOf(typeof(Clear)))
                    {
                        x++;
                    }
                    else if (!blocks[z, y - 2, x].GetType().IsSubclassOf(typeof(Clear)) && !blocks[z, y, x].GetType().IsSubclassOf(typeof(Clear)))
                    {
                        x++;
                    }
                    return x;
            }
            return 0;
        }
        /// <summary>
        /// Decides what print statement to use
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="blockSize"></param>
        /// <param name="player1Z"></param>
        /// <param name="player1Y"></param>
        /// <param name="player1X"></param>
        /// <param name="currentDirection"></param>
        /// <param name="upDown"></param>
        public static void PrintDecider(Block [,,] blocks, int blockSize, int player1Z, int player1Y, int player1X, int currentDirection, int upDown)
        {
            player1Y = UpdateY(blocks, player1Z, player1Y, player1X); //updates y here because sometimes the arguements aren't always accurate coming in, ex. player moved forward into a pit 
            Console.Clear();
            if (upDown == 0) //determines what view type to call
            {
                switch (currentDirection) //determines the orientation
                {
                    case 0:
                        PrintBlocksTopNorth(blocks, blockSize, player1Z, player1Y, player1X);
                        break;
                    case 1:
                        PrintBlocksTopWest(blocks, blockSize, player1Z, player1Y, player1X);
                        break;
                    case 2:
                        PrintBlocksTopSouth(blocks, blockSize, player1Z, player1Y, player1X);
                        break;
                    case 3:
                        PrintBlocksTopEast(blocks, blockSize, player1Z, player1Y, player1X);
                        break;
                }
                PrintPlayerTop(blockSize);
            }
            else if (upDown == 2)
            {
                switch (currentDirection)
                {
                    case 0:
                        PrintBlocksBottomNorth(blocks, blockSize, player1Z, player1Y, player1X);
                        break;
                    case 1:
                        PrintBlocksBottomWest(blocks, blockSize, player1Z, player1Y, player1X);
                        break;
                    case 2:
                        PrintBlocksBottomSouth(blocks, blockSize, player1Z, player1Y, player1X);
                        break;
                    case 3:
                        PrintBlocksBottomEast(blocks, blockSize, player1Z, player1Y, player1X);
                        break;
                }
                PrintPlayerTop(blockSize);
            }
            else
            {
                switch (currentDirection)
                {
                    case 0:
                        PrintBlocksNorth(blocks, blockSize, player1Z, player1Y, player1X);
                        break;
                    case 1:
                        PrintBlocksWest(blocks, blockSize, player1Z, player1Y, player1X);
                        break;
                    case 2:
                        PrintBlocksSouth(blocks, blockSize, player1Z, player1Y, player1X);
                        break;
                    case 3:
                        PrintBlocksEast(blocks, blockSize, player1Z, player1Y, player1X);
                        break;
                }
                PrintPlayer(blockSize);
            }
        }

        /// <summary>
        /// First print block method, they're all pretty similar, so no need for comments on all of them yet, WARNING THERE ARE 12!!!
        /// </summary>
        /// <param name="blocks"></param>
        /// <param name="blockSize"></param>
        /// <param name="viewZ"></param>
        /// <param name="viewY"></param>
        /// <param name="viewX"></param>
        public static void PrintBlocksNorth(Block [,,] blocks, int blockSize, int viewZ, int viewY, int viewX)
        {
            int bufferWidth = Console.WindowWidth / (blockSize * 2) + 2; //ensures their inside the bounds of the current console window size
            int bufferHeight = (Console.WindowHeight) / blockSize - 1;

            int gapX = viewX - bufferWidth / 2; //fins what halfway from the player's coordinates would be on the current window
            int gapY = viewY - bufferHeight / 2;
            for (int i = gapY; i < bufferHeight + gapY; i++) //this determines what block to print giving its vertical coordinate
            {
                if (i >= 0 && i < blocks.GetLength(1))
                {
                    for (int n = 0; n < blockSize; n++) //this says what line of the block to print
                    {
                        for (int j = gapX; j < bufferWidth + gapX; j++) //this determines what block to print giving its horizontal coordinate
                        {
                            if (j >= 0 && j < blocks.GetLength(1))
                            {
                                Console.Write(blocks[viewZ, i, j].BlockLineNorth(n));
                            }
                        }
                        Console.WriteLine("");
                    }
                }
            }
        }
        public static void PrintBlocksSouth(Block[,,] blocks, int blockSize, int viewZ, int viewY, int viewX)
        {
            int bufferWidth = Console.WindowWidth / (blockSize * 2) + 2;
            int bufferHeight = (Console.WindowHeight) / blockSize - 1;
            int gapX = viewX - bufferWidth / 2;
            int gapY = viewY - bufferHeight / 2;
            for (int i = gapY; i < bufferHeight + gapY; i++)
            {
                if (i >= 0 && i < blocks.GetLength(1))
                {
                    for (int n = 0; n < blockSize; n++)
                    {
                        for (int j = bufferWidth + gapX; j >= gapX+1; j--)
                        {
                            if (j >= 0 && j < blocks.GetLength(1))
                            {
                                Console.Write(blocks[viewZ, i, j].BlockLineSouth(n));
                            }
                        }
                        Console.WriteLine("");
                    }
                }
            }
        }

        public static void PrintBlocksWest(Block[,,] blocks, int blockSize, int viewZ, int viewY, int viewX)
        {
            int bufferWidth = Console.WindowWidth / (blockSize * 2) + 2;
            int bufferHeight = Console.WindowHeight / blockSize - 1;
            int gapZ = viewZ  - bufferWidth / 2;
            int gapY = viewY  - bufferHeight / 2;
            for (int i = gapY; i < bufferHeight + gapY; i++)
            {
                if (i >= 0 && i < blocks.GetLength(1))
                {
                    for (int n = 0; n < blockSize; n++)
                    {
                        for (int j = gapZ; j < bufferWidth + gapZ; j++)
                        {
                            if (j >= 0 && j < blocks.GetLength(0))
                            {
                                Console.Write(blocks[j, i, viewX].BlockLineWest(n));
                            }
                        }
                        Console.WriteLine("");
                    }
                }
            }
        }
        public static void PrintBlocksEast(Block[,,] blocks, int blockSize, int viewZ, int viewY, int viewX)
        {

            int bufferWidth = Console.WindowWidth / (blockSize * 2) + 2;
            int bufferHeight = Console.WindowHeight / blockSize - 1;
            int gapZ = viewZ - bufferWidth / 2;
            int gapY = viewY - bufferHeight / 2;
            for (int i = gapY; i < bufferHeight + gapY; i++)
            {
                if (i >= 0 && i < blocks.GetLength(1))
                {
                    for (int n = 0; n < blockSize; n++)
                    {
                        for (int j = bufferWidth + gapZ; j >= gapZ+1; j--)
                        {
                            if (j >= 0 && j < blocks.GetLength(0))
                            {
                                Console.Write(blocks[j, i, viewX].BlockLineEast(n));
                            }
                        }
                        Console.WriteLine("");
                    }
                }
            }
        }
        public static void PrintBlocksTopNorth(Block[,,] blocks, int blockSize, int viewZ, int viewY, int viewX)
        {
            int bufferWidth = Console.WindowWidth / (blockSize * 2) + 2;
            int bufferHeight = Console.WindowHeight / (blockSize - 1) - 1;
            int gapX = viewX - bufferWidth / 2;
            int gapZ = viewZ - bufferHeight / 2;
            for (int i = gapZ; i < bufferHeight + gapZ; i++)
            {
                if (i >= 0 && i < blocks.GetLength(2))
                {
                    for (int n = 0; n < blockSize - 1; n++)
                    {
                        for (int j = gapX; j < bufferWidth + gapX; j++)
                        {
                            if (j >= 0 && j < blocks.GetLength(0))
                            {
                                Console.Write(blocks[i, viewY + 1, j].BlockLineTop(n));
                            }
                        }
                        Console.WriteLine("");
                    }
                }
            }
        }
        public static void PrintBlocksTopSouth(Block[,,] blocks, int blockSize, int viewZ, int viewY, int viewX)
        {
            int bufferWidth = Console.WindowWidth / (blockSize * 2) + 2;
            int bufferHeight = Console.WindowHeight / (blockSize-1) - 1;
            int gapX = viewX - bufferWidth / 2;
            int gapZ = viewZ - bufferHeight / 2;
            for (int i = bufferHeight + gapZ-1; i >= gapZ; i--)
            {
                if (i >= 0 && i < blocks.GetLength(2))
                {
                    for (int n = 0; n < blockSize - 1; n++)
                    {
                        for (int j = bufferWidth + gapX; j >= gapX + 1; j--)
                        {
                            if (j >= 0 && j < blocks.GetLength(0))
                            {
                                Console.Write(blocks[i, viewY + 1, j].BlockLineTop(n));
                            }
                        }
                        Console.WriteLine("");
                    }
                }
            }
        }
        public static void PrintBlocksTopWest(Block[,,] blocks, int blockSize, int viewZ, int viewY, int viewX)
        {
            int bufferWidth = Console.WindowWidth / (blockSize * 2) + 2;
            int bufferHeight = Console.WindowHeight / (blockSize - 1) - 1;
            int gapZ = viewZ - bufferWidth / 2;
            int gapX = viewX - bufferHeight / 2;
            for (int i = gapX; i < bufferHeight + gapX; i++)
            {
                if (i >= 0 && i < blocks.GetLength(2))
                {
                    for (int n = 0; n < blockSize - 1; n++)
                    {
                        for (int j = gapZ; j < bufferWidth + gapZ; j++)
                        {
                            if (j >= 0 && j < blocks.GetLength(0))
                            {
                                Console.Write(blocks[j, viewY + 1, i].BlockLineTop(n));
                            }
                        }
                        Console.WriteLine("");
                    }
                }
            }
        }
        public static void PrintBlocksTopEast(Block[,,] blocks, int blockSize, int viewZ, int viewY, int viewX)
        {
            int bufferWidth = Console.WindowWidth / (blockSize * 2) + 2;
            int bufferHeight = Console.WindowHeight / (blockSize - 1) - 1;
            int gapZ = viewZ - bufferWidth / 2;
            int gapX = viewX - bufferHeight / 2;
            for (int i = bufferHeight + gapX - 1; i >= gapX; i--)
            {
                if (i >= 0 && i < blocks.GetLength(2))
                {
                    for (int n = 0; n < blockSize - 1; n++)
                    {
                        for (int j = bufferWidth + gapZ; j >= gapZ + 1; j--)
                        {
                            if (j >= 0 && j < blocks.GetLength(0))
                            {
                                Console.Write(blocks[j, viewY + 1, i].BlockLineTop(n));
                            }
                        }
                        Console.WriteLine("");
                    }
                }
            }
        }

        public static void PrintBlocksBottomNorth(Block[,,] blocks, int blockSize, int viewZ, int viewY, int viewX)
        {
            int bufferWidth = Console.WindowWidth / (blockSize * 2) + 2;
            int bufferHeight = Console.WindowHeight / (blockSize - 1) - 1; 
            int gapX = viewX - bufferWidth / 2;
            int gapZ = viewZ - bufferHeight / 2;
            for (int i = gapZ; i < bufferHeight + gapZ; i++)
            {
                if (i >= 0 && i < blocks.GetLength(2))
                {
                    for (int n = 0; n < blockSize - 1; n++)
                    {
                        for (int j = bufferWidth + gapX; j >= gapX + 1; j--)
                        {
                            if (j >= 0 && j < blocks.GetLength(0))
                            {
                                Console.Write(blocks[i, viewY - 2, j].BlockLineBottom(n));
                            }
                        }
                        Console.WriteLine("");
                    }
                }
            }
        }
        public static void PrintBlocksBottomSouth(Block[,,] blocks, int blockSize, int viewZ, int viewY, int viewX)
        {
            int bufferWidth = Console.WindowWidth / (blockSize * 2) + 2;
            int bufferHeight = Console.WindowHeight / (blockSize - 1) - 1;
            int gapX = viewX - bufferWidth / 2;
            int gapZ = viewZ - bufferHeight / 2;
            for (int i = bufferHeight + gapZ - 1; i >= gapZ; i--)
            {
                if (i >= 0 && i < blocks.GetLength(2))
                {
                    for (int n = 0; n < blockSize - 1; n++)
                    {
                        for (int j = gapX; j < bufferWidth + gapX; j++)
                        {
                            if (j >= 0 && j < blocks.GetLength(0))
                            {
                                Console.Write(blocks[i, viewY - 2, j].BlockLineBottom(n));
                            }
                        }
                        Console.WriteLine("");
                    }
                }
            }
        }
        public static void PrintBlocksBottomWest(Block[,,] blocks, int blockSize, int viewZ, int viewY, int viewX)
        {
            int bufferWidth = Console.WindowWidth / (blockSize * 2) + 2;
            int bufferHeight = Console.WindowHeight / (blockSize - 1) - 1;
            int gapZ = viewZ - bufferWidth / 2;
            int gapX = viewX - bufferHeight / 2;
            for (int i = gapX; i < bufferHeight + gapX; i++)
            {
                if (i >= 0 && i < blocks.GetLength(2))
                {
                    for (int n = 0; n < blockSize - 1; n++)
                    {
                        for (int j = gapZ; j < bufferWidth + gapZ; j++)
                        {
                            if (j >= 0 && j < blocks.GetLength(0))
                            {
                                Console.Write(blocks[j, viewY - 2, i].BlockLineBottom(n));
                            }
                        }
                        Console.WriteLine("");
                    }
                }
            }
        }
        public static void PrintBlocksBottomEast(Block[,,] blocks, int blockSize, int viewZ, int viewY, int viewX)
        {
            int bufferWidth = Console.WindowWidth / (blockSize * 2) + 2;
            int bufferHeight = Console.WindowHeight / (blockSize - 1) - 1;
            int gapZ = viewZ - bufferWidth / 2;
            int gapX = viewX - bufferHeight / 2;
            for (int i = bufferHeight + gapX - 1; i >= gapX; i--)
            {
                if (i >= 0 && i < blocks.GetLength(2))
                {
                    for (int n = 0; n < blockSize - 1; n++)
                    {
                        for (int j = bufferWidth + gapZ; j >= gapZ + 1; j--)
                        {
                            if (j >= 0 && j < blocks.GetLength(0))
                            {
                                Console.Write(blocks[j, viewY - 2, i].BlockLineBottom(n));
                            }
                        }
                        Console.WriteLine("");
                    }
                }
            }
        }

        /// <summary>
        /// Print player is very different from the others *player should eventually be added to a duplicate of the block array or something to keep this more consistent
        /// </summary>
        /// <param name="blockSize"></param>
        public static void PrintPlayer(int blockSize)
        {
            int x = Console.WindowWidth/2 - blockSize + 1; //finds the middle of the screen
            int y = Console.WindowHeight/2 - blockSize * 2; 
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(x,y);
            Console.Write(@"  ####  " + "\n");
            Console.SetCursorPosition(x, y+1);
            Console.Write(@"  |''|  " + "\n");
            Console.SetCursorPosition(x, y+2);
            Console.Write(@"  |//|  " + "\n");
            Console.SetCursorPosition(x, y+3);
            Console.Write(@" M|  |M " + "\n");
            Console.SetCursorPosition(x, y+4);
            Console.Write(@"|||  |||" + "\n");
            Console.SetCursorPosition(x, y+5);
            Console.Write(@"|||  |||" + "\n");
            Console.SetCursorPosition(x, y+6);
            Console.Write(@"# |--| #" + "\n");
            Console.SetCursorPosition(x, y+7);
            Console.Write(@"  ||||  " + "\n");
            Console.SetCursorPosition(x, y+8);
            Console.Write(@"  ||||  " + "\n");
            Console.SetCursorPosition(x, y+9);
            Console.Write(@"  ####  " + "\n");
        }
        /// <summary>
        /// Similar to the above just a bit simpler
        /// </summary>
        /// <param name="blockSize"></param>
        public static void PrintPlayerTop(int blockSize)
        {
            int x = Console.WindowWidth / 2 - blockSize + 1;
            int y = Console.WindowHeight/ 2 - blockSize + 2; 
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.SetCursorPosition(x, y);
            Console.Write(@"        " + "\n");
            Console.SetCursorPosition(x, y + 1);
            Console.Write(@"||####||" + "\n");
            Console.SetCursorPosition(x, y + 2);
            Console.Write(@"||####||" + "\n");
            Console.SetCursorPosition(x, y + 3);
            Console.Write(@"        " + "\n");
        }

        public static void WorldLimit()
        {
            Console.Clear();
            Console.WriteLine("You went too far");
            Console.ReadKey();
        }
    }
}
//original code for creating a demonstration of blocks
/*string[,] block = new string[5, 4];
for (int i = 0; i < 5; i++)
{
    if (i  > 0 && i < 4)
    {
        block[i,0] = "| ";
        for (int j = 1; j < 3; j++)
        {
            block[i, j] = "  ";
        }
        block[i, 3] = " |";
    }
    if (i != 4) {
        block[0, i] = "··";
        block[4, i] = "··";
    }
}
for (int m = 0; m < 5; m++)
{
    for (int i = 0; i < 5; i++)
    {
        for (int n = 0; n < 3; n++)
        {
            for (int j = 0; j < 4; j++)
            {
                Console.Write(block[i, j]);
            }
        }
        Console.Write("\n");
    }
}
Console.ReadKey();*/