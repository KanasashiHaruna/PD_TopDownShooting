using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [Header("透明度を変化")]
    [SerializeField] private MeshRenderer mesh;
    [SerializeField] private float lerpSpeed = 5.5f;

    private cameraScript camera;
    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        camera = Camera.main.GetComponent<cameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        ExplosionMove();
    }

    void OnTriggerEnter(Collider collision)
    {
        //爆弾と当たったら----------------------------
        if (collision.gameObject.CompareTag("Bom"))
        {
           
            Vector3 direction = collision.transform.position - transform.position;
            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction, out hit))
            {
                if (hit.collider.CompareTag("Bom"))
                {
                    camera.StartShake(0.5f, 0.5f);
                    Destroy(hit.collider.gameObject);
                    Instantiate(this.gameObject,hit.transform.position, Quaternion.identity);
                }
            }
        }

        //敵と当たったら------------------------------
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log("敵と当たったよー ");

            Vector3 direction=collision.transform.position - transform.position;
            RaycastHit hit;
            if(Physics.Raycast(transform.position,direction, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    camera.StartShake(0.5f, 0.5f);
                    hit.collider.gameObject.GetComponent<Enemy>().DecreaseHp(5.0f);
                    //Destroy(hit.collider.gameObject);
                    Instantiate(this.gameObject, hit.transform.position, Quaternion.identity);
                }
            }
        }
    }


        private void ExplosionMove()
    {
        float scaleSpeed = 12.0f; //大きくなるスピード
        transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
       
        Color color=mesh.material.color;
        color = Color.Lerp(color, new Color(color.r, color.g, color.b, 0), lerpSpeed * Time.deltaTime);
        mesh.material.color = color;

        if (transform.localScale.x >= 6.0f)
        {
            Destroy(gameObject);
        }
    }
}
