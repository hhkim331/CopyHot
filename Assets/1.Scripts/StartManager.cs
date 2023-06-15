using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartManager : MonoBehaviour
{
    readonly string chat1 = "너 SuperHot이라고 해봤어? 진짜 역대급이네 이거.";
    readonly string chat2 = "갑자기 무슨 소리야?";
    readonly string chat3 = "superhot.exe";
    readonly string chat4 = "크랙된 거라 말이지.";
    readonly string chat5 = "제작사 사이트에 정보 입력하면 플레이 할 수 있어.\n완전 쩌는 슈팅게임이야!";
    readonly string chat6 = "CopyHot.exe을 실행하면 전부 다 알아서 진행될 거야.";
    readonly string chat7 = "CopyHot.exe";

    enum ChatState
    {
        None,
        Chat1,
        Chat2,
        Chat3,
        Chat4,
        Chat5,
        Chat6,
        Chat7
    }
    ChatState chatState = ChatState.None;

    bool chatEnd = false;
    float chatDelay = 0.1f;
    float chatDelayTime = 0f;

    [SerializeField] TextMeshProUGUI chat1Text;
    [SerializeField] TextMeshProUGUI chat2Text;
    [SerializeField] TextMeshProUGUI chat3Text;
    [SerializeField] TextMeshProUGUI chat4Text;
    [SerializeField] TextMeshProUGUI chat5Text;
    [SerializeField] TextMeshProUGUI chat6Text;
    [SerializeField] TextMeshProUGUI chat7Text;

    [SerializeField] Button startButton;
    bool shine = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine(SceneFade.Instance.LoadScene_FadeOut());
        chatEnd = true;
    }

    // Update is called once per frame
    void Update()
    {
        //채팅이 진행중인경우 리턴
        if (!chatEnd) return;

        if (chatState == ChatState.Chat7)    //마지막 채팅이 다 나온경우
        {
            if (shine)
            {
                startButton.image.color = new Color(startButton.image.color.r + Time.deltaTime * 0.5f,
                    startButton.image.color.g + Time.deltaTime * 0.5f,
                    startButton.image.color.b + Time.deltaTime * 0.5f);
                if (startButton.image.color.r >= 1)
                    shine = false;
            }
            else
            {
                startButton.image.color = new Color(startButton.image.color.r - Time.deltaTime * 0.5f,
                    startButton.image.color.g - Time.deltaTime * 0.5f,
                    startButton.image.color.b - Time.deltaTime * 0.5f);
                if (startButton.image.color.r <= 0.7f)
                    shine = true;
            }
            return;
        }

        chatDelayTime += Time.deltaTime;
        if (chatDelayTime > chatDelay)
        {
            chatEnd = false;
            chatDelayTime = 0f;
            switch (chatState)
            {
                case ChatState.None:
                    chatState = ChatState.Chat1;
                    StartCoroutine(Chat1());
                    break;
                case ChatState.Chat1:
                    chatState = ChatState.Chat2;
                    StartCoroutine(Chat2());
                    break;
                case ChatState.Chat2:
                    chatState = ChatState.Chat3;
                    StartCoroutine(Chat3());
                    break;
                case ChatState.Chat3:
                    chatState = ChatState.Chat4;
                    StartCoroutine(Chat4());
                    break;
                case ChatState.Chat4:
                    chatState = ChatState.Chat5;
                    StartCoroutine(Chat5());
                    break;
                case ChatState.Chat5:
                    chatState = ChatState.Chat6;
                    StartCoroutine(Chat6());
                    break;
                case ChatState.Chat6:
                    chatState = ChatState.Chat7;
                    StartCoroutine(Chat7());
                    break;
            }
        }
    }

    IEnumerator Chat1()
    {
        chat1Text.text = "";
        for (int i = 0; i < chat1.Length; i++)
        {
            chat1Text.text += chat1[i];
            yield return new WaitForSeconds(0.1f);
        }

        chatEnd = true;
    }
    IEnumerator Chat2()
    {
        chat2Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        chat2Text.text = "";
        for (int i = 0; i < chat2.Length; i++)
        {
            chat2Text.text += chat2[i];
            yield return new WaitForSeconds(0.1f);
        }

        chatEnd = true;
    }

    IEnumerator Chat3()
    {
        chat3Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        chat3Text.text = "";
        for (int i = 0; i < chat3.Length; i++)
        {
            chat3Text.text += chat3[i];
            yield return new WaitForSeconds(0.1f);
        }

        chatEnd = true;
    }

    IEnumerator Chat4()
    {
        chat4Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        chat4Text.text = "";
        for (int i = 0; i < chat4.Length; i++)
        {
            chat4Text.text += chat4[i];
            yield return new WaitForSeconds(0.1f);
        }

        chatEnd = true;
    }

    IEnumerator Chat5()
    {
        chat5Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        chat5Text.text = "";
        for (int i = 0; i < chat5.Length; i++)
        {
            chat5Text.text += chat5[i];
            yield return new WaitForSeconds(0.1f);
        }

        chatEnd = true;
    }

    IEnumerator Chat6()
    {
        chat6Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        chat6Text.text = "";
        for (int i = 0; i < chat6.Length; i++)
        {
            chat6Text.text += chat6[i];
            yield return new WaitForSeconds(0.1f);
        }

        chatEnd = true;
    }

    IEnumerator Chat7()
    {
        chat7Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);

        chat7Text.text = "";
        for (int i = 0; i < chat7.Length; i++)
        {
            chat7Text.text += chat7[i];
            yield return new WaitForSeconds(0.1f);
        }

        chatEnd = true;
        startButton.interactable = true;
    }

    void StartButtonEvent()
    {
        SceneFade.Instance.nextSceneName = "Stage1";
        StartCoroutine(SceneFade.Instance.LoadScene_FadeIn());
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (startButton != null && startButton.onClick.GetPersistentEventCount() == 0)
        {
            UnityEditor.Events.UnityEventTools.AddPersistentListener(startButton.onClick, StartButtonEvent);
        }
    }
#endif
}
