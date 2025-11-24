using UnityEngine;
using System;

namespace GraphQLModels
{
    //Player class as returned from the backend, field names matching the GraphQL JSON fields.
    public class PlayerDTO
    {
        public int id;
        public string username;
        public int level;
        public int experience;
    }

    //HighScore class as returned from the backend, field names matching the GraphQL JSON fields.
    //Includes the scores and the Player who achieved it nested (PlayerDTO player)
    public class HighScoreDTO
    {
        public int id;
        public int playerId;
        public int score;
        public string achievedAt;
        public PlayerDTO player;
    }

    //Wrapper for GraphQL responses, as GraphQL always returns a JSON file wrapped with { "data": { ... }}
    //Where T is the specific response type specified in below classes.
    public class GraphQLResponse<T>
    {
        public T data;
    }

    //GraphQL mutation looks as follows: 
    // ----- MUTATION: createPlayer -----
    //
    // GraphQL response shape:
    // {
    //   "data": {
    //     "createPlayer": {
    //       "player": { ... }
    //     }
    //   }
    // }

    //Object under "createPlayer" in the mutation
    public class CreatePlayerData
    {
        public CreatePlayerResult createPlayer;
    }

    //Object under "player" in the mutation
    public class CreatePlayerResult
    {
        public PlayerDTO player;
    }

    //Mutation for submitScore:
    // ----- MUTATION: submitScore -----
    //
    // GraphQL response shape:
    // {
    //   "data": {
    //     "submitScore": {
    //       "highScore": { ... }
    //     }
    //   }
    // }

    public class SubmitScoreData
    {
        public SubmitScoreResult submitScore;
    }

    public class SubmitScoreResult
    {
        public HighScoreDTO highScore;
    }

    //The GraphQL Query for returning the array of HighScores
    // ----- QUERY: highScores -----
    //
    // GraphQL response shape:
    // {
    //   "data": {
    //     "highScores": [ { ... }, { ... } ]
    //   }
    // }
    public class HighScoresData
    {
        public HighScoreDTO[] highScores;
    }
}
