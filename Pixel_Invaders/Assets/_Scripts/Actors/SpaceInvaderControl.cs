using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceInvaderControl : EnemyControl {
    public float changeLaneInterval = 2.0f;
    private float[] laneX;

    new void Start(){
      base.Start();
      //laneX = MasterSpawn.GetInitialX();
      StartCoroutine(ChangeLaneRoutine());
    }

    void OnEnable()
    {
        StartCoroutine(ChangeLaneRoutine());
    }


    IEnumerator ChangeLaneRoutine(){
        while (true)
        {
            ChangeLane();
            yield return new WaitForSeconds(changeLaneInterval);
        }
    }

    void ChangeLane(){   
        int laneIndex;
        laneX = MasterSpawn.GetInitialX();
        for (laneIndex = 0;laneIndex<laneX.Length;laneIndex++){
          if (Mathf.Abs(transform.position.x-laneX[laneIndex]) < 0.01){
            break;
          }
        }

        if (laneIndex == laneX.Length){
            return;
        }

        if (laneIndex == 0){
          MoveX(laneX[1]);
        }else if(laneIndex == laneX.Length - 1){
          MoveX(laneX[laneX.Length-2]);
        }else{
          int randDirection = Random.Range(0,3);
          if (randDirection < 1){
            MoveX(laneX[laneIndex-1]);
          }else{
            MoveX(laneX[laneIndex+1]);       
          }
        }
    }

    void MoveX(float x){
      StartCoroutine(MoveToPosition(x, 1.0f));
    }

    IEnumerator MoveToPosition(float x, float time)
    {
        float elapsedTime = 0;
        Vector3 startingPos = transform.position;
        while (elapsedTime < time)
        {
            float tx = Mathf.Lerp(transform.position.x, x, (elapsedTime / time));
            transform.position = new Vector3(tx, transform.position.y, transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
    }


    public new void Action(GameManager gm)
    {
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
        StartCoroutine(DelayedCallback(audio.clip.length, gm));
    }

    private IEnumerator DelayedCallback(float time, GameManager gm)
    {
        yield return new WaitForSeconds(time);
        base.Action(gm);
    }
}
