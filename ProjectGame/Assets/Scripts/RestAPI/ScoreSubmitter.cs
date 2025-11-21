using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;
public class ScoreSubmitter : MonoBehaviour
{

    public string apiUrl = "http://localhost:5283/api/scores"; 
    //Opdater til jeres egne port.
    //Burde bare lave en shared APIUrl et sted, 
    public void SubmitScore(string playerName, float score)
    {
        StartCoroutine(PostScore(playerName, score));
    }

    private IEnumerator PostScore(string playerName, float score)
{
    Score scoreData = new Score { PlayerName = playerName, ScoreTime = score };
    string jsonData = JsonUtility.ToJson(scoreData);

    using (UnityWebRequest request = UnityWebRequest.Post(apiUrl, jsonData, "application/json"))
    {
        
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Score submitted successfully!");
        }
        else
        {
            Debug.LogError("Score submission failed! Error: " + request.error);
        }
    }
}    

}

