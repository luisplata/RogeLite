using UnityEngine;

namespace City.CameraMovement
{
    public interface ITouchInputHandler
    {
        bool IsTouching();
        Vector2 GetTouchDelta();
    }
}