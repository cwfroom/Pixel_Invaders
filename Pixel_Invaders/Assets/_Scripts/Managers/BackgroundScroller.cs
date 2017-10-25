using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

	public float speed;
	Vector2 bgPos;
    static private bool scrolling;

	// Use this for initialization
	void Start () {
        scrolling = true;
	}

	// Update is called once per frame
	void Update () {
        if (scrolling)
        {
            moveBg();
        }
		
	}

	void moveBg(){
		bgPos = new Vector2(0,Time.time * speed);
		GetComponent<Renderer>().material.mainTextureOffset = bgPos;
	}

    public static void EnableScrolling()
    {
        scrolling = true;
    }

    public static void DisableScrolling()
    {
        scrolling = false;
    }
}