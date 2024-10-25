using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBulletScript : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private LineRenderer lineRenderer;
    //public Camera camera;
    //private float speed = 2.0f;
    //[SerializeField] private GameObject shotPosition; //”­ŽËˆÊ’u
    //[SerializeField] private Vector3 direction;    //•ûŒü
    //[SerializeField] private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //’e‚Ì“®‚«
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        lineRenderer.SetPosition(0, transform.position);

    }

    public void SetUp(Vector3 startPosition, Vector3 endPosition)
    {
        lineRenderer.SetPosition(0, startPosition);
        lineRenderer.SetPosition(1, new Vector3(0,1,100));
       
    }

    //public void Shot()
    //{
    //   //direction = shotPosition.transform.forward * speed * Time.deltaTime;
    //   //RaycastHit hit;
    //   //Ray ray = new Ray(shotPosition.transform.position,direction);
    //   //lineRenderer.SetPosition(0,shotPosition.transform.position);
    //   //
    //   //if (Physics.Raycast(ray, out hit))
    //   //{
    //   //    Debug.Log( hit.transform.name);
    //   //    lineRenderer.SetPosition(1, shotPosition.transform.position + direction);
    //   //}
    //}
}
