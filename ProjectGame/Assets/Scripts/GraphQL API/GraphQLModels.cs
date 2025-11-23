using UnityEngine;
using System;

namespace GraphQLModels
{
    public class PlayerDTO
    {
        public int id;
        public string userName;
        public int level;
        public int experience;
    }

    public class HighScoreDTO
    {
        public int id;
        public int playerId;
        public int score;
        public string achievedAt;
        public PlayerDTO player;
    }

    public class GraphQLResponse<T>
    {
        public T data;
    }

    public class CreatePlayerResult
    {
        public PlayerDTO player;
    }

    public class CreatePlayerData
    {
        public CreatePlayerResult createPlayer;
    }

    public class SubmitScoreResult
    {
        public HighScoreDTO highScore;
    }

    public class SubmitScoreData
    {
        public SubmitScoreResult submitScore;
    }

    public class HighScoresData
    {
        public HighScoreDTO[] highScores;
    }
}
