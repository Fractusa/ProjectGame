using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using GraphQLModels;

public class GraphQLClient : MonoBehaviour
{
    [SerializeField] private string graphqlUrl = "http://localhost:5000/graphql";

    [Serializable]
    private class GraphQLRequest
    {
        public string query;
    }

    public IEnumerator CreatePlayer(string username, Action<PlayerDTO> callback)
    {
        string mutation = $@"
            mutation {{
              createPlayer(input: {{ username: ""{username}"" }}) {{
                player {{
                  id
                  username
                  level
                  experience
                }}
              }}
            }}";

        yield return SendRequest<CreatePlayerData>(mutation,
            result => callback(result.createPlayer.player));
    }

    public IEnumerator SubmitScore(int playerId, int score, Action<HighScoreDTO> callback)
    {
        string mutation = $@"
            mutation {{
              submitScore(input: {{ playerId: {playerId}, score: {score} }}) {{
                highScore {{
                  id
                  playerId
                  score
                  achievedAt
                  player {{
                    id
                    username
                  }}
                }}
              }}
            }}";

        yield return SendRequest<SubmitScoreData>(mutation,
            result => callback(result.submitScore.highScore));
    }

    public IEnumerator GetHighScores(Action<HighScoreDTO[]> callback)
    {
        string query = @"
            query {
              highScores(order: { score: DESC }, take: 10) {
                id
                playerId
                score
                achievedAt
                player {
                  id
                  username
                  level
                  experience
                }
              }
            }";

        yield return SendRequest<HighScoresData>(query,
            result => callback(result.highScores));
    }

    private IEnumerator SendRequest<T>(string query, Action<T> callback)
    {
        var body = new GraphQLRequest { query = query };
        string json = JsonUtility.ToJson(body);
        byte[] bytes = Encoding.UTF8.GetBytes(json);

        var request = new UnityWebRequest(graphqlUrl, "POST");
        request.uploadHandler = new UploadHandlerRaw(bytes);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        var response = JsonUtility.FromJson<GraphQLResponse<T>>(request.downloadHandler.text);
        callback(response.data);
    }
}
