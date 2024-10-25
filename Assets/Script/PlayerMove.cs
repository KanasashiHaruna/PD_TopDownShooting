using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class PlayerMove : MonoBehaviour
{

    [Header("プレイヤーの移動関連")]
    [SerializeField] private float moveSpeed = 3.0f;
    [SerializeField] private Vector3 distance;
    [SerializeField] Rigidbody rb;

    [Header("プレイヤーが弾を撃つ関連")]
    [SerializeField] HandScript hand;          //ハンドのオブジェクト
    [SerializeField] GameObject handPosition;  //銃をくっつけるオブジェクト(ハンド)
    [SerializeField] private float fireRate = 1.0f;  //射撃間隔
    private float fireTime = 0f;

    // Start is called before the first frame update

    //プレイヤーの移動

    void Start()
    {
        //rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        //移動--------------------
        Move();

        //弾を撃つ----------------
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
        //銃とプレイヤーに当たり判定---------------------
        if (collision.gameObject.CompareTag("Gun"))
        {
            //gunPositionに親子付けする
            collision.gameObject.transform.SetParent(handPosition.transform);

            
            GunScript gun;
            //銃オブジェクトから GunScript コンポーネントを取得
            if (collision.gameObject.TryGetComponent(out gun))
            {
                //プレイヤーの手に銃をセット
                hand.SetGun(gun);
            }
            else
            {
                return;
            }

        }
    }
}
