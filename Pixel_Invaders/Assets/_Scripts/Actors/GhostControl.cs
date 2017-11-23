using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostControl : EnemyControl {
    public Sprite normalSprite;
    public Sprite vulnerableSprite;
    private bool isVulnerable;
    private int originalDamage;

    new void Start()
    {
        isVulnerable = false;
        base.Start();
    }

    public void MakeVulnerable()
    {
        if (isVulnerable)
        {
            return;
        }

        GetComponent<SpriteRenderer>().sprite = vulnerableSprite;
        isVulnerable = true;
        originalDamage = damage;
        damage = 0;
        Invoke("Restore", 2.0f);
    }

    private void Restore()
    {
        GetComponent<SpriteRenderer>().sprite = normalSprite;
        isVulnerable = false;
        damage = originalDamage;

    }

    public new void Action(GameManager gm)
    {
        if (isVulnerable)
        {
            gm.AddScore(originalDamage);
            gameObject.SetActive(false);
            MasterSpawn.buffer[name].Push(gameObject);
        }
        else
        {
            base.Action(gm);
        }
        
    }


}
