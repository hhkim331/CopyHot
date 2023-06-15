using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneFade.Instance.nextSceneName = "Start";
            StartCoroutine(SceneFade.Instance.LoadScene_FadeIn());
        }
    }
}
