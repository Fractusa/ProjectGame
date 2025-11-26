using System;
using System.Collections;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using GraphQLModels;

public class GraphQLClient : MonoBehaviour
{
    //Url for the GraphQL endpoint. Default: "http://localhost:5000/graphql"
    [SerializeField] private string graphqlUrl = "http://localhost:5000/graphql";

    //Represents the body Unity sends requests TO GraphQL server. GraphQL expects JSON like: { "query": "..." }
    [Serializable]
    private class GraphQLRequest
    {
        public string query;
    }

    //Asks the GraphQL backend to Create a Player with the given username. 
    public IEnumerator CreatePlayer(string username, Action<PlayerDTO> callback)
    {
        //GraphQL mutation sent to the backend, matches the mutation setup in the .NET backend.
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

        //Sends the request to the backend with type CreatePlayerData.
        yield return SendRequest<CreatePlayerData>(mutation,
            //Passes the PlayerDTO back, enabling us to make the current player = the player we created.
            result => callback(result.createPlayer.player));
    }

    //Same idea as CreatePlayer, sends the backend a HighScore for a given player. 
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

    //Sends a request for the top HighScores stored in the backend.
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

        //Sends the request, and invokes callback to receive an array of HighScoreDTO, which Unity can use for displaying Leaderboard.
        yield return SendRequest<HighScoresData>(query,
            result => callback(result.highScores));
    }

    //Generic method sending a GraphQL request to our .NET backend, either sending a mutation or a query. 
    //Then deserializes the "data" field into type T
    private IEnumerator SendRequest<T>(string query, Action<T> callback)
    {
        //Uses helper GraphQLRequest to create a new object with field query
        var body = new GraphQLRequest { query = query };
        //Converts the object to a JSON string eg. {"query":"mutation { createPlayer ... }"}
        string json = JsonUtility.ToJson(body);
        //Since the HTTP request sends bytes converts the JSON string into a byte array.
        byte[] bytes = Encoding.UTF8.GetBytes(json);

        //Create a POST request to the GraphQL endpoint
        var request = new UnityWebRequest(graphqlUrl, "POST");
        //Tells Unity to send the byte array from earlier as body. 
        request.uploadHandler = new UploadHandlerRaw(bytes);
        //Lets Unity know to store the response in memory for later use.
        request.downloadHandler = new DownloadHandlerBuffer();
        //Lets the GraphQL endpoint know that the message sent is of type JSON.
        request.SetRequestHeader("Content-Type", "application/json");

        //Starts the Coroutine for the HTTP request asynchronously, yield return tells Unity to pause and let other frames run, then come back when the web request is finished.
        yield return request.SendWebRequest();

        //Converts the response from JSON to a string, automatically put into the type T using GraphQLResponse. 
        var response = JsonUtility.FromJson<GraphQLResponse<T>>(request.downloadHandler.text);
        callback(response.data);
    }
}
