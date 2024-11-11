using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField] public float Hp = 5.0f;

    [Header("ナビメッシュ関連")]
    [SerializeField] NavMeshAgent navmeshAgent = null;   //ナビメッシュ
    [SerializeField] private Transform[] targets; //ターゲット地点(複数)
    private int currentTargetIndex;               //現在のターゲットの位置を保持
    [SerializeField] PlayerMove player;           //プレイヤー
    [SerializeField] private float angle = 90.0f; //視野の広さ

    [Header("弾を撃つ関連")]
    [SerializeField] EnemyGun gun;
    [SerializeField] private float time;
    [SerializeField] private float shotTime = 0.3f;

    [Header("プレイヤーを探す挙動")]
    [SerializeField] private float rotateAngle = 45.0f;    //探すときどれくらい振り向くか
    [SerializeField] private float serchTime = 8.0f;       //探す挙動を何秒するか
    [SerializeField] private float serchCount = 0.0f;
    [SerializeField] private float rotateSec = 2.0f;       //何秒書けて回転するか
    [SerializeField] private float rotateCount = 0.0f;     //回転する秒数
    //[SerializeField] private bool isSerching = false;    //探すかどうか
    [SerializeField] private bool isLeftRotate = true;   //左回転するかどうか
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
        #region Hp管理
        //敵のHP管理-----------------------------
        //if (Hp <= 0)
        //{
        //    Destroy(gameObject);
        //    return;
        //}
        #endregion

        #region 敵の視野
        //敵の視野の計算-----------------------------------
        Vector3 playerPosition = player.transform.position;
        Vector3 enemyPosition = transform.position;

        Vector3 direction = (playerPosition - enemyPosition).normalized;　　　//正規化
        float dotDirection = Vector3.Dot(transform.forward, direction);      //内積
        float theta = Mathf.Acos(dotDirection) * Mathf.Rad2Deg;        //ここでプレイヤーと敵の間の角度をとる
        #endregion

        //--------------------------------------------------
        #region 視野内。見つけてるかも
        if (theta < angle)
        {
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    //プレイヤーの方向を向く-------------------------------------------
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


    //探す挙動--------------------------------------------
     void Serch()
     {

         if (isLeftRotate)//左回転する
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
             //右回転して
             transform.Rotate(0.0f, (rotateAngle / rotateSec * Time.deltaTime), 0.0f);
             rotateCount += Time.deltaTime;
         }
         if (rotateCount >= rotateSec)
         {
             isLeftRotate = true;
             rotateCount = 0.0f;
         }
     }

    //ナビメッシュの移動---------------------------------
    void MoveToNextTarget()
    {
        if (navmeshAgent.isStopped) { return; }
    if (targets.Length == 0) { return; }
        // 次のターゲットに移動
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

         //弾を撃つ-----------------------------------------------------------
         time += Time.deltaTime;
         if (time >= shotTime)
         {
           gun.Shot();
           time = time - shotTime;

         }
     }
    }
