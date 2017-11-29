using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[System.Serializable]
public class ScoreList {
    public string[] names;
    public int[] scores;
}


public class LeaderboardManager : MonoBehaviour {
    private static LeaderboardManager _instance;
    private const string serverAddr = "http://127.0.0.1:8000";
    public static int highScore;
    public ScoreList scoreList;

    void Awake()
    {
        if (!_instance)
        {
            _instance = this;
        }else
        {
            Destroy(this.gameObject);
            
        }

        DontDestroyOnLoad(this.gameObject);

    }

    // Use this for initialization
    void Start () {
        UpdateScores(true);
	}

    public void UpdateScores(bool updateUI) {
        StartCoroutine(GetScores(updateUI));
    }

    IEnumerator GetScores(bool updateUI)
    {
        UnityWebRequest www = UnityWebRequest.Get(serverAddr);
        yield return www.Send();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            string data = www.downloadHandler.text;
            scoreList = JsonUtility.FromJson<ScoreList>(data);
            if (scoreList != null)
            {
                ButtonManager bm = FindObjectOfType<ButtonManager>();
                if (bm != null)
                {
                    bm.UpdateLeaderboard(getStr());
                }
                
            }
            
        }
    }

    private string getStr()
    {
        string result = "";
        for (int i = 0; i < scoreList.names.Length; i++)
        {
            result += scoreList.names[i] + " " + scoreList.scores[i] + "\n";
        }
        return result;
    }

    public int GetHigh()
    {
        return scoreList.scores[0];
    }
    public int GetLow()
    {
        return scoreList.scores[scoreList.scores.Length-1];
    }

    // Update is called once per frame
    void Update () {
		
	}
}
