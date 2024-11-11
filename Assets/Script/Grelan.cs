using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grelan : GunScript
{
    [SerializeField] public Gure gure;
    [SerializeField] public float gureInterval = 1.5f;
    public float lastGureTime = 0.0f;
   

    public override void Shot()
    {
        shotInterval = 0.3f;
        if (Time.time >= lastGureTime + gureInterval)
        {
            Gure g=Instantiate(gure,transform.position,Quaternion.identity);
            lastGureTime= Time.time;

            g.SetGure(shotPosition.transform.position,shotPosition.transform.rotation);
        }
        base.Shot();
        

    }
}
