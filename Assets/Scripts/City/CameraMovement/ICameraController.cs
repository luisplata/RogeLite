using System;
using UnityEngine;

namespace City.CameraMovement
{
    public interface ICameraController
    {
        void UpdateCameraPosition();
        event Action<Vector3> OnClick;
    }
}