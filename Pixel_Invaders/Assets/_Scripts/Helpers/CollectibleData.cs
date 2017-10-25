using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleData : ObjectData {
    public int value;
    public int stamina;

    public CollectibleData(string name, float speed, int value, int stamina)
    {
        this.speed = speed;
        this.name = name;
        this.value = value;
        this.stamina = stamina;
    }

}
