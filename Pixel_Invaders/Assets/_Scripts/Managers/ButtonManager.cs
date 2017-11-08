using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	

    public void GotoGame()
    {
        SceneManager.LoadScene("level");
    }

    public void GotoOptions()
    {

    }

    public void GotoLeaderBoard()
    {

    }

}
