using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {
    public Canvas menuCanvas;
    public Canvas optionsCanvas;

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
        optionsCanvas.gameObject.SetActive(false);
    }

    public void GotoOptions()
    {
        menuCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(true);
    }

    public void GotoLeaderBoard()
    {

    }

}
