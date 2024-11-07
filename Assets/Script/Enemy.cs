using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float Hp = 10.0f;

    [Header("�i�r���b�V���֘A")]
    [SerializeField] NavMeshAgent navmeshAgent = null;   //�i�r���b�V��
    [SerializeField] private Transform[] targets; //�^�[�Q�b�g�n�_(����)
    private int currentTargetIndex;               //���݂̃^�[�Q�b�g�̈ʒu��ێ�

    [SerializeField] PlayerMove player;           //�v���C���[
    [SerializeField] private float angle = 90.0f; //����̍L��

    [Header("�e�����֘A")]
    [SerializeField] EnemyGun gun;
    [SerializeField] private float time;
    [SerializeField] private float shotTime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        navmeshAgent=GetComponent<NavMeshAgent>();
        currentTargetIndex = 0;
        MoveToNextTarget();
    }

    // Update is called once per frame
    void Update()
    {

        if (Hp <= 0)
        {
            Destroy(gameObject);
            //Destroy(navmeshAgent);
            return;
        }
        //navmeshAgent.isStopped = false;

        Vector3 playerPosition=player.transform.position;
        Vector3 enemyPosition=transform.position;

        Vector3 direction=(playerPosition-enemyPosition).normalized;�@�@�@//���K��
        float dotDirection=Vector3.Dot(transform.forward,direction);      //����
        float theta=Mathf.Acos(dotDirection)*Mathf.Rad2Deg;�@�@�@�@�@�@�@ //�����Ńv���C���[�ƓG�̊Ԃ̊p�x���Ƃ�

        if (theta > angle) {
           // Debug.Log("����O");
           
        }
        else
        {
            //Debug.Log("�����");
            Ray ray=new Ray(transform.position, direction);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("�U������");

                    //�v���C���[�̕���������-------------------------------------------
                    float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, targetAngle, 0);
                    Fire();
                    navmeshAgent.isStopped = true;


                }
                else
                {
                    navmeshAgent.isStopped = false;
                }
            }
        }

        //�i�r���b�V���̈ړ�----------------------------------------------------
        if (!navmeshAgent.pathPending && navmeshAgent.remainingDistance < 0.5f)
        {
            MoveToNextTarget();
        }
          
    }

    void MoveToNextTarget()
    {
        if(navmeshAgent.isStopped) { return; }
        if (targets.Length == 0) return; 
        // ���̃^�[�Q�b�g�Ɉړ�
        navmeshAgent.destination = targets[currentTargetIndex].position; currentTargetIndex = (currentTargetIndex + 1) % targets.Length; 
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
