using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverData : ObjectData{
	public int damage;

	public RiverData(string name,float speed, int damage)
	{
		this.name = name;
		this.speed = speed;
		this.damage = damage;
	}

}
