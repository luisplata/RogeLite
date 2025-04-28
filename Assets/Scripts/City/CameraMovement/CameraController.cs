using City.Terrain;
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
            cameraController.OnClick += HandleClick;
        }

        private void Update()
        {
            // Actualizamos la posición de la cámara
            cameraController.UpdateCameraPosition();
        }


        private void HandleClick(Vector3 screenPosition)
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var anchor = hit.collider.GetComponent<TerrainAnchor>();
                if (anchor != null)
                {
                    Debug.Log(
                        $"Click on anchor: {anchor.Id} of terrain: {anchor.OwnerTerrain.Guid} is occupied: {anchor.IsOccupied}",
                        anchor.OwnerTerrain);
                    return;
                }

                var terrain = hit.collider.GetComponent<BaseTerrain>();
                if (terrain != null)
                {
                    Debug.Log($"Click on terrain: {terrain.name}");
                    return;
                }
            }
        }
    }
}