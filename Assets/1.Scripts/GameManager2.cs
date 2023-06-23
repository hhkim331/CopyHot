using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager2 : MonoBehaviour
{
    public static GameManager2 Instance;

    private void Awake()
    {
        Instance = this;
    }

    readonly string chat1 = "코어를";
    readonly string chat2 = "해킹중입니다";
    string curChat = "";

    public GameObject gameMassage;
    public TextMeshProUGUI messageText;
    bool gamestart = false;

    public TotalEnemySpawnData totalEnemySpawnData;
    public GameObject slow;
    float gametime;
    public GameObject playercam;
    public GameObject maincamera;

    //해킹
    //슬라이더

    private void Start()
    {
        //화면 중앙에 커서 고정
        Cursor.lockState = CursorLockMode.Locked;
        //커서 보이지않음
        Cursor.visible = false;
        gameMassage.SetActive(true);
        messageText.text = " ";
        curChat = " ";
        Time.timeScale = 0;
        gamestart = true;
        gametime = 0f;
        playercam.GetComponent<CAM>().enabled = false;
        maincamera.GetComponent<CAM>().enabled = false;
    }

    private void Update()
    {
        if (gamestart == true && !slow.activeSelf)
        {
            gametime += Time.unscaledDeltaTime;
            if (gametime > 1.5f)
            {
                gameMassage.SetActive(false);
                slow.SetActive(true);
                playercam.GetComponent<CAM>().enabled = true;
                maincamera.GetComponent<CAM>().enabled = true;
                gametime = 0;
            }
            else if (gametime > 1.2f)
            {
                if (curChat != chat2)
                {
                    curChat = chat2;
                    messageText.text = chat2;
                    SoundManager.Instance.PlaySFX("message");
                }
                gameMassage.transform.localScale = Vector3.one * (1.5f - (gametime - 1.2f) / 0.6f);
            }
            else if (gametime > 0.9f)
            {
                if (curChat != chat1)
                {
                    curChat = chat1;
                    messageText.text = chat1;
                    SoundManager.Instance.PlaySFX("message");
                }
                gameMassage.transform.localScale = Vector3.one * (1.5f - (gametime - 0.9f) / 0.6f);
            }
        }
        else
        {
            //코어 해킹중
            gametime += Time.deltaTime;
        }
    }
}
