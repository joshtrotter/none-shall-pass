using UnityEngine;

//The touch data from a single game frame.
public struct TouchFrame
{
    private float time;
    private Vector2 position;
    private float deltaTime;
    private Vector2 deltaPosition;

    public TouchFrame(float time, Vector2 position, float deltaTime, Vector2 deltaPosition)
    {
        this.time = time;
        this.position = position;
        this.deltaTime = deltaTime;
        this.deltaPosition = deltaPosition;
    }

    public float GetTime()
    {
        return time;
    }

    public Vector2 GetPosition()
    {
        return position;
    }

    public float GetDeltaTime()
    {
        return deltaTime;
    }

    public Vector2 GetDeltaPosition()
    {
        return deltaPosition;
    }
}
