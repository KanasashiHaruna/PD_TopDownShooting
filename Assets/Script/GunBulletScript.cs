using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;




public class GunBulletScript : MonoBehaviour
{
    private float speed = 10.0f;
    private LineRenderer lineRenderer;

    private Vector3 start;  //���C�̊J�n
    private Vector3 end;    //���C�̏I��
    private Vector3 distance;      //���C�̕����x�N�g��
    private float totalDistance;   //���C�̑S�ړ�����
    private float traveledDistance;//���݂܂ł̈ړ�����
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
        //���C�̊J�n�ƏI���ʒu���擾
        start = lineRenderer.GetPosition(0);
        end = lineRenderer.GetPosition(1);

        //�����x�N�g�����v�Z
        distance = (end - start).normalized;

        //�S���ǂ��������v�Z
        totalDistance = Vector3.Distance(start, end);
        traveledDistance = 0;

        direction = distance;

    }
    // Update is called once per frame
    void Update()
    {
        //�e�̓���
        rb.velocity = direction * speed;

        //���C�̈ʒu---------------------------------
        //�J�n�ʒu�̌v�Z�X�V
        start += distance * speed * Time.deltaTime;

        lineRenderer.SetPosition(0, start);

        //����̈ړ��������Z
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
