using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gure : MonoBehaviour
{
    private float speed = 10.0f;
    private Vector3 direction;
    [SerializeField] Rigidbody rb;
    [SerializeField] Explosion ex;
    private cameraScript camera;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = Camera.main.GetComponent<cameraScript>();
        //direction = gurelan.shotPosition.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        //’e‚Ì“®‚«----------------------------
        rb.velocity= direction * speed;
        rb.rotation = Quaternion.LookRotation(direction);
    }

    public void SetGure(Vector3 shotPosition, Quaternion shotRotation)
    {
        direction = shotRotation * Vector3.forward;
    }


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            camera.StartShake(0.5f, 0.5f);
            Destroy(this.gameObject);
            Explosion obj = Instantiate(ex, transform.position, Quaternion.identity);
        }

        if (collision.gameObject.CompareTag("Bom"))
        {
            camera.StartShake(0.5f, 0.5f);
            Destroy(this.gameObject);
            Explosion obj = Instantiate(ex, transform.position, Quaternion.identity);
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            camera.StartShake(0.5f, 0.5f);
            Destroy(this.gameObject);
            Explosion obj = Instantiate(ex, transform.position, Quaternion.identity);
        }
    }
}
