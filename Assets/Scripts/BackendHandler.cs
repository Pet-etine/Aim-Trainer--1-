using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackendHandler : MonoBehaviour
{
const string jsonTestStr = "{ " +
"\"scores\":[ " +
"{\"id\":1, \"playername\":\"Matti\", \"score\":20, \"playtime\": \"2020-21-11 08:20:00\"}, " +
"{\"id\":2, \"playername\":\"Hankka\", \"score\":30, \"playtime\": \"2020-21-11 08:20:00\"}, " +
"{\"id\":3, \"playername\":\"Ismo\", \"score\":40, \"playtime\": \"2020-21-11 08:20:00\"} " +
"] }";
//highscore table
HighScores.HighScores hs;
    void Start()
    {
        Debug.Log("BackendHandler started.");

    //testing json conversion
    hs = JsonUtility.FromJson<HighScores.HighScores>(jsonTestStr);
    Debug.Log("HighScore winner name:" + hs.scores[0].playername);
    Debug.Log("HighScores as json:" + JsonUtility.ToJson(hs));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
