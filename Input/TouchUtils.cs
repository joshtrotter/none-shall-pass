using UnityEngine;

class TouchUtils
{
    public static bool TouchPositionToRaycastHit(Vector2 touchPos, out RaycastHit hit)
    {
        int swipeMask = 1 << 8;
        swipeMask = ~swipeMask;
        return Physics.Raycast(Camera.main.ScreenPointToRay(touchPos), out hit, 100f, swipeMask);
    }

    public static bool TouchPositionToWorldCoords(Vector2 touchPos, out Vector3 coords, float height = 0f)
    {
        RaycastHit hit;
        if (TouchPositionToRaycastHit(touchPos, out hit))
        {
            coords = new Vector3(hit.point.x, height, hit.point.z);
            return true;
        }
        coords = Vector3.zero;
        return false;
    }

    public static void DrawTouch(TouchEvent touchEvent)
    {
        Vector2 currentTouch = touchEvent.GetStartPosition();
        foreach (TouchFrame frame in touchEvent.GetFrames())
        {
            Vector2 nextTouch = currentTouch + frame.GetDeltaPosition();
            Vector3 currentTouchWorld;
            Vector3 nextTouchWorld;
            TouchPositionToWorldCoords(currentTouch, out currentTouchWorld, 0.5f);
            TouchPositionToWorldCoords(nextTouch, out nextTouchWorld, 0.5f);
            Debug.Log(currentTouch);
            Debug.DrawLine(currentTouchWorld, nextTouchWorld, Color.red, 5f);
            currentTouch = nextTouch;
        }
    }

}

