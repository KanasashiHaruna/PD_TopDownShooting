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

        //����-------------------
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        //
        if (Physics.Raycast(ray, out hit, 100.0f, floorLayerMask))
        {

            // �q�b�g�����ʒu���^�[�Q�b�g�|�W�V����
             Vector3�@mousePosition= hit.point;

            //�v���C���[�̃|�W�V�����擾
            Vector3 playerPosition=player.transform.position;

            Vector3 distance = (mousePosition - playerPosition) * 0.5f;

            if (distance.magnitude > maxDistance)
            {
                distance = distance.normalized * maxDistance; //�ő勗���𒴂��Ȃ��悤�ɐ���
            }

            //�ړ������J�����̈ʒu
            Vector3 moveCamera = playerPosition+origin + distance;


            transform.position = Vector3.Lerp(transform.position, moveCamera, 0.1f);
        }
    }
}
