using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GunScript : MonoBehaviour
{
    [Header("弾撃つ関連")]
    [SerializeField] public GunBulletScript bullet;　　//弾
    [SerializeField] public Transform shotPosition; 　//発射位置
    [SerializeField] public  float range = 50.0f;      //レイの飛ぶ距離
    //Enemy enemy;
    [SerializeField] PlayerMove player;
    [SerializeField] public float shotInterval = 1.0f;
    public float lastShotTime = 0.0f;

    [Header("銃の回転")]
    [SerializeField] private float rotateSpeed = 50.0f;  //回転の速さ
    private bool isParent = false;       //ペアレントされたかどうか

    [Header("爆弾に当たったら")]
    [SerializeField] public Explosion explosion;
    //[SerializeField] public BomScript bom;
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


    //銃の回転-------------------------------------------------
    private void RotateGun()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
    public void SetParented(bool parented) { isParent = parented; }


    public virtual void Shot()
    {
        if (Time.time >= lastShotTime + shotInterval)
        {
            //弾の生成-------------------------------
            GunBulletScript obj = Instantiate(bullet, transform.position, Quaternion.identity);


            //レイの設定----------------------------
            Ray ray = new Ray(shotPosition.position, shotPosition.forward);
            RaycastHit hit;
            Vector3 hitPosition = Vector3.zero;

            //当たったら--------------------------------------------------
            if (Physics.Raycast(ray, out hit, range))
            {
                // 何かにヒットした場合の処理
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
                    //Destroy(hit.collider.gameObject);
                    hit.collider.gameObject.GetComponent<BomScript>().Explosion();
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
