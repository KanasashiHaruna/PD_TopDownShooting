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
        


    }

   
    //�e���n���h�ɂ���
    public void SetGun(GunScript chilGun)
    {
        gun = chilGun;
    }

    //�e������
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
