using Billiards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billiards.Shared
{
    class UserManager
    {
        /// <summary>
        /// Gets a list of players
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Player> PlayerList()
        {
            return DbContext.DbConnection.Table<Player>().AsEnumerable();
        }


        /// <summary>
        /// Creates a new player
        /// </summary>
        public void CreatePlayer(string name)
        {
            if (name != null && !string.IsNullOrWhiteSpace(name))
            {
                if (DbContext.DbConnection.Table<Player>().Any(p => p.Name.ToLower() == name.ToLower()))
                {
                    Player player = new Player() { Name = name };
                    DbContext.DbConnection.InsertOrReplace(player);
                }else
                {
                    throw new Exception("Player with same name already exists.");
                }
            }
        }
    }
}
