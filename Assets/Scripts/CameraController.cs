using Assets.Scripts.Conf.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform camTarget;
    public CameraConfig conf;

    private void Start()
    {
        transform.position = camTarget.transform.position;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, camTarget.position, conf.pLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, camTarget.rotation, conf.rLerp);
    }
}
