/*
 * Currently useless
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Minecraftlike
{
    internal class Player
    {
        public Player() { }
        public override string ToString()
        {
            return @"  ####  " + "\n" +
                   @"  |''|  " + "\n" +
                   @"  |//|  " + "\n" +
                   @" M|  |M " + "\n" +
                   @"|||  |||" + "\n" +
                   @"|||  |||" + "\n" +
                   @"# |--| #" + "\n" +
                   @"  ||||  " + "\n" +
                   @"  ||||  " + "\n" +
                   @"  ####  " + "\n";
        }

    }
}
