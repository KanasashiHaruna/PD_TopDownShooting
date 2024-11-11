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


    private Vector3 originalPosition;      //�J�����̌��̈ʒu
    private float shakeDuration = 0.0f;  �@//�V�F�C�N�̎�������
    private float shakeMagnitude = 0.0f;�@ //�V�F�C�N�̋��x
    private float initialShakeDuration;    //�����̃V�F�C�N�̎�������
    private Vector3 shakeOffset;
    // Start is called before the first frame update
    void Start()
    {
        //�J�����̌��̈ʒu���擾
        origin=(transform.position- player.transform.position);
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //�J�����̃V�F�C�N------------------------------------------
        if (shakeDuration > 0.0f)
        {
            //�������Ԃ��c���Ă��鎞�J�����̈ʒu��ύX
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

        //����-------------------
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //
        if (Physics.Raycast(ray, out hit, 100.0f, floorLayerMask))
        {
            // �q�b�g�����ʒu���^�[�Q�b�g�|�W�V����-------------
            Vector3�@mousePosition= hit.point;

            //�v���C���[�̃|�W�V�����擾/�v�Z------------------
            Vector3 playerPosition=player.transform.position;
            Vector3 distance = (mousePosition - playerPosition) * 0.5f;

            //�ő勗���𒴂��Ȃ��悤�ɐ���---------------------
            if (distance.magnitude > maxDistance)
            {
                distance = distance.normalized * maxDistance; 
            }

            //�ړ������J�����̈ʒu------------------------------
            Vector3 moveCamera = playerPosition+origin + distance;

            transform.position = Vector3.Lerp(transform.position, moveCamera, 0.1f);
            originalPosition = transform.position;
        }
    }

    //�J�����̃V�F�C�N-------------------------------------
    public void StartShake(float duration, float magnitude)
    {
        shakeDuration = duration;
        initialShakeDuration = duration;
        shakeMagnitude = magnitude;
    }
}
