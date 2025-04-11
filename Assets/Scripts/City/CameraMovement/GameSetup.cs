using UnityEngine;

namespace City.CameraMovement
{
    public class GameSetup : MonoBehaviour
    {
        [SerializeField] private GameObject mainCamera;

        private void Start()
        {
            // Configurar el controlador de la cámara
            CameraController cameraController = mainCamera.GetComponent<CameraController>();
            cameraController.Configure(new CameraDragController(mainCamera));
        }
    }
}