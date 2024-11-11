using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    [Header("プレイヤーの移動関連")]
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private Vector3 distance;
    [SerializeField] Rigidbody rb;
    [SerializeField] private float angleSpeed = 4.0f;


    [Header("プレイヤーが弾を撃つ関連")]
    [SerializeField] HandScript hand;                //ハンドのオブジェクト
    [SerializeField] GameObject handPosition;        //銃をくっつけるオブジェクト(ハンド)
    //[SerializeField] private float fireRate = 1.0f;  //射撃間隔
    //private float fireTime = 0f;

    // Start is called before the first frame update

    //プレイヤーの移動

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //方向-------------------
        Ray ray=Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //
        if(Physics.Raycast(ray, out hit, 100.0f))
        {
            Vector3 targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        
            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * angleSpeed);
        }
       
        //移動--------------------
        Move();

        //弾を撃つ----------------
        if (hand.isParent)
        {
            if (Input.GetMouseButton(0))
            {
                hand.Fire();
                //fireTime = Time.time + fireRate;
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
        //銃とプレイヤーに当たり判定---------------------
        if (collision.gameObject.CompareTag("Gun"))
        {
            //gunPositionに親子付けする
            collision.gameObject.transform.SetParent(handPosition.transform);

            
            GunScript gun;
            //銃オブジェクトから GunScript コンポーネントを取得
            if (collision.gameObject.TryGetComponent(out gun))
            {
                Vector3 gunPosition = new Vector3(0.05f, 0.1f, 0.1f); // ここで銃の位置を指定
                Quaternion gunRotation = Quaternion.Euler(0, 0, 0);
                //プレイヤーの手に銃をセット
                hand.SetGun(gun,gunPosition,gunRotation);
            }
            else
            {
                return;
            }

        }
    }
}
