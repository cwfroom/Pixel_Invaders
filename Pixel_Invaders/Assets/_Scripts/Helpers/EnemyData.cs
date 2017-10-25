using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : ObjectData{
    public int damage;
    
    public EnemyData(string name,float speed, int damage)
    {
        this.name = name;
        this.speed = speed;
        this.damage = damage;
    }

}
