using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    private GameManager gm;
    private SoundEffectManager se;

    public float lowerX = -1.28f;
    public float initialY = -5.5f;
    public float unit = 0.65f;
    private float[] playerPositions;
	private static bool isJumping;


    private int pos = 2;
    private float screenCenterX;
    private bool movementEnabled;

    public Collider toeCollider;
    public Collider baseCollider;
    private Animator charaAnimator;
    private float jumpAnimLength;
    private float damageAnimLength;

    // Use this for initialization
    void Start () {
        gm = FindObjectOfType<GameManager>();
        if (!gm)
        {
            Debug.Log("Failed to load game manager");
        }
        se = GetComponent<SoundEffectManager>();

        screenCenterX = Screen.width * 0.5f;

        movementEnabled = true;
        playerPositions = new float[5];
        for (int i = 0; i < 5; i++)
        {
            playerPositions[i] = lowerX + unit * i;
        }
        transform.position = new Vector3(playerPositions[pos], initialY,0.1f);

		isJumping = false;
        charaAnimator = GetComponent<Animator>();
        RuntimeAnimatorController ac = charaAnimator.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
        {
            if (ac.animationClips[i].name == "JUMP00")
            {
                jumpAnimLength = ac.animationClips[i].length;
            }
            if (ac.animationClips[i].name == "DAMAGED00")
            {
                damageAnimLength = ac.animationClips[i].length;
            }
            
        }
    }
	
	// Update is called once per frame
	void Update () {
        if (movementEnabled)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (pos > 0)
                {
                    transform.Translate(-0.65f, 0, 0);
                    pos = pos - 1;
                    se.PlaySE("rush");
                }

            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (pos < 4)
                {
                    transform.Translate(0.65f, 0, 0);
                    pos = pos + 1;
                    se.PlaySE("rush");
                }

			}else if (Input.GetKeyDown (KeyCode.Space) && !isJumping) {
                /*
				GetComponent<Rigidbody2D> ().gravityScale = 1.0f;
				GetComponent<Rigidbody2D> ().AddForce (transform.up * 200.0f);
			    */
                Jump();
                
			}

            if (Input.touchCount > 0){
                Debug.Log(Input.touchCount);
            }

            if(Input.touchCount == 2 && !isJumping){
                /*
                GetComponent<Rigidbody2D> ().gravityScale = 1.0f;
                GetComponent<Rigidbody2D> ().AddForce (transform.up * 200.0f);
                */
                Jump();
            }
            else if(Input.touchCount == 1)
            {
                Touch firstTouch = Input.GetTouch(0);

                if (firstTouch.phase == TouchPhase.Began)
                {
                    if (firstTouch.position.x < screenCenterX)
                    {
                        if (pos > 0)
                        {
                            transform.Translate(-0.65f, 0, 0);
                            pos = pos - 1;
                            se.PlaySE("rush");
                        }

                    }
                    else if (firstTouch.position.x > screenCenterX)
                    {
                        if (pos < 4)
                        {
                            transform.Translate(0.65f, 0, 0);
                            pos = pos + 1;
                            se.PlaySE("rush");
                        }
                    }
                }
            }

        }
			


        //Unrestricted movement for debugging
        /*
        float horizontalAxis = Input.GetAxis("Horizontal");
        transform.Translate(Time.deltaTime * horizontalAxis * 5, 0, 0);
        float verticalAxis = Input.GetAxis("Vertical");
        transform.Translate(0, Time.deltaTime * verticalAxis * 5, 0);
        */
    }

    public void EnableMovement()
    {
        movementEnabled = true;
    }

    public void DisableMovement()
    {
        movementEnabled = false;
    }

    public void EnableCollision()
    {
        gameObject.layer = 8;
    }

    public void DisableCollsion()
    {
        gameObject.layer = 12;
    }

    public static bool IsJumping()
    {
        return isJumping;
    }

    private void Jump()
    {
        isJumping = true;
        charaAnimator.SetBool("isJumping?", true);
        se.PlaySE("jump");
        baseCollider.enabled = false;
        Invoke("ResetJumping", jumpAnimLength);
        
    }

    private void ResetJumping()
    {
        baseCollider.enabled = true;
        isJumping = false;
        charaAnimator.SetBool("isJumping?", false);
    }

    private void TakeDamageAnim()
    {
        charaAnimator.SetBool("isDamage?",true);
        Invoke("ResetDamageAnim", damageAnimLength);
    }

    private void ResetDamageAnim()
    {
        charaAnimator.SetBool("isDamage?", false);
    }


    void OnCollisionEnter(Collision collision)
    {
        
        string otherTag = collision.gameObject.tag;
        switch (otherTag) {
            case "Enemy":
                EnemyControl ec = collision.gameObject.GetComponent<EnemyControl>();
                if (ec)
                {
					if (!isJumping)
                    {
                        TakeDamageAnim();
                    }

					if (ec.gameObject.name == "SpaceInvader") {
						SpaceInvaderControl sc = (SpaceInvaderControl)ec;
                        sc.Action(gm);
					} else if (ec.gameObject.name == "Goomba"){
                        GoombaControl gc = (GoombaControl)ec;
                        gc.Action(gm);
                    }else{
                        se.PlaySE("cry");
                        ec.Action(gm);
                    }
                }

                break;
            case "River":
                if (!isJumping)
                {
                    //GetComponent<Renderer>().isVisible = false;
                    RiverControl rc = collision.gameObject.GetComponent<RiverControl>();
                    if (rc)
                    {
                        rc.Action(gm);
                    }
                    gameObject.SetActive(false);
                    GameObject splash = Instantiate(Resources.Load("Prefabs/Splash")) as GameObject;
                    splash.transform.position = gameObject.transform.position;
                }

                break;

            case "Collectible":
                CollectibleControl cc = collision.gameObject.GetComponent<CollectibleControl>();
                if (cc)
                {
                    cc.Action(gm);
                }

                break;
        }



    }

}
