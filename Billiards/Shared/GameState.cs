using Billiards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billiards.Shared
{
    class GameState
    {
        public List<PlayerGameContainer> players { get; set; }
        public int TimeRemaining { get; set; }


    }
}
