using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform basePlayer;
    public float speed = 1, boost = 20, sens = 50;
    public int hp = 5;

    void Start()
    {
        Respawn();    
    }

    void Update()
    {
        Rotate();
        Move();
    }

    void Rotate()
    {
        float xAxiox = 0;
        float yAxiox = 0;
        float zAxiox = 0;

        if (Input.GetKey(KeyCode.A)) zAxiox = 1;
        if (Input.GetKey(KeyCode.D)) zAxiox = -1;
        if (Input.GetKey(KeyCode.W)) xAxiox = 1;
        if (Input.GetKey(KeyCode.S)) xAxiox = -1;
        if (Input.GetKey(KeyCode.Q)) yAxiox = -1;
        if (Input.GetKey(KeyCode.E)) yAxiox = 1;

        Vector3 allAxios = new Vector3(xAxiox, yAxiox, zAxiox) * sens * Time.deltaTime;
        Quaternion newOrentation = Quaternion.Euler(allAxios);
        transform.rotation *= newOrentation;
    }

    void Move()
    {
        float currSpeed = speed;
        if (Input.GetKey(KeyCode.Space)) currSpeed *= boost;
        transform.Translate(Vector3.forward * currSpeed * Time.deltaTime);
    }

    public void Shoot()
    {
        Debug.Log(true);
    }

    void Respawn()
    {
        Vector3 randomPos = Random.onUnitSphere * 100;
        transform.position = randomPos;
        transform.LookAt(basePlayer);
    }

    private void OnTriggerEnter(Collider other)
    {
        Bullet bulletScript = other.GetComponent<Bullet>();
        if (bulletScript != null) return;
        hp--;
        Respawn();
    }
}