using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectGameAPI.Data;
using ProjectGameAPI.Models;
using HotChocolate;
using HotChocolate.Data;

namespace ProjectGameAPI.GraphQL
{
    //Records are a lot like setting up a class with eg. Username as a property instead it can be setup simply like below.
    //Should handle more or less the same as far as I've understood.
    public record CreatePlayerInput(string Username);
    public record CreatePlayerPayload(Player Player);
    public record SubmitScoreInput(int PlayerId, int Score);
    public record SubmitScorePayload(HighScore HighScore);

    //Mutations being the write operations that can be accessed through the API (writing to database)
    public class Mutation
    {
        //Mutation for creating a player, takes the input sent from Unity (input.Username) and applies default values (Level and Experience).
        //This enables the ability to create a save, albeit this is likely not to be used.
        public async Task<CreatePlayerPayload> CreatePlayerAsync(
            CreatePlayerInput input, AppDbContext context)
        {
            //Creates the player using the input and default values.
            var player = new Player
            {
                Username = input.Username,
                Level = 1,
                Experience = 0
            };

            //Adds the player to the database.
            context.Players.Add(player);
            await context.SaveChangesAsync();

            return new CreatePlayerPayload(player);
        }

        //Mutation for submitting a highscore, takes PlayerId as input along with score value. 
        public async Task<SubmitScorePayload> SubmitScoreAsync(
            SubmitScoreInput input, AppDbContext context)
        {
            //Finds the player attempting to submit a highscore.
            var player = await context.Players.FindAsync(input.PlayerId);
            if (player is null)
            {
                throw new GraphQLException("Player not found");
            }

            //Creates a new highscore which should be linked to the player.
            var score = new HighScore
            {
                PlayerId = player.Id,
                Score = input.Score,
                AchievedAt = DateTime.UtcNow
            };

            //Adds the highscore to the database.
            context.HighScores.Add(score);
            await context.SaveChangesAsync();

            return new SubmitScorePayload(score);
        }
    }
}
