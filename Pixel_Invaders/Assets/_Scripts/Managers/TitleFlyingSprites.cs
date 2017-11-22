using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFlyingSprites : MonoBehaviour {
    public GameObject startPoint;
    public GameObject endPoint;
    public GameObject[] sprites;

    public float interval = 2.0f;
    public float speed = 5.0f;

    private Vector3 dir;
    private GameObject currentSprite;

	// Use this for initialization
	void Start () {
        dir = (endPoint.transform.position - startPoint.transform.position).normalized;
        StartCoroutine(randomSprite());
	}

    IEnumerator randomSprite()
    {
        while (true)
        {
            int rand = Random.Range(0, sprites.Length);
            currentSprite = sprites[rand];
            currentSprite.transform.position = startPoint.transform.position;
            yield return new WaitForSeconds(interval);
        }
    }

    
	
	// Update is called once per frame
	void Update () {
        if (currentSprite)
        {
            currentSprite.transform.position += dir * speed * Time.deltaTime;
            if (Vector3.Distance(currentSprite.transform.position,endPoint.transform.position) < 0.1f )
            {
                currentSprite = null;
            }
        }
	}
}
