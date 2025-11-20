using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameAPI.Models
{
    public class HighScore
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int Score { get; set; }
        public DateTime AchievedAt { get; set; }

        public Player Player { get; set; } = default!;
    }
}
