using UnityEngine;
using UnityEngine.EventSystems;

public class FreeCamera : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public CameraController camController;

    private bool isDragging = false;
    private Vector2 dragStartPosition;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isDragging)
        {
            isDragging = true;
            dragStartPosition = eventData.position;
            camController.FreeCamStart();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isDragging)
        {
            isDragging = false;
            camController.FreeCamStop();
        }
    }

    private void Update()
    {
        if (isDragging)
        {
            Vector2 deltaPosition = (Vector2)Input.mousePosition - dragStartPosition;
            float rotationX = -deltaPosition.y * camController.conf.FreeCameraRotationSpeed * Time.deltaTime;
            float rotationY = deltaPosition.x * camController.conf.FreeCameraRotationSpeed * Time.deltaTime;

            camController.RotateCamera(rotationX, rotationY);

            dragStartPosition = Input.mousePosition;
        }
    }
}
