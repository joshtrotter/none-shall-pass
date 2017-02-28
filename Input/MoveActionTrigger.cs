using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MoveActionTrigger : ActionTrigger
{
    public HeroController hero;

    public float maxTapTime = 0.2f;
    public float maxTapDistance = 0.04f; //this value will be converted to a ratio of the screen width

    void Start()
    {
        maxTapDistance = Screen.width * maxTapDistance;
    }

    public override void Apply(TouchEvent touchEvent)
    {
        if (touchEvent.GetCurrentState() == TouchPhase.Ended && IsTap(touchEvent))
        {
            Vector3 target;
            if (TouchUtils.TouchPositionToWorldCoords(touchEvent.GetStartPosition(), out target, hero.transform.position.y))
            {
                hero.MoveToTarget(target);
            }
        }
    }

    private bool IsTap(TouchEvent touchEvent)
    {
        return touchEvent.GetTouchEventTime() <= maxTapTime &&
            touchEvent.GetTouchEventPositionDelta().magnitude < maxTapDistance;
    }
}
