using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData  {
    float getTiredInterval;
    float gainProgressInterval;
    float normalSpeed;

    public LevelData(float getTiredInterval, float gainProgressInterval, float normalSpeed)
    {
        this.getTiredInterval = getTiredInterval;
        this.gainProgressInterval = gainProgressInterval;
        this.normalSpeed = normalSpeed;
    }

	
}
