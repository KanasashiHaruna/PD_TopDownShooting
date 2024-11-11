using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomScript : MonoBehaviour
{
    [SerializeField] private Explosion explosion;
    private cameraScript camera;
    //public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.GetComponent<cameraScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void Explosion()
    {
        Instantiate(explosion,transform.position,Quaternion.identity);
        Destroy(this.gameObject);
       
    }
}
