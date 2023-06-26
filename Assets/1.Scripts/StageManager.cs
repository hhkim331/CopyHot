using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public int stageNumber;
    public EnemySpawner enemySpawner;

    public string nextSceneName = "";
    protected bool gameClear = false;
    public bool GAMECLEAR
    {
        get
        {
            return gameClear;
        }
        set
        {
            if (!gameClear)
            {
                SceneFade.Instance.nextSceneName = nextSceneName;
                slow.SetActive(false);
                Time.timeScale = 1;
                SoundManager.Instance.ChangeSFXPitch(1);
                StartCoroutine(SceneFade.Instance.LoadScene_FadeIn());
            }
            gameClear = value;
        }
    }

    protected bool stageClear = false;
    public bool STAGECLEAR
    {
        get
        {
            return stageClear;
        }
        set
        {
            stageClear = value;
        }
    }

    protected bool stageFall = false;

    protected float textChangeTime = 0f;
    //게임 재시작 변수
    public float restartTime = 3f;
    //재시작키 입력시간
    float restartInputTime = 0f;
    //해킹
    public bool hacking = false;
    public Vector3 cameraTargetPos;

    public GameObject slow;
    public CAM playerCam;
    public CAM maincameraCam;

    public bool pause=false;


    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        StageSpawnData stageSpawnData = GameManager.Instance.totalEnemySpawnData.stageSpawnDatas[stageNumber];
        enemySpawner.Set(stageSpawnData);
        StartCoroutine(SceneFade.Instance.LoadScene_FadeOut());

        playerCam.enabled = false;
        maincameraCam.enabled = false;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            restartInputTime = 0f;
            if (stageFall)
            {
                //재시작
                //씬 다시 로딩
                SceneFade.Instance.nextSceneName = SceneManager.GetActiveScene().name;
                StartCoroutine(SceneFade.Instance.LoadScene_FadeIn());
            }
        }
        else if (Input.GetKey(KeyCode.R))
        {
            restartInputTime += Time.unscaledDeltaTime;
            if (restartInputTime >= restartTime)
            {
                //씬 다시 로딩
                restartInputTime = 0f;
                SceneFade.Instance.nextSceneName = SceneManager.GetActiveScene().name;
                StartCoroutine(SceneFade.Instance.LoadScene_FadeIn());
            }
        }
    }

    public virtual void StageClear()
    {
        stageClear = true;

        textChangeTime = 0f;
    }

    public virtual void StageFALL()
    {
        //멘트 활성화
        stageFall = true;
    }
}
