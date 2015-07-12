using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Action;
using RAIN.Core;

[RAINAction]
public class returnDamage : RAINAction
{
    public override void Start(RAIN.Core.AI ai)
    {
        base.Start(ai);
    }

    public override ActionResult Execute(RAIN.Core.AI ai)
	{	Debug.Log("In the action");
		Debug.Log(ai.WorkingMemory.GetItem("myHealth"));
		ai.WorkingMemory.SetItem ("myHealth", 0);
		return ActionResult.SUCCESS;
		Debug.Log(ai.WorkingMemory.GetItem("myHealth"));

    }

    public override void Stop(RAIN.Core.AI ai)
    {
        base.Stop(ai);
    }
}