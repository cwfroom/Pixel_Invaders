using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour {
	public float speed;
    private bool allowMovement;

    // Use this for initialization
    protected void Start () {
        allowMovement = true;
	}
	
	// Update is called once per frame
	protected void Update () {
        if (allowMovement)
        {
            transform.Translate(0, (-Time.deltaTime * speed) / 10, 0);
            if (transform.position.y < -9)
            {
                gameObject.SetActive(false);
                MasterSpawn.buffer[name].Push(gameObject);

            }
        }
        
    }

    public void DisableMovement()
    {
        allowMovement = false;
    }

    protected void Action(GameManager gm)
    {
        gameObject.SetActive(false);
        MasterSpawn.buffer[name].Push(gameObject);
    }
}
