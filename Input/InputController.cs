using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Responsible for mapping touch input into TouchEvents
public class InputController : MonoBehaviour {

    public ActionController actionController;

    private IDictionary<int, TouchEvent> trackedTouches = new Dictionary<int, TouchEvent>();

	void Start () {
        trackedTouches.Clear();	
	}
	
	void Update () {
        //For each touch input create / update the associated TouchEvent
        foreach (Touch t in Input.touches)
        {
            TouchEvent touchEvent = UpdateTouchEvent(t);
            if (touchEvent != null)
            {
                //Notify the ActionController of the updated TouchEvent
                actionController.TriggerActionsFromTouchEvent(touchEvent);
            }
        }
    }

    private TouchEvent UpdateTouchEvent(Touch t)
    {
        TouchEvent touchEvent;
        if (t.phase == TouchPhase.Began)
        {
            //Creating a new TouchEvent
            touchEvent = TouchEvent.Began(t);
            trackedTouches.Add(t.fingerId, touchEvent);
        }
        else if (trackedTouches.TryGetValue(t.fingerId, out touchEvent))
        {
            //Updating an existing TouchEvent
            if (t.phase == TouchPhase.Stationary || t.phase == TouchPhase.Canceled)
            {
                //Just update the state on a hold or a cancel
                touchEvent.UpdateState(t);
            }
            else
            {
                //Add a new TouchFrame on a moved or a completed
                touchEvent.Moved(t);
            }

            if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled)
            {
                //Stop tracking after a completed or a cancel
                trackedTouches.Remove(t.fingerId);
            }
        }

        return touchEvent;
    }

    
}
