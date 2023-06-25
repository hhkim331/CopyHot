using Redcode.Pools;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1Manager : StageManager
{
    //UI
    [Header("UI")]
    [SerializeField] GameObject panel;
    [SerializeField] GameObject StageFallImage;
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] TextMeshProUGUI descText;
    [SerializeField] TextMeshProUGUI StageFallText;
    bool superText = false;
    float textChangeDelay = 0.8f;

    //스테이지 시작
    readonly string chat1 = "적을";
    readonly string chat2 = "모두";
    readonly string chat3 = "죽여라";

    string curChat = "";

    public GameObject gameStartMassage;
    public TextMeshProUGUI gameStartText;
    bool gamestart = false;

    float gametime;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        gameStartMassage.SetActive(true);
        gameStartText.text = " ";
        curChat = " ";
        Time.timeScale = 0;
        gamestart = true;
        gametime = 0f;

        SoundManager.Instance.PlayBGM("Stage1");
    }

    protected override void Update()
    {
        base.Update();
        if (gameClear) return;


        //스테이지 클리어
        if (stageClear)
        {
            if (superText)
            {
                textChangeTime += Time.unscaledDeltaTime;
                if (textChangeTime >= textChangeDelay)
                {
                    textChangeTime = 0f;
                    superText = false;
                    mainText.text = "HOT";
                    SoundManager.Instance.PlaySFX("hot");
                    mainText.fontStyle = FontStyles.Bold;
                }
            }
            else
            {
                textChangeTime += Time.unscaledDeltaTime;
                if (textChangeTime >= textChangeDelay)
                {
                    textChangeTime = 0f;
                    superText = true;
                    mainText.text = "SUPER";
                    SoundManager.Instance.PlaySFX("super");
                    mainText.fontStyle = FontStyles.Normal;
                }
            }
            panel.transform.localScale = Vector3.one * (1 + (textChangeDelay - textChangeTime) / textChangeDelay * 0.5f);

            //발사버튼 누르면 다음스테이지
            if (Input.GetButtonDown("Fire1"))
            {
                GAMECLEAR = true;
            }
        }
        else
        {
            if (gamestart == true && !slow.activeSelf)
            {
                gametime += Time.unscaledDeltaTime;
                if (gametime > 1.8f)
                {
                    gameStartMassage.SetActive(false);
                    slow.SetActive(true);
                    playerCam.enabled = true;
                    maincameraCam.enabled = true;
                }
                else if (gametime > 1.5f)
                {
                    if (curChat != chat3)
                    {
                        curChat = chat3;
                        gameStartText.text = chat3;
                        SoundManager.Instance.PlaySFX("message");
                    }
                    gameStartMassage.transform.localScale = Vector3.one * (1.5f - (gametime - 1.5f) / 0.6f);
                }
                else if (gametime > 1.2f)
                {
                    if (curChat != chat2)
                    {
                        curChat = chat2;
                        gameStartText.text = chat2;
                        SoundManager.Instance.PlaySFX("message");
                    }
                    gameStartMassage.transform.localScale = Vector3.one * (1.5f - (gametime - 1.2f) / 0.6f);
                }
                else if (gametime > 0.9f)
                {
                    if (curChat != chat1)
                    {
                        curChat = chat1;
                        gameStartText.text = chat1;
                        SoundManager.Instance.PlaySFX("message");
                    }
                    gameStartMassage.transform.localScale = Vector3.one * (1.5f - (gametime - 0.9f) / 0.6f);
                }
            }
        }

        ////스테이지 실패
        //if (stageFall)
        //{
        //    if (superText)
        //    {
        //        textChangeTime += Time.unscaledDeltaTime;
        //        if (textChangeTime >= textChangeDelay)
        //        {
        //            textChangeTime = 0f;
        //            superText = false;
        //            mainText.text = "HOT";
        //            mainText.fontStyle = FontStyles.Bold;
        //        }
        //    }
        //    else
        //    {
        //        textChangeTime += Time.unscaledDeltaTime;
        //        if (textChangeTime >= textChangeDelay)
        //        {
        //            textChangeTime = 0f;
        //            superText = true;
        //            mainText.text = "SUPER";
        //            mainText.fontStyle = FontStyles.Normal;
        //        }
        //    }
        //}
    }

    public override void StageClear()
    {
        base.StageClear();

        panel.SetActive(true);
        superText = true;
        mainText.text = "SUPER";
        SoundManager.Instance.PlaySFX("super");
        mainText.fontStyle = FontStyles.Normal;
    }

    public override void StageFALL()
    {
        base.StageFALL();
        StageFallImage.SetActive(true);
    }
}
