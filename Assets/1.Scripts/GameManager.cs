using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    readonly string chat1 = "적을";
    readonly string chat2 = "모두";
    readonly string chat3 = "죽여라";

    public int stage;

    public GameObject gameStartMassage;
    public TextMeshProUGUI gameStartText;
    bool gamestart = false;

    public TotalEnemySpawnData totalEnemySpawnData;
    public GameObject slow;
    float gametime;
    public GameObject playercam;
    public GameObject maincamera;

    private void Start()
    {
        //화면 중앙에 커서 고정
        Cursor.lockState = CursorLockMode.Locked;
        //커서 보이지않음
        Cursor.visible = false;
        gameStartMassage.SetActive(true);
        gameStartText.text = "";
        Time.timeScale = 0;
        gamestart = true;
        gametime = 0f;
        playercam.GetComponent<CAM>().enabled = false;
        maincamera.GetComponent<CAM>().enabled = false;
    }

    private void Update()
    {
        if (gamestart == true)
        {
            gametime += Time.unscaledDeltaTime;
            if (gametime > 1.8f)
            {
                gameStartMassage.SetActive(false);
                slow.SetActive(true);
                playercam.GetComponent<CAM>().enabled = true;
                maincamera.GetComponent<CAM>().enabled = true;
            }
            else if (gametime > 1.5f)
            {
                gameStartText.text = chat3;
                gameStartMassage.transform.localScale = Vector3.one * (1.5f - (gametime - 1.5f) / 0.6f);
            }
            else if (gametime > 1.2f)
            {
                gameStartText.text = chat2;
                gameStartMassage.transform.localScale = Vector3.one * (1.5f - (gametime - 1.2f) / 0.6f);
            }
            else if (gametime > 0.9f)
            {
                gameStartText.text = chat1;
                gameStartMassage.transform.localScale = Vector3.one *  (1.5f-  (gametime - 0.9f) / 0.6f);
            }
        }
    }

}
