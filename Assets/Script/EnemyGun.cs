using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : GunScript
{
    [SerializeField] private int shotsFired = 0; //�������e�̐�
    [SerializeField] private bool isCooldown = false; //�N�[���^�C�������ǂ���
    private int maxShotsBeforeCooldown = 6; //�N�[���^�C���܂ł̍ő�e��
    private float cooldownTime = 2.0f;
    private float cooldownTimer = 0.0f; // �N�[���^�C���𑪂�^�C�}�[

    //---------------------------------------------------
    void Update() {
        if (isCooldown) {
            cooldownTimer += Time.deltaTime;
            if (cooldownTimer >= cooldownTime) {
                isCooldown = false; cooldownTimer = 0.0f;
                shotsFired = 0; 
                Debug.Log("�N�[���^�C���I��"); 
            } 
        }
    }

    //----------------------------------------------------
    public override void Shot()
    {
        if (isCooldown) {
            Debug.Log("�N�[���^�C�����ł��B"); 
            return; 
        }
        shotInterval = 0.3f;
        //range = 5.0f;
        base.Shot();

        shotsFired++;
        if (shotsFired >= maxShotsBeforeCooldown) {
            isCooldown = true;
            Debug.Log("�N�[���^�C���J�n"); 
        }
    }
}
