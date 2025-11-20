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
    //Mutations being the write operations that can be accessed through the API (writing to database)
    public record CreatePlayerInput(string Username);
    public record CreatePlayerPayload(Player Player);
    public record SubmitScoreInput(int PlayerId, int Score);
    public record SubmitScorePayload(HighScore HighScore);

    public class Mutation
    {
        public async Task<CreatePlayerPayload> CreatePlayerAsync(
            CreatePlayerInput input, AppDbContext context)
        {
            var player = new Player
            {
                Username = input.Username,
                Level = 1,
                Experience = 0
            };

            context.Players.Add(player);
            await context.SaveChangesAsync();

            return new CreatePlayerPayload(player);
        }

        public async Task<SubmitScorePayload> SubmitScoreAsync(
            SubmitScoreInput input, AppDbContext context)
        {
            var player = await context.Players.FindAsync(input.PlayerId);
            if (player is null)
            {
                throw new GraphQLException("Player not found");
            }

            var score = new HighScore
            {
                PlayerId = player.Id,
                Score = input.Score,
                AchievedAt = DateTime.UtcNow
            };

            context.HighScores.Add(score);
            await context.SaveChangesAsync();

            return new SubmitScorePayload(score);
        }
    }
}
