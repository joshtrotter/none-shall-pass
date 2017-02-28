using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MeleeActionTrigger : ActionTrigger
{
    public HeroController hero;
    public LineRenderer lineAttack;

    public float minSwipeDistance = 0.06f; //this value will be converted to a ratio of the screen width
    public float maxSwipeVarianceFromStraightLine = 120f;
    public float attackTime = 1f;

    private bool swipeStarted;

    void Start()
    {
        minSwipeDistance = Screen.width * minSwipeDistance;
    }

    public override void Apply(TouchEvent touchEvent)
    {
        if (!swipeStarted && CheckForSwipeStart(touchEvent))
        {
            StartSwipe(touchEvent);
        }
        //Note this is not an else-if because a swipe can potentially start and end on the same frame
        if (swipeStarted && touchEvent.GetCurrentState() == TouchPhase.Ended)
        {
            if (CheckForSwipeEnd(touchEvent))
            {
                hero.ClearAnimationState();
                StartCoroutine(DrawSwipeAttack(touchEvent));
            } else
            {
                cancelSwipe();
            }
        }

        if (swipeStarted && touchEvent.GetCurrentState() == TouchPhase.Canceled)
        {
            cancelSwipe();
        }


    }

    private bool CheckForSwipeStart(TouchEvent touchEvent)
    {
        if (touchEvent.GetCurrentState() != TouchPhase.Moved && touchEvent.GetCurrentState() != TouchPhase.Ended)
        {
            return false;
        }

        Vector2 totalDistance = touchEvent.GetTouchEventPositionDelta();
        return totalDistance.magnitude >= minSwipeDistance;
    }

    private bool CheckForSwipeEnd(TouchEvent touchEvent)
    {
        float totalDistance = touchEvent.GetTouchEventPositionDelta().magnitude;
        if (totalDistance >= minSwipeDistance)
        {
            Vector2 startPosition = touchEvent.GetStartPosition();
            Vector2 endPosition = touchEvent.GetEndPosition();
            float minLengthAllowed = totalDistance - maxSwipeVarianceFromStraightLine;
            float maxLengthAllowed = totalDistance + maxSwipeVarianceFromStraightLine;
            foreach (TouchFrame frame in touchEvent.GetFrames())
            {
                Vector2 framePosition = frame.GetPosition();
                float frameDistance = Vector2.Distance(startPosition, framePosition) + Vector2.Distance(framePosition, endPosition);
                if (frameDistance < minLengthAllowed || frameDistance > maxLengthAllowed)
                {
                    Debug.Log("Frame distance " + frameDistance + " outside of " + minLengthAllowed + " and " + maxLengthAllowed);
                    return false;
                }
            }
            return true;
        }
        return false;
    }

    private void StartSwipe(TouchEvent touchEvent)
    {
        Vector3 lookPosition;
        if (TouchUtils.TouchPositionToWorldCoords(touchEvent.GetEndPosition(), out lookPosition, 0.5f))
        {
            swipeStarted = true;
            hero.CancelMoveToTarget();
            hero.LookAt(lookPosition, attackTime);
            hero.StartSwipeAttack();
        }          
    }

    private IEnumerator DrawSwipeAttack(TouchEvent touchEvent, float swipeTime = 0.2f)
    {
        int vertexCount = 0;
        List<Vector2> swipe = smoothSwipeInput(simplifySwipeInput(touchEvent));
        foreach (Vector2 frame in swipe)
        {
            Vector3 currentTouchWorld;
            TouchUtils.TouchPositionToWorldCoords(frame, out currentTouchWorld, 0.5f);
            lineAttack.numPositions = vertexCount + 1;
            lineAttack.SetPosition(vertexCount++, currentTouchWorld);
            yield return new WaitForSeconds(swipeTime / swipe.Count);
        }
        swipeStarted = false;
    }

    private List<Vector2> simplifySwipeInput(TouchEvent touchEvent)
    {
        List<Vector2> simplified = new List<Vector2>();
        Vector2 prevPoint = touchEvent.GetStartPosition();
        simplified.Add(prevPoint);

        Vector2 runningDelta = Vector2.zero;
        foreach (TouchFrame frame in touchEvent.GetFrames())
        {
            runningDelta += frame.GetDeltaPosition();
            if (runningDelta.magnitude > minSwipeDistance)
            {
                prevPoint += runningDelta;
                runningDelta = Vector2.zero;
                simplified.Add(prevPoint);
            }
        }

        //if (runningDelta != Vector2.zero)
        //{
        //    simplified.Add(prevPoint += runningDelta);
        //}

        return simplified;
    }

    private List<Vector2> smoothSwipeInput(List<Vector2> simpleSwipe)
    {
        List<Vector2> smoothed = new List<Vector2>(simpleSwipe.Count * 2);

        //first element
        smoothed.Add(simpleSwipe[0]);

        //average elements
        for (int i = 0; i < simpleSwipe.Count - 1; i++)
        {
            Vector2 p0 = simpleSwipe[i];
            Vector2 p1 = simpleSwipe[i + 1];

            Vector2 Q = new Vector2(0.75f * p0.x + 0.25f * p1.x, 0.75f * p0.y + 0.25f * p1.y);
            Vector2 R = new Vector2(0.25f * p0.x + 0.75f * p1.x, 0.25f * p0.y + 0.75f * p1.y);
            smoothed.Add(Q);
            smoothed.Add(R);
        }

        //last element
        smoothed.Add(simpleSwipe[simpleSwipe.Count - 1]);

        return smoothed;
    }

    private void cancelSwipe()
    {
        //TODO
        swipeStarted = false;
    }

}

