using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : GunScript
{
    public override void Shot()
    {
        range = 5.0f;
        base.Shot();
    }
}
