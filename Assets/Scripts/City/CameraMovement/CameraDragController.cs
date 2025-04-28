using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using System;

namespace City.CameraMovement
{
    public class CameraDragController : ICameraController
    {
        private readonly GameObject camera;
        private Vector3 lastTouchPosition;
        private bool isDragging = false;
        private readonly float dragSensitivity;
        private readonly float clickThreshold = 10f; // píxeles

        public event Action<Vector3> OnClick; // Posición del click en pantalla

        public CameraDragController(GameObject camera, float dragSensitivity)
        {
            this.camera = camera;
            this.dragSensitivity = dragSensitivity;
        }

        public void UpdateCameraPosition()
        {
            if (Touchscreen.current != null && Touchscreen.current.touches.Count > 0)
            {
                TouchControl touch = Touchscreen.current.touches[0];
                Vector3 touchPosition = new Vector3(touch.position.x.ReadValue(), touch.position.y.ReadValue(), 0f);

                HandleInput(touch.press.wasPressedThisFrame, touch.press.isPressed, touch.press.wasReleasedThisFrame, touchPosition);
            }
            else if (Mouse.current != null)
            {
                Vector3 mousePosition = Mouse.current.position.ReadValue();

                HandleInput(Mouse.current.leftButton.wasPressedThisFrame, Mouse.current.leftButton.isPressed, Mouse.current.leftButton.wasReleasedThisFrame, mousePosition);
            }
        }

        private void HandleInput(bool pressedThisFrame, bool isPressed, bool releasedThisFrame, Vector3 inputPosition)
        {
            if (pressedThisFrame)
            {
                lastTouchPosition = inputPosition;
                isDragging = true;
            }
            else if (isPressed && isDragging)
            {
                Vector3 delta = inputPosition - lastTouchPosition;
                MoveCamera(delta);
                lastTouchPosition = inputPosition;
            }
            else if (releasedThisFrame && isDragging)
            {
                float distance = Vector3.Distance(inputPosition, lastTouchPosition);

                if (distance < clickThreshold)
                {
                    OnClick?.Invoke(inputPosition); // Disparamos el evento de Click
                }

                isDragging = false;
            }
        }

        private void MoveCamera(Vector3 delta)
        {
            Vector3 move = new Vector3(-delta.x * dragSensitivity, 0, -delta.y * dragSensitivity);
            camera.transform.Translate(move, Space.World);
        }
    }
}
