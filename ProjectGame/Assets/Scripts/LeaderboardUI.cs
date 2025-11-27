using System.Collections;
using System.Linq;
using System.Text;
using UnityEngine;
using GraphQLModels;
using TMPro;

public class LeaderboardUI : MonoBehaviour
{
    [SerializeField] private GraphQLClient backend;
    [SerializeField] private TextMeshProUGUI leaderboardText;

    private void OnEnable()
    {
        //When the panel becomes active, load highscores
        StartCoroutine(LoadLeaderboard());
    }

    private IEnumerator LoadLeaderboard()
    {
        //Call the backend to get the highscores
        yield return backend.GetHighScores(scores =>
        {
            //Sorted in descending order, so highest score first -> last.
            var ordered = scores
                .OrderByDescending(s => s.score)
                .ToArray();

            var sb = new StringBuilder();
            sb.AppendLine("Leaderboard");

            int rank = 1;
            foreach (var s in ordered)
            {
                string name = s.player != null ? s.player.username : $"Player {s.playerId}";
                sb.AppendLine($"{rank}. {name} - {s.score}");
                rank++;
            }

            leaderboardText.text = sb.ToString();
        });
    }
}
