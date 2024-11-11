using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    GunScript gun;
    public LayerMask floorLayerMask;
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
     
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        
       if (Physics.Raycast(ray, out hit, 100.0f, floorLayerMask))
       {
            float h = transform.position.y;  //çÇÇ≥
            Vector3 direction = ray.direction * -1;
            float theta = Mathf.Acos(Vector3.Dot(direction, Vector3.up));

            float s = h/Mathf.Cos(theta);
            Vector3 a = (hit.point - (direction * s));

            transform.LookAt(a);

       }

    }

    //èeÇÉnÉìÉhÇ…Ç¬ÇØÇÈ
    public void SetGun(GunScript chilGun, Vector3 position, Quaternion rotation)
    {
        if (gun != null)
        {
            Destroy(gun.gameObject);
        }

        gun = chilGun;
        gun.transform.localPosition = position;
        gun.transform.localRotation = rotation;
        gun.SetParented(true);
    }

    //èeÇåÇÇ¬
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
