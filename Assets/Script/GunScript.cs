using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [Header("弾撃つ関連")]
    [SerializeField] public GunBulletScript bullet;　　//弾
    [SerializeField] private Transform shotPosition; 　//発射位置
    [SerializeField] private float range = 1000.0f;      //レイの飛ぶ距離
 
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shot()
    {
        //弾の生成
        GunBulletScript obj = Instantiate(bullet, transform.position, Quaternion.identity);
        
        Ray ray = new Ray(shotPosition.position, shotPosition.forward);
        RaycastHit hit;

        Vector3 hitPosition;
      

        if (Physics.Raycast(ray, out hit, range))
        {
            // 何かにヒットした場合の処理
            Debug.Log(hit.transform.name);
            // LineRendererの終点をヒット地点に設定
           
            hitPosition = hit.point;
        }
        else
        {
            hitPosition = shotPosition.position + shotPosition.forward * range;
        }
        //hitPosition = new Vector3(0, 1, 100);   
        obj.SetUp(shotPosition.position, hitPosition);
    }
}
