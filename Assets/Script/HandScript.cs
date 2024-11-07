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
        // �}�E�X�̈ʒu�����[���h���W�n�Ŏ擾
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        
        if (plane.Raycast(ray, out float distance)) {
            Vector3 mousePosition = ray.GetPoint(distance); 

            // �n���h����}�E�X�̈ʒu�ւ̕������v�Z
            Vector3 direction = mousePosition - transform.position;
            direction.y = 0;// �n���h�����������ɂ̂݉�]����悤�ɂ���

            // �p�x���v�Z���ĉ�]��K�p
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 10f); // �X���[�Y�ȉ�]�̂��߂̕��
        }
    }

    //�e���n���h�ɂ���
    public void SetGun(GunScript chilGun, Vector3 position, Quaternion rotation)
    {
        gun = chilGun;
        //gun.transform.SetParent(transform); 
        gun.transform.localPosition = position;
        gun.transform.localRotation = rotation;
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
