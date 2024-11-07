using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GunScript : MonoBehaviour
{
    [Header("弾撃つ関連")]
    [SerializeField] public GunBulletScript bullet;　　//弾
    [SerializeField] private Transform shotPosition; 　//発射位置
    [SerializeField] public  float range = 50.0f;      //レイの飛ぶ距離


    [SerializeField] Enemy enemy;
    [SerializeField] PlayerMove player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void Shot()
    {
        //弾の生成
        GunBulletScript obj = Instantiate(bullet, transform.position, Quaternion.identity);

       
        //レイの設定
        Ray ray = new Ray(shotPosition.position, shotPosition.forward);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
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
                enemy.Hp -= 1.0f;
            }

        }
        else
        {

            hitPosition = shotPosition.position + shotPosition.forward * range;
        }
        
        obj.SetUp(shotPosition.position, hitPosition);
    }

}
