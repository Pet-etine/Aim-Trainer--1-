using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;
using System.Text;

public class BackendHandler : MonoBehaviour
{
    // UI Elements
    public TMP_Text playername;
    public TMP_Text hits;
    public TMP_Text acc;
    public TMP_Text playtime;
    public TMP_Text highscoreText;

    bool updateHighScoreTextArea = false;
    const string urlBackendHighScores = "http://localhost/Unity-PHP-Demo-Backend/api/v1/highscores.php";

    HighScores.HighScores hs;

    void Start()
    {
        Debug.Log("BackendHandler started");
        StartCoroutine(GetRequestForHighScores());
    }

    void Update()
    {
        if (updateHighScoreTextArea)
        {
            highscoreText.text = DisplayHighScores(hs);
            updateHighScoreTextArea = false;
        }
    }

    string DisplayHighScores(HighScores.HighScores highScores)
    {
        if (highScores == null || highScores.scores.Length == 0)
        {
            return "No high scores available.";
        }

        string displayText = "Highscores:\n";
        foreach (var score in highScores.scores)
        {
            displayText += $"Player: {score.playername}\n";
            displayText += $"Hits: {score.hits}\n";
            displayText += $"Accuracy: {score.accuracy}%\n";
            displayText += $"Playtime: {score.playtime} minutes\n";
            displayText += "----------------------------\n";
        }

        return displayText;
    }

    public void PostGameResults()
    {
        Debug.Log($"Sending Results: Playername: {playername.text}, Hits: {hits.text}, Accuracy: {acc.text}, Playtime: {playtime.text}");

        StartCoroutine(SendPostRequest());
    }

    private IEnumerator SendPostRequest()
    {
        // Create a new GameResult object with the player's data
        GameResult result = new GameResult(playername.text, hits.text, acc.text, playtime.text);

        // Serialize it to JSON
        string json = JsonUtility.ToJson(result);

        // Log the JSON being sent for debugging
        Debug.Log($"Sending Post Data: {json}");

        // Create a UnityWebRequest with the appropriate headers for JSON
        using (UnityWebRequest www = new UnityWebRequest(urlBackendHighScores, "POST"))
        {
            // Set request header to indicate that we're sending JSON
            www.SetRequestHeader("Content-Type", "application/json");

            // Convert the JSON string to a byte array and set it as the request body
            byte[] jsonToSend = Encoding.UTF8.GetBytes(json);
            www.uploadHandler = new UploadHandlerRaw(jsonToSend);
            www.downloadHandler = new DownloadHandlerBuffer();

            // Send the request and wait for it to finish
            yield return www.SendWebRequest();

            // Handle the response
            if (www.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Post Success: " + www.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Post Error: " + www.error);
            }
        }
    }







    private IEnumerator GetRequestForHighScores()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(urlBackendHighScores))
        {
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Accept", "application/json");

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                byte[] data = webRequest.downloadHandler.data;

                // Check if data is not null or empty
                if (data != null && data.Length > 0)
                {
                    string resultStr = System.Text.Encoding.UTF8.GetString(data);

                    try
                    {
                        HighScores.HighScores hs = JsonUtility.FromJson<HighScores.HighScores>(resultStr);
                        updateHighScoreTextArea = true;
                        Debug.Log("Received: " + resultStr);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError("JSON Parse Error: " + e.Message);
                    }
                }
                else
                {
                    Debug.LogError("Received empty or null response.");
                }
            }
        }
    }

}

[System.Serializable]
public class GameResult
{
    public string playername;
    public string hits;
    public string accuracy;
    public string playtime;

    public GameResult(string playername, string hits, string accuracy, string playtime)
    {
        this.playername = playername;
        this.hits = hits;
        this.accuracy = accuracy;
        this.playtime = playtime;
    }
}
