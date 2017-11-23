using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryControl : CollectibleControl {
    public new void Action(GameManager gm)
    {
        gm.MakeGhostsVulnerable();
        base.Action(gm);
    }
}
