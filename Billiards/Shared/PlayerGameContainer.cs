using Billiards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billiards.Shared
{
    class PlayerGameContainer
    {
        public Player Player { get; set; }
        public int Score { get; set; }

        public int Plays { get; set; }
        public int Fouls { get; set; }
        public int TotalPointsLost { get; set; }
    }
}
