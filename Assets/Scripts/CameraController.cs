using Assets.Scripts.Conf.Scripts;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public CameraConfig conf;
    public Transform camTarget;

    private bool isFreeCamera = false;

    private Quaternion startRotation;

    private void Start()
    {
        transform.position = camTarget.position;
        transform.rotation = camTarget.rotation;
        startRotation = transform.rotation;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, camTarget.position, conf.pLerp);

        if (!isFreeCamera)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, camTarget.rotation, conf.rLerp);
        }
    }

    public void FreeCamStart()
    {
        isFreeCamera = true;
    }

    public void FreeCamStop()
    {
        isFreeCamera = false;
    }

    public void RotateCamera(float rotationX, float rotationY)
    {
        if (isFreeCamera)
        {
            transform.RotateAround(camTarget.position, Vector3.up, rotationY);
            transform.RotateAround(camTarget.position, transform.right, rotationX);
        }
    }
}
