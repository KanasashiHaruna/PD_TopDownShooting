using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GunScript : MonoBehaviour
{
    [Header("�e���֘A")]
    [SerializeField] public GunBulletScript bullet;�@�@//�e
    [SerializeField] private Transform shotPosition; �@//���ˈʒu
    [SerializeField] public  float range = 50.0f;      //���C�̔�ԋ���


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
        //�e�̐���
        GunBulletScript obj = Instantiate(bullet, transform.position, Quaternion.identity);

       
        //���C�̐ݒ�
        Ray ray = new Ray(shotPosition.position, shotPosition.forward);
        //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        Vector3 hitPosition = Vector3.zero;

        //����������--------------------------------------------------
        if (Physics.Raycast(ray, out hit, range))
        {
            // �����Ƀq�b�g�����ꍇ�̏���
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
