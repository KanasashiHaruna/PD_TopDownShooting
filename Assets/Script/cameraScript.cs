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


    private Vector3 originalPosition;      //カメラの元の位置
    private float shakeDuration = 0.0f;  　//シェイクの持続時間
    private float shakeMagnitude = 0.0f;　 //シェイクの強度
    private float initialShakeDuration;    //初期のシェイクの持続時間
    private Vector3 shakeOffset;
    // Start is called before the first frame update
    void Start()
    {
        //カメラの元の位置を取得
        origin=(transform.position- player.transform.position);
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //カメラのシェイク------------------------------------------
        if (shakeDuration > 0.0f)
        {
            //持続時間が残っている時カメラの位置を変更
            float currentMagnitude = shakeMagnitude * (shakeDuration / initialShakeDuration);
            shakeOffset = Random.insideUnitSphere * currentMagnitude;
            transform.position = originalPosition + Random.insideUnitSphere * currentMagnitude;
            shakeDuration -= Time.deltaTime;
        }
        else
        {
            shakeOffset = Vector3.zero;
            transform.position = originalPosition;
        }

        //方向-------------------
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //
        if (Physics.Raycast(ray, out hit, 100.0f, floorLayerMask))
        {
            // ヒットした位置をターゲットポジション-------------
            Vector3　mousePosition= hit.point;

            //プレイヤーのポジション取得/計算------------------
            Vector3 playerPosition=player.transform.position;
            Vector3 distance = (mousePosition - playerPosition) * 0.5f;

            //最大距離を超えないように制限---------------------
            if (distance.magnitude > maxDistance)
            {
                distance = distance.normalized * maxDistance; 
            }

            //移動したカメラの位置------------------------------
            Vector3 moveCamera = playerPosition+origin + distance;

            transform.position = Vector3.Lerp(transform.position, moveCamera, 0.1f);
            originalPosition = transform.position;
        }
    }

    //カメラのシェイク-------------------------------------
    public void StartShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        initialShakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
