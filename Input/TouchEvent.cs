using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The touch data from the beginning of a touch to the end.
public class TouchEvent {

    private int touchId;
    private TouchPhase currentState;
    private List<TouchFrame> frames;

    private TouchEvent(Touch touch)
    {
        this.touchId = touch.fingerId;
        UpdateState(touch);
        TouchFrame firstFrame = new TouchFrame(Time.time, touch.position, 0f, Vector2.zero);
        this.frames = new List<TouchFrame>() { firstFrame };
    }

    public static TouchEvent Began(Touch touch)
    {
        return new TouchEvent(touch);
    }

    public void Moved(Touch touch)
    {
        UpdateState(touch);

        float frameTime = Time.time;
        Vector2 framePosition = touch.position;
        float frameTimeDelta = frameTime - LastFrame().GetTime();
        Vector2 framePositionDelta = framePosition - LastFrame().GetPosition();
        TouchFrame frame = new TouchFrame(frameTime, framePosition, frameTimeDelta, framePositionDelta);

        frames.Add(frame);
    }

    public void UpdateState(Touch touch)
    {
        this.currentState = touch.phase;
    }

    private TouchFrame FirstFrame()
    {
        return frames[0];
    }

    private TouchFrame LastFrame()
    {
        return frames[frames.Count - 1];
    } 

    public int GetTouchId()
    {
        return touchId;
    }

    public TouchPhase GetCurrentState()
    {
        return currentState;
    }

    public List<TouchFrame> GetFrames()
    {
        return frames;
    }

    public Vector2 GetStartPosition()
    {
        return FirstFrame().GetPosition(); ;
    }

    public Vector2 GetEndPosition()
    {
        return LastFrame().GetPosition();
    }

    public float GetTouchEventTime()
    {
        return LastFrame().GetTime() - FirstFrame().GetTime();
    }

    public Vector2 GetTouchEventPositionDelta()
    {
        return GetEndPosition() - GetStartPosition();
    }

    

}
