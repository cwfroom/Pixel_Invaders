using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingMover : MonoBehaviour {
    public bool moving = true;
    public float speed = 5.0f;
    public float borderY = -16.0f;
    public float resetY = 14.0f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (moving)
        {
            transform.position = transform.position - new Vector3(0, speed, 0) * Time.deltaTime;
            if (transform.position.y < borderY)
            {
                transform.position = new Vector3(0, resetY, 0);
            }
        }
	}
}
