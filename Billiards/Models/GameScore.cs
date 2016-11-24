using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billiards.Models
{
    [Table ("GameScores")]
    class GameScore
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [ForeignKey (typeof(Games))]
        public int GameId { get; set; }

        [ForeignKey (typeof(Player))]
        public int Player { get; set; }

        public int Score { get; set; }
    }
}
