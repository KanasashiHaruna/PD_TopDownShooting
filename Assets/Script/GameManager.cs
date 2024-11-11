using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("シーン切り替え")]
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] float fadeSpeed = 0.5f;
    [SerializeField] Text gameClear;
    [SerializeField] Text space;
    [SerializeField] private bool isReset = false;
    [SerializeField] PlayerMove player;
    //public CameraShake/ cameraShake;


    // Start is called beforf;he first frame update
    void Start()
    {
        
    }

    
    // Update is called once per frame
    void Update()
    {
       if (player.transform.position.z >= 153.0f)
       {
           FeadAction();

            Vector3 newPosition = player.transform.position;
            newPosition.z += 3.0f * Time.deltaTime;
            player.transform.position = newPosition;
        }
        if(isReset && Input.GetKeyDown(KeyCode.Space))
        {
            ResetScene();
        }
    }

    void FeadAction()
    {
        canvasGroup.alpha += fadeSpeed * Time.deltaTime;
        if (canvasGroup.alpha >= 1.0f)
        {
            canvasGroup.alpha = 1.0f;
            gameClear.gameObject.SetActive(true);
            space.gameObject.SetActive(true);
            isReset = true;
        }
    }

    void ResetScene()
    {
        SceneManager.LoadScene("reveluUp", LoadSceneMode.Single); // 現在のシーンを再読み込み
    }

    
}
