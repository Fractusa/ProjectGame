using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameAPI.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public int EnemiesKilled { get; set; }

        public ICollection<HighScore> HighScores { get; set; } = new List<HighScore>();
    }
}
