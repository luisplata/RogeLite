using UnityEngine;

namespace City.CameraMovement
{
    public class CameraSetup : MonoBehaviour
    {
        [SerializeField] private GameObject mainCamera;
        [SerializeField] [Range(0, 1)] private float cameraSpeed = 0.01f;

        private void Start()
        {
            // Configurar el controlador de la cámara
            CameraController cameraController = mainCamera.GetComponent<CameraController>();
            cameraController.Configure(new CameraDragController(mainCamera, cameraSpeed));
        }
    }
}