using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class gimmickWall : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 2.0f;
    public List<GameObject> buttun;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bool allNull = true;
        for(int i=0; i<buttun.Count; i++)
        {
            if (buttun[i] != null)
            {
                allNull = false;
                break;
            }
            
        }

        if (allNull) {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime); 
        }

        if (transform.position.y <= -4.0f) { Destroy(gameObject); }

    }

}
