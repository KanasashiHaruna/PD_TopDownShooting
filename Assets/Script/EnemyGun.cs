using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : GunScript
{
    [SerializeField] private int shotsFired = 0; //撃った弾の数
    [SerializeField] private bool isCooldown = false; //クールタイム中かどうか
    private int maxShotsBeforeCooldown = 6; //クールタイムまでの最大弾数
    private float cooldownTime = 2.0f;
    private float cooldownTimer = 0.0f; // クールタイムを測るタイマー

    //---------------------------------------------------
    void Update() {
        if (isCooldown) {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownTime) {
                isCooldown = false; cooldownTimer = 0.0f;
                shotsFired = 0; 
                Debug.Log("クールタイム終了"); 
            } 
        }
    }

    //----------------------------------------------------
    public override void Shot()
    {
        if (isCooldown) {
            Debug.Log("クールタイム中です。"); 
            return; 
        }
        shotInterval = 0.3f;
        //range = 5.0f;
        base.Shot();

        shotsFired++;
        if (shotsFired >= maxShotsBeforeCooldown) {
            isCooldown = true;
            Debug.Log("クールタイム開始"); 
        }
    }
}
