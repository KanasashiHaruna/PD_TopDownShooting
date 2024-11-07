using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    [SerializeField] private float maxDistance = 1.0f;
    private Vector3 origin;
    public LayerMask floorLayerMask;
    public PlayerMove player;
    // Start is called before the first frame update
    void Start()
    {
        origin=(transform.position- player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {

        //方向-------------------
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //
        if (Physics.Raycast(ray, out hit, 100.0f, floorLayerMask))
        {

            // ヒットした位置をターゲットポジション
             Vector3　mousePosition= hit.point;

            //プレイヤーのポジション取得
            Vector3 playerPosition=player.transform.position;

            Vector3 distance = (mousePosition - playerPosition) * 0.5f;

            if (distance.magnitude > maxDistance)
            {
                distance = distance.normalized * maxDistance; //最大距離を超えないように制限
            }

            //移動したカメラの位置
            Vector3 moveCamera = playerPosition+origin + distance;


            transform.position = Vector3.Lerp(transform.position, moveCamera, 0.1f);
        }
    }
}
