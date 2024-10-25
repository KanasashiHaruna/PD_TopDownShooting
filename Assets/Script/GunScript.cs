using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [Header("�e���֘A")]
    [SerializeField] public GunBulletScript bullet;�@�@//�e
    [SerializeField] private Transform shotPosition; �@//���ˈʒu
    [SerializeField] private float range = 1000.0f;      //���C�̔�ԋ���
 
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
        //�e�̐���
        GunBulletScript obj = Instantiate(bullet, transform.position, Quaternion.identity);
        
        Ray ray = new Ray(shotPosition.position, shotPosition.forward);
        RaycastHit hit;

        Vector3 hitPosition;
      

        if (Physics.Raycast(ray, out hit, range))
        {
            // �����Ƀq�b�g�����ꍇ�̏���
            Debug.Log(hit.transform.name);
            // LineRenderer�̏I�_���q�b�g�n�_�ɐݒ�
           
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
