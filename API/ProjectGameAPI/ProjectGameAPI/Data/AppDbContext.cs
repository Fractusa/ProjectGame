using Microsoft.EntityFrameworkCore;
using ProjectGameAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectGameAPI.Data
{
    //Represents the EF Core database.
    public class AppDbContext : DbContext
    {
        //Database tables defined with 2 tables: Player and HighScore
        //Data types therein is as specified in their respective classes 
        public DbSet<Player> Players => Set<Player>();
        public DbSet<HighScore> HighScores => Set<HighScore>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
