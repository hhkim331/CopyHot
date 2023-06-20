using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct LoadingSoundInfo
{
    [Header("Key value")]
    public string key;
    [Header("Directory in Resrouces folder")]
    public AudioClip audioClip;
}

public class SoundManager : Singleton<SoundManager>
{
    float bgmVolume = 1;
    public float BGMVolume
    {
        set
        {
            bgmVolume = value;
            if (bgmCoroutine != null)
                StopCoroutine(bgmCoroutine);
            bgmCoroutine = StartCoroutine(BGMFade(value, 1f));
        }
    }
    Coroutine bgmCoroutine = null;

    float sfxVolume = 1;
    public float SFXVolume
    {
        set
        {
            sfxVolume = value;
            if (sfxCoroutine != null)
                StopCoroutine(sfxCoroutine);
            sfxCoroutine = StartCoroutine(SFXFade(value, 1f));
        }
    }
    Coroutine sfxCoroutine = null;

    [SerializeField] List<LoadingSoundInfo> loadingBGMSoundInfos = new List<LoadingSoundInfo>();
    [SerializeField] List<LoadingSoundInfo> loadingSFXSoundInfos = new List<LoadingSoundInfo>();

    Dictionary<string, AudioClip> sfxContainer = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> bgmContainer = new Dictionary<string, AudioClip>();

    GameObject bgmObj = null;   // 백그라운드 오브젝트
    AudioSource bgmSrc = null;  // 백그라운드 AudioSource 컴포넌트

    int sfxMaxCount = 4;
    int sfxCurCount = 0;
    List<GameObject> sfxObjList = new List<GameObject>(); //ArrayList m_sndObjList = new ArrayList();          // 효과음 오브젝트
    AudioSource[] sfxSrcList = new AudioSource[4];  // 넉넉히 만들어 놓는다.

    AudioClip a_GAudioClip = null;

    // Start is called before the first frame update

    protected override void Awake()
    {
        base.Awake();
        LoadSound();
        LoadChildGameObj();
    }

    void LoadSound()
    {
        //bgm
        for (int i = 0; i < loadingBGMSoundInfos.Count; i++)
        {
            bgmContainer.Add(loadingBGMSoundInfos[i].key, loadingBGMSoundInfos[i].audioClip);
        }

        //sfx
        for (int i = 0; i < loadingSFXSoundInfos.Count; i++)
        {
            sfxContainer.Add(loadingSFXSoundInfos[i].key, loadingSFXSoundInfos[i].audioClip);
        }
    }

    public void LoadChildGameObj()
    {
        //m_bgmObj == null 이면 PlayBGM()하게 되면 다시 로딩하게 된다. 
        if (bgmObj == null)
        {
            bgmObj = new GameObject();
            bgmObj.transform.SetParent(this.transform);
            bgmObj.transform.position = Vector3.zero;
            bgmSrc = bgmObj.AddComponent<AudioSource>();
            bgmSrc.playOnAwake = false;
            bgmObj.name = "BGMObj";
        }

        for (int a_ii = 0; a_ii < sfxMaxCount; a_ii++)
        {
            // 최대 4개까지 재생되게 제어 렉방지(Androud: 4개, PC: 무제한)  
            if (sfxObjList.Count < sfxMaxCount)
            {
                GameObject newSoundOBJ = new GameObject();
                newSoundOBJ.transform.SetParent(this.transform);
                newSoundOBJ.transform.localPosition = Vector3.zero;
                AudioSource a_AudioSrc = newSoundOBJ.AddComponent<AudioSource>();
                a_AudioSrc.playOnAwake = false;
                a_AudioSrc.loop = false;
                newSoundOBJ.name = "SFXObj";

                sfxSrcList[sfxObjList.Count] = a_AudioSrc;
                sfxObjList.Add(newSoundOBJ);
            }
        }//for (int a_ii = 0; a_ii < m_EffSdCount; a_ii++)
    }

    #region BGM
    public void PlayBGM(string key, float volume = 1f)
    {
        a_GAudioClip = bgmContainer[key];

        //Scene이 넘어가면 GameObject는 지워지고, m_bgmObj == null 이면 
        //PlayBGM()하게 되면 다시 로딩하게 된다. 
        if (bgmObj == null)
        {
            bgmObj = new GameObject();
            bgmObj.transform.SetParent(this.transform);
            bgmObj.transform.position = Vector3.zero;
            bgmSrc = bgmObj.AddComponent<AudioSource>();
            bgmSrc.playOnAwake = false;
            bgmObj.name = "BGMObj";
        }

        if (a_GAudioClip != null && bgmSrc != null)
        {
            if (bgmSrc.clip == a_GAudioClip)
                return;

            bgmSrc.clip = a_GAudioClip;
            bgmSrc.volume = volume;
            bgmSrc.loop = true;
            bgmSrc.spatialBlend = 0f;
            bgmSrc.Play(0);
        }
    }

    IEnumerator BGMFade(float endValue, float fadeTime)
    {
        float time = 0;
        while (time < fadeTime)
        {
            yield return null;
            bgmSrc.volume = Mathf.Lerp(bgmSrc.volume, endValue, time / fadeTime);
            time += Time.deltaTime;
        }
    }
    #endregion

    #region SFX
    //효과음 플레이 함수
    public void PlaySFX(string key, float volume = 1f, float delay = 0, bool bLoop = false)
    {
        if (sfxContainer.ContainsKey(key) == false)
            return;

        a_GAudioClip = sfxContainer[key];

        // 최대 4개까지 재생
        if (sfxObjList.Count < sfxMaxCount)
        {
            GameObject newSoundOBJ = new GameObject();
            newSoundOBJ.transform.SetParent(this.transform);
            newSoundOBJ.transform.localPosition = Vector3.zero;
            AudioSource a_AudioSrc = newSoundOBJ.AddComponent<AudioSource>();
            a_AudioSrc.playOnAwake = false;
            newSoundOBJ.name = "SFXObj";

            sfxSrcList[sfxObjList.Count] = a_AudioSrc;
            sfxObjList.Add(newSoundOBJ);
        }

        if (a_GAudioClip != null && sfxSrcList[sfxCurCount] != null)
        {
            sfxSrcList[sfxCurCount].clip = a_GAudioClip;
            sfxSrcList[sfxCurCount].volume = volume;
            sfxSrcList[sfxCurCount].loop = bLoop;
            sfxSrcList[sfxCurCount].PlayDelayed(delay);

            sfxCurCount++;
            if (sfxMaxCount <= sfxCurCount)
                sfxCurCount = 0;
        }
    }

    //동일한 효과음 한번만 호출
    public void PlaySFXOnce(string key, float volume = 1f, float delay = 0, bool bLoop = false)
    {
        if (sfxContainer.ContainsKey(key) == false)
            return;

        a_GAudioClip = sfxContainer[key];

        foreach (var sfxSrc in sfxSrcList)
        {
            if (sfxSrc.clip == a_GAudioClip)
            {
                if (!sfxSrc.isPlaying)
                {
                    sfxSrc.volume = volume;
                    sfxSrc.loop = bLoop;
                    sfxSrc.PlayDelayed(delay);
                }
                return;
            }
        }

        // 최대 4개까지 재생
        if (sfxObjList.Count < sfxMaxCount)
        {
            GameObject newSoundOBJ = new GameObject();
            newSoundOBJ.transform.SetParent(this.transform);
            newSoundOBJ.transform.localPosition = Vector3.zero;
            AudioSource a_AudioSrc = newSoundOBJ.AddComponent<AudioSource>();
            a_AudioSrc.playOnAwake = false;
            newSoundOBJ.name = "SFXObj";

            sfxSrcList[sfxObjList.Count] = a_AudioSrc;
            sfxObjList.Add(newSoundOBJ);
        }

        if (a_GAudioClip != null && sfxSrcList[sfxCurCount] != null)
        {
            sfxSrcList[sfxCurCount].clip = a_GAudioClip;
            sfxSrcList[sfxCurCount].volume = volume;
            sfxSrcList[sfxCurCount].loop = bLoop;
            sfxSrcList[sfxCurCount].PlayDelayed(delay);

            sfxCurCount++;
            if (sfxMaxCount <= sfxCurCount)
                sfxCurCount = 0;
        }
    }

    public void PlaySFXFromObject(AudioSource audioSource, string key, float volume = 1f, float delay = 0, bool bLoop = false)
    {
        if (sfxContainer.ContainsKey(key) == false)
            return;

        a_GAudioClip = sfxContainer[key];

        if (a_GAudioClip != null && audioSource != null)
        {
            audioSource.clip = a_GAudioClip;
            audioSource.volume = volume;
            audioSource.loop = bLoop;
            audioSource.playOnAwake = false;
            audioSource.PlayDelayed(delay);

            sfxCurCount++;
            if (sfxMaxCount <= sfxCurCount)
                sfxCurCount = 0;
        }
    }

    IEnumerator SFXFade(float endValue, float fadeTime)
    {
        float time = 0;
        while (time < fadeTime)
        {
            yield return null;
            for (int i = 0; i < sfxSrcList.Length; i++)
                sfxSrcList[i].volume = Mathf.Lerp(sfxSrcList[i].volume, endValue, time / fadeTime);
            time += Time.deltaTime;
        }
    }

    public void ClearAllSFX()
    {
        foreach (var src in sfxSrcList)
            src.Stop();
    }
    #endregion
}