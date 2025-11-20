using ProjectGameAPI.Data;
using ProjectGameAPI.Models;
using HotChocolate;
using HotChocolate.Data;

namespace ProjectGameAPI.GraphQL
{
    //Queries being the read operations that anyone using the API can use (reading from database)
    public class Query
    {
        //Get all players
        [UseFiltering]
        [UseSorting]
        public IQueryable<Player> GetPlayers(AppDbContext context) => context.Players;

        //Get highscores
        [UseFiltering]
        [UseSorting]
        public IQueryable<HighScore> GetHighScores(AppDbContext context) => context.HighScores;

        public string Hello() => "World";
    }
}
