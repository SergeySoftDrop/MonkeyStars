using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SimpleTestMove : MonoBehaviour
{
    [SerializeField] float speed;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
