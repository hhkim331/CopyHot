using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFade : Singleton<SceneFade>
{
    [SerializeField] GameObject SplashObj;  //판넬오브젝트
    [SerializeField] Image image;           //판넬 이미지

    public bool fade = false;               //페이드인 페이드아웃 체크
    public string nextSceneName;            //다음 씬 이름

    public IEnumerator LoadScene_FadeIn()
    {
        fade = true;
        SplashObj.SetActive(true);
        SoundManager.Instance.BGMVolume = -80;
        float a = 0f;
        while (a < 1f)
        {
            a += Time.unscaledDeltaTime;
            if (image.color.a < 1)
                image.color = new Color(image.color.r, image.color.g, image.color.b, a);
            yield return null;
        }

        SceneManager.LoadScene(nextSceneName);
    }

    public IEnumerator LoadScene_FadeOut()
    {
        SoundManager.Instance.BGMVolume = 0;
        float a = 1f;
        while (image.color.a > 0f)
        {
            a -= Time.unscaledDeltaTime;
            if (image.color.a > 0)
                image.color = new Color(image.color.r, image.color.g, image.color.b, a);
            yield return null;
        }

        SplashObj.SetActive(false);
        fade = false;
    }

    public void SetBlack()
    {
        image.color = new Color(0, 0, 0, 1);
    }
}
