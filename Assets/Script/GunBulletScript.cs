using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;




public class GunBulletScript : MonoBehaviour
{
    private float speed = 10.0f;
    private LineRenderer lineRenderer;

    private Vector3 start;  //レイの開始
    private Vector3 end;    //レイの終了
    private Vector3 distance;      //レイの方向ベクトル
    private float totalDistance;   //レイの全移動距離
    private float traveledDistance;//現在までの移動距離
    private Vector3 direction;

    [SerializeField] Rigidbody rb;
    // Start is called before the first frame update
    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
    }

    private void Start()
    {
        //レイの開始と終了位置を取得
        start = lineRenderer.GetPosition(0);
        end = lineRenderer.GetPosition(1);

        //方向ベクトルを計算
        distance = (end - start).normalized;

        //全いどう距離を計算
        totalDistance = Vector3.Distance(start, end);
        traveledDistance = 0;

        direction = distance;

    }
    // Update is called once per frame
    void Update()
    {
        //弾の動き
        rb.velocity = direction * speed;

        //レイの位置---------------------------------
        //開始位置の計算更新
        start += distance * speed * Time.deltaTime;

        lineRenderer.SetPosition(0, start);

        //今回の移動距離加算
        traveledDistance += speed * Time.deltaTime;


        if (Vector3.Distance(start, end) < 0.1f)
        {
            Destroy(gameObject);
        }

    }

    public void SetUp(Vector3 startPosition, Vector3 endPosition)
    {
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, endPosition);
    }

    private void OnTriggerEnter(Collider collision)
    {
       if (collision.gameObject.CompareTag("Wall"))
       {
           Destroy(gameObject);
           //Destroy(lineRenderer);
       }
       
    }
}
