using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndManager : MonoBehaviour
{
    readonly string chat1 = "탈출이다!";
    readonly string chat2 = "이럴 수는 없어!!!";
    readonly string chat3 = "잘 있어라 망할 놈아.";
    readonly string chat4 = "EXIT";

    enum ChatState
    {
        None,
        Chat1,
        Chat2,
        Chat3,
        Chat4
    }
    ChatState chatState = ChatState.None;

    bool chatEnd = false;
    float chatDelay = 0.1f;
    float chatDelayTime = 0f;

    [SerializeField] TextMeshProUGUI chat1Text;
    [SerializeField] TextMeshProUGUI chat2Text;
    [SerializeField] TextMeshProUGUI chat3Text;
    [SerializeField] TextMeshProUGUI chat4Text;

    [SerializeField] Button endButton;
    bool shine = false;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        yield return StartCoroutine(SceneFade.Instance.LoadScene_FadeOut());
        chatEnd = true;
    }

    // Update is called once per frame
    void Update()
    {
        //채팅이 진행중인경우 리턴
        if (!chatEnd) return;

        if (chatState == ChatState.Chat4)    //마지막 채팅이 다 나온경우
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
        endButton.interactable = true;
    }

    void EndButtonEvent()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
