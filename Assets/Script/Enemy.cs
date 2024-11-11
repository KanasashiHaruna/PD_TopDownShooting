using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float Hp = 5.0f;

    [Header("�i�r���b�V���֘A")]
    [SerializeField] NavMeshAgent navmeshAgent = null;   //�i�r���b�V��
    [SerializeField] private Transform[] targets; //�^�[�Q�b�g�n�_(����)
    private int currentTargetIndex;               //���݂̃^�[�Q�b�g�̈ʒu��ێ�
    [SerializeField] PlayerMove player;           //�v���C���[
    [SerializeField] private float angle = 90.0f; //����̍L��

    [Header("�e�����֘A")]
    [SerializeField] EnemyGun gun;
    [SerializeField] private float time;
    [SerializeField] private float shotTime = 0.3f;

    [Header("�v���C���[��T������")]
    [SerializeField] private float rotateAngle = 45.0f;    //�T���Ƃ��ǂꂭ�炢�U�������
    [SerializeField] private float serchTime = 8.0f;       //�T�����������b���邩
    [SerializeField] private float serchCount = 0.0f;
    [SerializeField] private float rotateSec = 2.0f;       //���b�����ĉ�]���邩
    [SerializeField] private float rotateCount = 0.0f;     //��]����b��
    //[SerializeField] private bool isSerching = false;    //�T�����ǂ���
    [SerializeField] private bool isLeftRotate = true;   //����]���邩�ǂ���
    //[SerializeField] private bool isFind = false;

    // Start is called before the first frame update
    void Start()
    {
        navmeshAgent = GetComponent<NavMeshAgent>();
        currentTargetIndex = 0;
        MoveToNextTarget();

    }

    // Update is called once per frame
    void Update()
    {
        #region Hp�Ǘ�
        //�G��HP�Ǘ�-----------------------------
        //if (Hp <= 0)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        #endregion

        #region �G�̎���
        //�G�̎���̌v�Z-----------------------------------
        Vector3 playerPosition = player.transform.position;
        Vector3 enemyPosition = transform.position;

        Vector3 direction = (playerPosition - enemyPosition).normalized;�@�@�@//���K��
        float dotDirection = Vector3.Dot(transform.forward, direction);      //����
        float theta = Mathf.Acos(dotDirection) * Mathf.Rad2Deg;        //�����Ńv���C���[�ƓG�̊Ԃ̊p�x���Ƃ�
        #endregion

        //--------------------------------------------------
        #region ������B�����Ă邩��
        if (theta < angle)
        {
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    //�v���C���[�̕���������-------------------------------------------
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, targetAngle, 0);

                    Fire();
                    navmeshAgent.isStopped = true;
                    serchCount = serchTime;

                }
            }

        }
        #endregion

        if (serchCount > 0)
        {
            Serch();
            serchCount -= Time.deltaTime;
        }
        else
        {
            navmeshAgent.isStopped = false;
        }

        if (!navmeshAgent.pathPending && navmeshAgent.remainingDistance < 0.5f)
        {
            MoveToNextTarget();
        }

    }


    //�T������--------------------------------------------
     void Serch()
     {

         if (isLeftRotate)//����]����
         {
             float r = -(rotateAngle / rotateSec * Time.deltaTime);
             transform.Rotate(0.0f, r, 0.0f);
             rotateCount += Time.deltaTime;
         }
         if (rotateCount >= rotateSec)
         {
             isLeftRotate = false;
             rotateCount = 0.0f;
         }

         //--------------------------------------------------
         if (isLeftRotate == false)
         {
             //�E��]����
             transform.Rotate(0.0f, (rotateAngle / rotateSec * Time.deltaTime), 0.0f);
             rotateCount += Time.deltaTime;
         }
         if (rotateCount >= rotateSec)
         {
             isLeftRotate = true;
             rotateCount = 0.0f;
         }
     }

    //�i�r���b�V���̈ړ�---------------------------------
    void MoveToNextTarget()
    {
        if (navmeshAgent.isStopped) { return; }
    if (targets.Length == 0) { return; }
        // ���̃^�[�Q�b�g�Ɉړ�
        navmeshAgent.destination = targets[currentTargetIndex].position; 
    currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
    }

    public void DecreaseHp(float amount)
    {
        Hp -= amount; if (Hp <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }

        void Fire()
     {

         //�e������-----------------------------------------------------------
         time += Time.deltaTime;
         if (time >= shotTime)
         {
           gun.Shot();
           time = time - shotTime;

         }
     }
    }
