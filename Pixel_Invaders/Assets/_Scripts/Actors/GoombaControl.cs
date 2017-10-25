using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoombaControl : EnemyControl {
    public Sprite squish;
    public AudioClip cry;
    public AudioClip stomp;
    public int value = 10;

    public new void Action(GameManager gm)
    {
        if (PlayerControl.IsJumping())
        {
            GetComponent<Animator>().enabled = false;
            GetComponent<SpriteRenderer>().sprite = squish;
            damage = 0;
            gameObject.layer = 12;
            PlaySoundWithCallback(stomp, gm);
        }
        else
        {
            transform.position = new Vector3(1000, 1000, 1000);
            PlaySoundWithCallback(cry,gm);
        }

    }

    void OnEnable()
    {
        gameObject.layer = 9;
        GetComponent<Animator>().enabled = true;
    }

    public void PlaySoundWithCallback(AudioClip clip, GameManager gm)
    {
        AudioSource audio = GetComponent<AudioSource>();
        if (audio)
        {
            audio.clip = clip;
            audio.Play();
        }
        StartCoroutine(DelayedCallback(audio.clip.length, gm));
    }

    private IEnumerator DelayedCallback(float time, GameManager gm)
    {
        yield return new WaitForSeconds(time);
        if (PlayerControl.IsJumping())
        {
            gm.AddScore(10);
        }
        base.Action(gm);
    }

}
