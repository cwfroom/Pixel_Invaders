using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleControl : ObjectControl {
    public int value;
    public int stamina;

    public static CollectibleControl Create(CollectibleData data, Vector3 initialPos)
    {
        GameObject collectible = Instantiate(Resources.Load("Prefabs/" + data.name)) as GameObject;
        collectible.transform.position = initialPos;

        CollectibleControl cc = collectible.GetComponent<CollectibleControl>();
        cc.speed = data.speed;
        cc.value = data.value;
        cc.stamina = data.stamina;
        cc.name = data.name;

        return cc;
    }

    public new void Action(GameManager gm)
    {
        //base.Action(gm);  
        gm.AddScore(value);
        gm.AddStamina(stamina);
        transform.position = new Vector3(1000, 1000, 1000);
        PlaySoundWithCallback(gm);
        
    }
    

    public void PlaySoundWithCallback(GameManager gm)
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio)
        {
            audio.Play();
        }
        StartCoroutine(DelayedCallback(audio.clip.length,gm));
    }

    private IEnumerator DelayedCallback(float time, GameManager gm)
    {
        yield return new WaitForSeconds(time);
        base.Action(gm);
    }
}
