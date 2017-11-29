using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour {
    public Canvas menuCanvas;
    public Canvas creditCanvas;
    public Canvas leaderboardCanvas;
    public Text leaderboardText;

	// Use this for initialization
	void Start () {
		
	}
	

    public void GotoGame()
    {
        SceneManager.LoadScene("level");
    }

    public void GotoMainMenu()
    {
        menuCanvas.gameObject.SetActive(true);
        leaderboardCanvas.gameObject.SetActive(false);
        creditCanvas.gameObject.SetActive(false);
    }

    public void GotoLeaderBoard()
    {
        menuCanvas.gameObject.SetActive(false);
        leaderboardCanvas.gameObject.SetActive(true);
        creditCanvas.gameObject.SetActive(false);
    }

    public void GotoCredites()
    {
        menuCanvas.gameObject.SetActive(false);
        leaderboardCanvas.gameObject.SetActive(false);
        creditCanvas.gameObject.SetActive(true);
    }
    
    public void UpdateLeaderboard(string text)
    {
        leaderboardText.text = text;
    }


}
