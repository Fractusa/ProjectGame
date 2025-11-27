using ProjectGameAPI.Data;
using ProjectGameAPI.Models;
using HotChocolate;
using HotChocolate.Data;
using Microsoft.EntityFrameworkCore;

namespace ProjectGameAPI.GraphQL
{
    //Queries being the read operations that anyone using the API can use (reading from database)
    public class Query
    {
        //Get all players
        //UseFiltering allows Unity to filter the players eg. using "where" for finding a specific player
        //UseSorting allows for sorting the data by fields such as username, ID, highscore etc.
        [UseFiltering]
        [UseSorting]
        public IQueryable<Player> GetPlayers(AppDbContext context) => context.Players;

        //Get highscores
        [UseFiltering]
        [UseSorting]
        public IQueryable<HighScore> GetHighScores(AppDbContext context) => context.HighScores.Include(p => p.Player);

        public string Hello() => "World";
    }
}
