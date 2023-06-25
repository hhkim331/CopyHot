using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndManager : MonoBehaviour
{
    readonly string chat1 = "내가 해냈어!!!";
    readonly string chat2 = "이거 정말 엄청난 게임이 맞네.";
    readonly string chat3 = "정말 그렇지? 내가 생각해도 정말 만든거같아.";
    readonly string chat4 = "다음에는 내가 새로운 게임을 찾아서 소개해줄게.";
    readonly string chat5 = "그래? 기대하고 있을게.";
    readonly string chat6 = "다음에 또 보자, 안녕.";
    readonly string chat7 = "그래. 잘가.";
    readonly string chat8 = "EXIT";

    enum ChatState
    {
        None,
        Chat1,
        Chat2,
        Chat3,
        Chat4,
        Chat5,
        Chat6,
        Chat7,
        Chat8
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
    [SerializeField] TextMeshProUGUI chat8Text;

    [SerializeField] Button endButton;
    bool shine = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SoundManager.Instance.PlayBGM("Terminal2");
        yield return StartCoroutine(SceneFade.Instance.LoadScene_FadeOut());
        chatEnd = true;
    }

    // Update is called once per frame
    void Update()
    {
        //채팅이 진행중인경우 리턴
        if (!chatEnd) return;

        if (chatState == ChatState.Chat8)    //마지막 채팅이 다 나온경우
        {
            if (shine)
            {
                endButton.image.color = new Color(endButton.image.color.r + Time.deltaTime * 0.5f,
                    endButton.image.color.g + Time.deltaTime * 0.5f,
                    endButton.image.color.b + Time.deltaTime * 0.5f);
                if (endButton.image.color.r >= 1)
                    shine = false;
            }
            else
            {
                endButton.image.color = new Color(endButton.image.color.r - Time.deltaTime * 0.5f,
                    endButton.image.color.g - Time.deltaTime * 0.5f,
                    endButton.image.color.b - Time.deltaTime * 0.5f);
                if (endButton.image.color.r <= 0.7f)
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
                case ChatState.Chat7:

                    chatState = ChatState.Chat8;
                    StartCoroutine(Chat8());
                    break;

            }
        }
    }

    IEnumerator Chat1()
    {
        chat1Text.text = "";
        for (int i = 0; i < chat1.Length; i++)
        {
            if (chat1[i] == ' ')
                yield return new WaitForSeconds(0.12f);
            else
                yield return new WaitForSeconds(0.08f);
            chat1Text.text += chat1[i];
            if (i == chat1.Length - 1)
                SoundManager.Instance.PlaySFX("enter");
            else
                SoundManager.Instance.PlaySFX("space");
        }

        chatEnd = true;
    }
    IEnumerator Chat2()
    {
        yield return new WaitForSeconds(0.2f);
        chat2Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        chat2Text.text = "";
        for (int i = 0; i < chat2.Length; i++)
        {
            if (chat2[i] == ' ')
                yield return new WaitForSeconds(0.16f);
            else
                yield return new WaitForSeconds(0.08f);
            chat2Text.text += chat2[i];
            if (i == chat2.Length - 1)
                SoundManager.Instance.PlaySFX("enter");
            else
                SoundManager.Instance.PlaySFX("space");
        }

        chatEnd = true;
    }

    IEnumerator Chat3()
    {
        yield return new WaitForSeconds(0.2f);
        chat3Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        chat3Text.text = "";
        for (int i = 0; i < chat3.Length; i++)
        {
            if (chat3[i] == ' ')
                yield return new WaitForSeconds(0.16f);
            else
                yield return new WaitForSeconds(0.08f);
            chat3Text.text += chat3[i];
            if (i == chat3.Length - 1)
                SoundManager.Instance.PlaySFX("enter");
            else
                SoundManager.Instance.PlaySFX("space");
        }
        SoundManager.Instance.PlaySFX("enter");

        chatEnd = true;
    }

    IEnumerator Chat4()
    {
        yield return new WaitForSeconds(0.2f);
        chat4Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        chat4Text.text = "";
        for (int i = 0; i < chat4.Length; i++)
        {
            if (chat4[i] == ' ')
                yield return new WaitForSeconds(0.16f);
            else
                yield return new WaitForSeconds(0.08f);
            chat4Text.text += chat4[i];
            if (i == chat4.Length - 1)
                SoundManager.Instance.PlaySFX("enter");
            else
                SoundManager.Instance.PlaySFX("space");
        }
        SoundManager.Instance.PlaySFX("enter");

        chatEnd = true;
    }

    IEnumerator Chat5()
    {
        yield return new WaitForSeconds(0.2f);
        chat5Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        chat5Text.text = "";
        for (int i = 0; i < chat5.Length; i++)
        {
            if (chat5[i] == ' ')
                yield return new WaitForSeconds(0.16f);
            else
                yield return new WaitForSeconds(0.08f);
            chat5Text.text += chat5[i];
            if (i == chat5.Length - 1)
                SoundManager.Instance.PlaySFX("enter");
            else
                SoundManager.Instance.PlaySFX("space");
        }
        SoundManager.Instance.PlaySFX("enter");

        chatEnd = true;
    }

    IEnumerator Chat6()
    {
        yield return new WaitForSeconds(0.2f);
        chat6Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        chat6Text.text = "";
        for (int i = 0; i < chat6.Length; i++)
        {
            if (chat6[i] == ' ')
                yield return new WaitForSeconds(0.16f);
            else
                yield return new WaitForSeconds(0.08f);
            chat6Text.text += chat6[i];
            if (i == chat6.Length - 1)
                SoundManager.Instance.PlaySFX("enter");
            else
                SoundManager.Instance.PlaySFX("space");
        }
        SoundManager.Instance.PlaySFX("enter");

        chatEnd = true;
    }

    IEnumerator Chat7()
    {
        yield return new WaitForSeconds(0.2f);
        chat7Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        chat7Text.text = "";
        for (int i = 0; i < chat7.Length; i++)
        {
            if (chat7[i] == ' ')
                yield return new WaitForSeconds(0.16f);
            else
                yield return new WaitForSeconds(0.08f);
            chat7Text.text += chat7[i];
            if (i == chat7.Length - 1)
                SoundManager.Instance.PlaySFX("enter");
            else
                SoundManager.Instance.PlaySFX("space");
        }
        SoundManager.Instance.PlaySFX("enter");

        chatEnd = true;
    }

    IEnumerator Chat8()
    {
        yield return new WaitForSeconds(0.2f);
        chat8Text.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.4f);

        chat8Text.text = "";
        for (int i = 0; i < chat8.Length; i++)
        {
            if (chat8[i] == ' ')
                yield return new WaitForSeconds(0.16f);
            else
                yield return new WaitForSeconds(0.08f);
            chat8Text.text += chat8[i];
            if (i == chat8.Length - 1)
                SoundManager.Instance.PlaySFX("enter");
            else
                SoundManager.Instance.PlaySFX("space");
        }
        SoundManager.Instance.PlaySFX("enter");

        chatEnd = true;
        endButton.interactable = true;
    }


    void EndButtonEvent()
    {
        SceneFade.Instance.nextSceneName = "Title";
        StartCoroutine(SceneFade.Instance.LoadScene_FadeIn());
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (endButton != null && endButton.onClick.GetPersistentEventCount() == 0)
        {
            UnityEditor.Events.UnityEventTools.AddPersistentListener(endButton.onClick, EndButtonEvent);
        }
    }
#endif
}
