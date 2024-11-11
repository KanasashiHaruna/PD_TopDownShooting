using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MachineGunBullet : GunScript
{
    
    public override void Shot()
    {
        shotInterval = 0.3f;
        base.Shot();

    }
}



