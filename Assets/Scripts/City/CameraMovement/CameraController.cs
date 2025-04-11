using UnityEngine;

namespace City.CameraMovement
{
    public class CameraController : MonoBehaviour
    {
        private ICameraController cameraController;

        // Inyectamos la implementación del controlador de la cámara
        public void Configure(ICameraController cameraControllerIncomming)
        {
            cameraController = cameraControllerIncomming;
        }

        private void Update()
        {
            // Actualizamos la posición de la cámara
            cameraController.UpdateCameraPosition();
        }
    }
}