using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombControl : EnemyControl {
    public new void Action(GameManager gm)
    {
        gameObject.transform.position = new Vector3(1000, 1000, 1000);
        PlaySoundWithCallback(gm);
    }


    public void PlaySoundWithCallback(GameManager gm)
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio)
        {
            audio.Play();
        }
        StartCoroutine(DelayedCallback(audio.clip.length, gm));
    }

    private IEnumerator DelayedCallback(float time, GameManager gm)
    {
        yield return new WaitForSeconds(time);
    }
}
