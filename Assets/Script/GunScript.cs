using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GunScript : MonoBehaviour
{
    [Header("’eŒ‚‚ÂŠÖ˜A")]
    [SerializeField] public GunBulletScript bullet;@@//’e
    [SerializeField] public Transform shotPosition; @//”­ËˆÊ’u
    [SerializeField] public  float range = 50.0f;      //ƒŒƒC‚Ì”ò‚Ô‹——£
    //Enemy enemy;
    [SerializeField] PlayerMove player;
    [SerializeField] public float shotInterval = 1.0f;
    public float lastShotTime = 0.0f;

    [Header("e‚Ì‰ñ“]")]
    [SerializeField] private float rotateSpeed = 50.0f;  //‰ñ“]‚Ì‘¬‚³
    private bool isParent = false;       //ƒyƒAƒŒƒ“ƒg‚³‚ê‚½‚©‚Ç‚¤‚©

    [Header("”š’e‚É“–‚½‚Á‚½‚ç")]
    [SerializeField] public Explosion explosion;
    private cameraScript camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.GetComponent<cameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isParent == false)
        {
            RotateGun();
        }
    }


    //e‚Ì‰ñ“]-------------------------------------------------
    private void RotateGun()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
    public void SetParented(bool parented) { isParent = parented; }


    public virtual void Shot()
    {
        if (Time.time >= lastShotTime + shotInterval)
        {
            //’e‚Ì¶¬-------------------------------
            GunBulletScript obj = Instantiate(bullet, transform.position, Quaternion.identity);


            //ƒŒƒC‚Ìİ’è----------------------------
            Ray ray = new Ray(shotPosition.position, shotPosition.forward);
            RaycastHit hit;
            Vector3 hitPosition = Vector3.zero;

            //“–‚½‚Á‚½‚ç--------------------------------------------------
            if (Physics.Raycast(ray, out hit, range))
            {
                // ‰½‚©‚Éƒqƒbƒg‚µ‚½ê‡‚Ìˆ—
                Debug.Log(hit.transform.name);
                hitPosition = hit.point;

                if (hit.collider.CompareTag("Switch"))
                {
                    Destroy(hit.collider.gameObject);
                }

                if (hit.collider.CompareTag("Enemy"))
                {
                    hit.collider.gameObject.GetComponent<Enemy>().DecreaseHp(1.0f);
                    
                }

                if (hit.collider.CompareTag("Bom"))
                {
                    Destroy(hit.collider.gameObject);
                    camera.StartShake(0.5f, 0.5f);
                    Explosion objEx=Instantiate(explosion, hit.collider.transform.position, Quaternion.identity);  

                }
            }
            else
            {
                hitPosition = shotPosition.position + shotPosition.forward * range;
            }

            obj.SetUp(shotPosition.position, hitPosition);
            lastShotTime = Time.time;
        }
    }

}
