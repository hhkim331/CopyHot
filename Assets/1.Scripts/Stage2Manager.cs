using Redcode.Pools;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Stage2Manager : StageManager
{
    //UI
    [Header("UI")]
    [SerializeField] GameObject panel;
    [SerializeField] GameObject StageFallImage;
    [SerializeField] TextMeshProUGUI StageFallText;

    //스테이지 시작
    readonly string chat1 = "코어를";
    readonly string chat2 = "해킹중입니다";

    string curChat = "";

    public GameObject gameMassage;
    public TextMeshProUGUI messageText;
    bool gamestart = false;

    float gametime;

    //해킹
    public Slider hackingSlider;
    public TextMeshProUGUI hackingText;
    float hackingTime = 20f;
    float hackingImageTime = 1f;
    Vector3 hackCameraStartPos;
    [SerializeField] RectTransform hackImageTopRT;
    [SerializeField] RectTransform hackImageBottomRT;
    [SerializeField] RectTransform hackImageLeftRT;
    [SerializeField] RectTransform hackImageRightRT;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        gameMassage.SetActive(true);
        messageText.text = " ";
        curChat = " ";
        Time.timeScale = 0;
        gamestart = true;
        gametime = 0f;
        hacking = false;

        SoundManager.Instance.PlayBGM("Stage1");
    }

    protected override void Update()
    {
        base.Update();
        if (gameClear) return;

        if(pause)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                pause = false;
                gameMassage.SetActive(false);
                slow.SetActive(true);
            }
            return;
        }

        //스테이지 클리어
        if (stageClear)
        {
            gametime+=Time.unscaledDeltaTime;

            Camera.main.transform.position = Vector3.Lerp(hackCameraStartPos, cameraTargetPos, gametime);
            hackImageTopRT.localPosition = Vector3.Lerp(new Vector3(0,1500,0), new Vector3(0, 500, 0), gametime);
            hackImageBottomRT.localPosition = Vector3.Lerp(new Vector3(0, -1500, 0), new Vector3(0, -500, 0), gametime);
            hackImageLeftRT.localPosition = Vector3.Lerp(new Vector3(-2000, 0, 0), new Vector3(-750, 0, 0), gametime);
            hackImageRightRT.localPosition = Vector3.Lerp(new Vector3(2000, 0, 0), new Vector3(750, 0, 0), gametime);
            if(gametime>hackingImageTime)
                GAMECLEAR = true;
        }
        else
        {
            if (gamestart == true && !slow.activeSelf)
            {
                gametime += Time.unscaledDeltaTime;
                if (gametime > 1.5f)
                {
                    gameMassage.SetActive(false);
                    slow.SetActive(true);
                    playerCam.enabled = true;
                    maincameraCam.enabled = true;
                    gametime = 0f;
                    textChangeTime = 0f;

                    //슬라이더 활성화
                    hackingSlider.gameObject.SetActive(true);
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
            else if(!stageFall && !hacking)
            {
                //코어 해킹중
                gametime += Time.deltaTime;
                textChangeTime += Time.deltaTime;

                float per = gametime / hackingTime;

                hackingSlider.value = gametime / hackingTime;
                if (gametime > hackingTime)
                {
                    hackingText.text = "해킹완료";
                    hacking = true;
                    pause = true;
                    //화면 일시 멈춤
                    slow.SetActive(false);
                    Time.timeScale = 0;
                    //메시지 출력
                    gameMassage.SetActive(true);
                    messageText.text = "E키를 눌러\n코어에\n접속하세요";
                    SoundManager.Instance.PlaySFX("message");
                }
                else
                {
                    string str1;
                    string str2;
                    if (textChangeTime > 2.4f)
                    {
                        textChangeTime = 0f;
                        str1 = " 해킹중...";
                    }
                    else if (textChangeTime > 1.6f)
                    {
                        str1 = " 해킹중..";
                    }
                    else if (textChangeTime > 0.8f)
                    {
                        str1 = " 해킹중.";
                    }
                    else
                    {
                        str1 = " 해킹중";
                    }
                    str2 = string.Format(" {0:0.0}%", per * 100f);
                    hackingText.text = str1 + str2;
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
        gametime = 0f;
        playerCam.enabled = false;
        maincameraCam.enabled = false;
        Camera.main.transform.parent = null;
        hackCameraStartPos = Camera.main.transform.position;
    }

    public override void StageFALL()
    {
        base.StageFALL();
        StageFallImage.SetActive(true);
    }
}