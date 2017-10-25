using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : ObjectControl {   
    public int damage;
    
    protected new void Start(){
        base.Start();
    }

    protected new void Update(){
       base.Update();
    }

    public static EnemyControl Create(EnemyData data,Vector3 initialPos)
    {
        GameObject enemy = Instantiate(Resources.Load("Prefabs/" + data.name)) as GameObject;
        enemy.transform.position = initialPos;

        EnemyControl ec = enemy.GetComponent<EnemyControl>();
        ec.speed = data.speed;
        ec.damage = data.damage;
        ec.name = data.name;
        return ec;
    }

    public new void Action(GameManager gm)
    {
        base.Action(gm);
        gm.AddStamina(-damage);
    }
}
