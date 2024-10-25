using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class PlayerMove : MonoBehaviour
{

    [Header("�v���C���[�̈ړ��֘A")]
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private Vector3 distance;
    [SerializeField] Rigidbody rb;

    [Header("�v���C���[���e�����֘A")]
    [SerializeField] HandScript hand;          //�n���h�̃I�u�W�F�N�g
    [SerializeField] GameObject handPosition;  //�e����������I�u�W�F�N�g(�n���h)
    [SerializeField] private float fireRate = 1.0f;  //�ˌ��Ԋu
    private float fireTime = 0f;

    // Start is called before the first frame update

    //�v���C���[�̈ړ�

    void Start()
    {
        //rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //�ړ�--------------------
        Move();

        //�e������----------------
        if (hand.isParent)
        {
            if (Input.GetMouseButton(0) && Time.time>=fireTime)
            {
                hand.Fire();
                fireTime = Time.time + fireRate;
            }
        }
        
    }

    
    
    private void Move()
    {
        distance = Vector3.zero;

        if (Input.GetKey(KeyCode.W))
        {
            distance = Vector3.forward;
        }
        
        if (Input.GetKey(KeyCode.S))
        {
            distance = Vector3.back;
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            distance = Vector3.right;
        }
        
        if (Input.GetKey(KeyCode.A))
        {
            distance = Vector3.left;
        }
        //Input.GetAxis("Horizontal");
        //Input.GetAxis("Vertical");

        rb.velocity = distance * moveSpeed;
    }

    private void OnTriggerEnter(Collider collision)
    {
        //�e�ƃv���C���[�ɓ����蔻��---------------------
        if (collision.gameObject.CompareTag("Gun"))
        {
            //gunPosition�ɐe�q�t������
            collision.gameObject.transform.SetParent(handPosition.transform);

            
            GunScript gun;
            //�e�I�u�W�F�N�g���� GunScript �R���|�[�l���g���擾
            if (collision.gameObject.TryGetComponent(out gun))
            {
                //�v���C���[�̎�ɏe���Z�b�g
                hand.SetGun(gun);
            }
            else
            {
                return;
            }

        }
    }
}
