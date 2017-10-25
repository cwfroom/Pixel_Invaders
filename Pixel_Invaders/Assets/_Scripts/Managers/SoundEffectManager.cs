using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour {
    private AudioSource[] sePlayers;
    
    public AudioClip acquire;
    public AudioClip cry;
    public AudioClip explode;
    public AudioClip rush;
    public AudioClip jump;
    public AudioClip eat;
    Dictionary<string, AudioClip> seDic;
    
	// Use this for initialization
	void Start () {
        //sePlayer = GetComponent<AudioSource>();
        sePlayers = GetComponents<AudioSource>();
        seDic = new Dictionary<string, AudioClip>();

        seDic.Add("acquire", acquire);
        seDic.Add("cry", cry);
        seDic.Add("explode", explode);
        seDic.Add("rush", rush);
        seDic.Add("jump", jump);
        seDic.Add("eat", eat);
    }

    public void PlaySE(string name)
    {
        AudioClip clip = seDic[name];
        if (clip)
        {
            foreach (AudioSource sePlayer in sePlayers)
            {
                if (!sePlayer.isPlaying)
                {
                    sePlayer.clip = clip;
                    sePlayer.Play();
                    break;
                }
            }
            
        }
    }
	
	
}
