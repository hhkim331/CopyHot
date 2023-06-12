using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public int stageNumber;

    public EnemySpawner enemySpawner;
    public PoolManager poolManager;

    bool activeReset = false;
    bool stageClear = false;
    bool stageFall = false;

    //게임 재시작 변수
    public float restartTime = 3f;
    //재시작키 입력시간
    float restartInputTime = 0f;

    //UI
    [Header("UI")]
    [SerializeField] GameObject panel;
    [SerializeField] GameObject StageFallImage;
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] TextMeshProUGUI descText;
    [SerializeField] TextMeshProUGUI StageFallText;
    bool superText = false;
    float textChangeTime = 0f;
    float textChangeDelay = 1f;


    void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        StageSpawnData stageSpawnData = GameManager.Instance.totalEnemySpawnData.stageSpawnDatas[stageNumber];

        enemySpawner.Set(stageSpawnData);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            restartInputTime = 0f;
            if (activeReset)
            {
                //재시작
                //씬 다시 로딩
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
        else if (Input.GetKey(KeyCode.R))
        {
            restartInputTime += Time.unscaledDeltaTime;
            if (restartInputTime >= restartTime)
            {
                //씬 다시 로딩
                restartInputTime = 0f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        //스테이지 클리어
        if (stageClear)
        {
            if(superText)
            {
                textChangeTime += Time.unscaledDeltaTime;
                if (textChangeTime >= textChangeDelay)
                {
                    textChangeTime = 0f;
                    superText = false;
                    mainText.text = "HOT";
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
                    mainText.fontStyle = FontStyles.Normal;
                }
            }
        }

        //스테이지 실패
        if (stageFall)
        {
            if (superText)
            {
                textChangeTime += Time.unscaledDeltaTime;
                if (textChangeTime >= textChangeDelay)
                {
                    textChangeTime = 0f;
                    superText = false;
                    mainText.text = "HOT";
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
                    mainText.fontStyle = FontStyles.Normal;
                }
            }
        }
    }

    /// <summary>
    /// 스테이지 클리어
    /// </summary>
    public void StageClear()
    {
        stageClear = true;
        activeReset = true;
        //레코드 실행

        //UI 활성화
        panel.SetActive(true);

        textChangeTime = 0f;
        superText = true;
        mainText.text = "SUPER";
        mainText.fontStyle = FontStyles.Normal;
    }

    public void StageFALL()
    {
        //멘트 활성화
        stageFall = true;
        activeReset = true;

        StageFallImage.SetActive(true);
    }
}
