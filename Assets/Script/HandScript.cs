using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    GunScript gun;
    //public bool isShot = false;
    public bool isParent{ get { return transform.childCount >= 1; } }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateHandTowardsMouse();
    }


    void RotateHandTowardsMouse()
    {
        // マウスの位置をワールド座標系で取得
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        
        if (plane.Raycast(ray, out float distance)) {
            Vector3 mousePosition = ray.GetPoint(distance); 

            // ハンドからマウスの位置への方向を計算
            Vector3 direction = mousePosition - transform.position;
            direction.y = 0;// ハンドが水平方向にのみ回転するようにする

            // 角度を計算して回転を適用
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f); // スムーズな回転のための補間
        }
    }

    //銃をハンドにつける
    public void SetGun(GunScript chilGun, Vector3 position, Quaternion rotation)
    {
        gun = chilGun;
        //gun.transform.SetParent(transform); 
        gun.transform.localPosition = position;
        gun.transform.localRotation = rotation;
    }

    //銃を撃つ
    public void Fire()
    {
        if(gun != null)
        {
            gun.Shot();
        }
        else
        {
            return;
        }
    }
}
