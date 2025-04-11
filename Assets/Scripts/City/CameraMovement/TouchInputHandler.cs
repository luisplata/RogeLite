using UnityEngine;

namespace City.CameraMovement
{
    public class TouchInputHandler : ITouchInputHandler
    {
        public bool IsTouching()
        {
            return Input.touchCount > 0;
        }

        public Vector2 GetTouchDelta()
        {
            if (Input.touchCount > 0)
            {
                return Input.GetTouch(0).deltaPosition;
            }
            return Vector2.zero;
        }
    }
}