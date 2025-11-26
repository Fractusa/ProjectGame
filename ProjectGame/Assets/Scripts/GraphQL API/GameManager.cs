using UnityEngine;
using System.Collections;
using GraphQLModels;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GraphQLClient backend;

    private PlayerDTO player;

    private void Start()
    {
        StartCoroutine(Flow());
    }

    //An example for how the flow of data could look like, first we Create a new Player, submit the score of said player and then get all HighScores. 
    //Needs to be adjusted to how the leaderboards would actually function. We'd only create the Player at the start, probably using SteamID through their API.
    //Then start a new Coroutine once the Player finishes their run, so the achieved score is submitted to the backend, in turn enabling it to be displayed in leaderboards.
    private IEnumerator Flow()
    {
        yield return backend.CreatePlayer("User", p => player = p);
        yield return backend.SubmitScore(player.id, 1000, score => { });
        yield return backend.GetHighScores(scores => { });
    }
}
