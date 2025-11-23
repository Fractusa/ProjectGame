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

    private IEnumerator Flow()
    {
        yield return backend.CreatePlayer("User", p => player = p);
        yield return backend.SubmitScore(player.id, 1000, score => { });
        yield return backend.GetHighScores(scores => { });
    }
}
