using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayBGM("Terminal1");
        StartCoroutine(SceneFade.Instance.LoadScene_FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneFade.Instance.nextSceneName = "Start";
            SoundManager.Instance.PlaySFX("start");
            StartCoroutine(SceneFade.Instance.LoadScene_FadeIn());
        }
    }
}
