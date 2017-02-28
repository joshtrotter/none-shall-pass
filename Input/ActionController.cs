using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {

    public ActionTrigger[] actionTriggers;

    public void TriggerActionsFromTouchEvent(TouchEvent touchEvent)
    {
        foreach (ActionTrigger actionTrigger in actionTriggers)
        {
            actionTrigger.Apply(touchEvent);
        }
    }
}
