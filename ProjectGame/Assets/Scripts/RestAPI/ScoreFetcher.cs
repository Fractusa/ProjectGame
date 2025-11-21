using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ScoreFetcher : MonoBehaviour
{

    public string apiUrl = "http://localhost:5283/api/scores";
    //Opdater til jeres port for at det virker.

    public void FetchTopScores()
    {
        Debug.Log("FetchTopScores called from button click!");
        StartCoroutine(GetTopScores());
    }

    private IEnumerator GetTopScores()
    {
        Debug.Log("Sending request to: " + apiUrl);
        using (UnityWebRequest request = UnityWebRequest.Get(apiUrl))
        {
            request.timeout = 10;
            yield return request.SendWebRequest();
            
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("SUCCESS! Response: " + request.downloadHandler.text);
                // Mangler bare noget til at parse JSON til de dele vi vil have.
            }
            else
            {
                Debug.LogError("Request failed. Error: " + request.error);
            }
        }
    }
}

[System.Serializable]
public class Score
{
    public string PlayerName;
    public float ScoreTime;
}

[System.Serializable]
public class ScoreArray
{
    public Score[] scores;
}
