using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace City.CameraMovement
{
    public class CameraDragController : ICameraController
    {
        private readonly GameObject camera;
        private Vector3 lastTouchPosition;
        private bool isDragging = false;

        public CameraDragController(GameObject camera)
        {
            this.camera = camera;
        }

        public void UpdateCameraPosition()
        {
            // Touch (Mobile)
            if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
            {
                TouchControl touch = Touchscreen.current.touches[0];
                Vector3 touchPosition = new Vector3(touch.position.x.ReadValue(), touch.position.y.ReadValue(), 0f);

                if (touch.press.wasPressedThisFrame)
                {
                    lastTouchPosition = touchPosition;
                    isDragging = true;
                }
                else if (touch.press.isPressed && isDragging)
                {
                    Vector3 delta = touchPosition - lastTouchPosition;
                    MoveCamera(delta);
                    lastTouchPosition = touchPosition;
                }
                else if (touch.press.wasReleasedThisFrame)
                {
                    isDragging = false;
                }
            }
            // Mouse (Editor/PC)
            else if (Mouse.current != null)
            {
                Vector3 mousePosition = Mouse.current.position.ReadValue();

                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    lastTouchPosition = mousePosition;
                    isDragging = true;
                }
                else if (Mouse.current.leftButton.isPressed && isDragging)
                {
                    Vector3 delta = mousePosition - lastTouchPosition;
                    MoveCamera(delta);
                    lastTouchPosition = mousePosition;
                }
                else if (Mouse.current.leftButton.wasReleasedThisFrame)
                {
                    isDragging = false;
                }
            }
        }

        private readonly float dragSensitivity = 0.005f;

        private void MoveCamera(Vector3 delta)
        {
            Vector3 move = new Vector3(-delta.x * dragSensitivity, 0, -delta.y * dragSensitivity);
            camera.transform.Translate(move, Space.World);
        }
    }
}