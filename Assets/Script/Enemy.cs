using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float Hp = 10.0f;

    [Header("ナビメッシュ関連")]
    [SerializeField] NavMeshAgent navmeshAgent = null;   //ナビメッシュ
    [SerializeField] private Transform[] targets; //ターゲット地点(複数)
    private int currentTargetIndex;               //現在のターゲットの位置を保持

    [SerializeField] PlayerMove player;           //プレイヤー
    [SerializeField] private float angle = 90.0f; //視野の広さ

    [Header("弾を撃つ関連")]
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

        Vector3 direction=(playerPosition-enemyPosition).normalized;　　　//正規化
        float dotDirection=Vector3.Dot(transform.forward,direction);      //内積
        float theta=Mathf.Acos(dotDirection)*Mathf.Rad2Deg;　　　　　　　 //ここでプレイヤーと敵の間の角度をとる

        if (theta > angle) {
           // Debug.Log("視野外");
           
        }
        else
        {
            //Debug.Log("視野内");
            Ray ray=new Ray(transform.position, direction);
            RaycastHit hit;
            if(Physics.Raycast(ray,out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    Debug.Log("攻撃する");

                    //プレイヤーの方向を向く-------------------------------------------
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

        //ナビメッシュの移動----------------------------------------------------
        if (!navmeshAgent.pathPending && navmeshAgent.remainingDistance < 0.5f)
        {
            MoveToNextTarget();
        }
          
    }

    void MoveToNextTarget()
    {
        if(navmeshAgent.isStopped) { return; }
        if (targets.Length == 0) return; 
        // 次のターゲットに移動
        navmeshAgent.destination = targets[currentTargetIndex].position; currentTargetIndex = (currentTargetIndex + 1) % targets.Length; 
    }

    void Fire()
    {
        //弾を撃つ-----------------------------------------------------------
        time += Time.deltaTime;
        if (time >= shotTime)
        {
            gun.Shot();
            time = time - shotTime;
        }
    }
}
