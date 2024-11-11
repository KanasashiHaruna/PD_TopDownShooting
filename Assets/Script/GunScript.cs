using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GunScript : MonoBehaviour
{
    [Header("�e���֘A")]
    [SerializeField] public GunBulletScript bullet;�@�@//�e
    [SerializeField] public Transform shotPosition; �@//���ˈʒu
    [SerializeField] public  float range = 50.0f;      //���C�̔�ԋ���
    //Enemy enemy;
    [SerializeField] PlayerMove player;
    [SerializeField] public float shotInterval = 1.0f;
    public float lastShotTime = 0.0f;

    [Header("�e�̉�]")]
    [SerializeField] private float rotateSpeed = 50.0f;  //��]�̑���
    private bool isParent = false;       //�y�A�����g���ꂽ���ǂ���

    [Header("���e�ɓ���������")]
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


    //�e�̉�]-------------------------------------------------
    private void RotateGun()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
    public void SetParented(bool parented) { isParent = parented; }


    public virtual void Shot()
    {
        if (Time.time >= lastShotTime + shotInterval)
        {
            //�e�̐���-------------------------------
            GunBulletScript obj = Instantiate(bullet, transform.position, Quaternion.identity);


            //���C�̐ݒ�----------------------------
            Ray ray = new Ray(shotPosition.position, shotPosition.forward);
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
