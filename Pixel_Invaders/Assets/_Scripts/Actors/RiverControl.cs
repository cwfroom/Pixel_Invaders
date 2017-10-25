using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverControl : ObjectControl {
	
	public int damage;
    
	public static RiverControl Create(RiverData data,Vector3 initialPos)
	{
		GameObject river = Instantiate(Resources.Load("Prefabs/" + data.name)) as GameObject;
		river.transform.position = initialPos;

		RiverControl rc = river.GetComponent<RiverControl>();
		rc.speed = data.speed;
		rc.damage = data.damage;
		rc.name = data.name;
		return rc;
	}

	public void Action(GameManager gm)
	{
		gm.AddStamina(-damage);
	}
}
