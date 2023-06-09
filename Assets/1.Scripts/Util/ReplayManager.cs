using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//유저의 입력정보를 저장하는 클래스

public class ReplayInput
{
    public bool isLeft;
    public bool isRight;
    public bool isJump;
    public bool isAttack;
    public bool isDash;

    public ReplayInput(bool isLeft, bool isRight, bool isJump, bool isAttack, bool isDash)
    {
        this.isLeft = isLeft;
        this.isRight = isRight;
        this.isJump = isJump;
        this.isAttack = isAttack;
        this.isDash = isDash;
    }
}

//카메라의 움직임 정보를 저장하는 클래스
public class ReplayCamera
{
    public Vector3 position;
    public Quaternion rotation;

    public ReplayCamera(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}

//리플레이 정보를 저장하는 클래스
public class ReplayInfo
{
    public float time;
    public ReplayInput input;

    public ReplayInfo(float time, ReplayInput input)
    {
        this.time = time;
        this.input = input;
    }
}

//리플레이 정보를 저장하는 클래스
public class ReplayManager : MonoBehaviour
{
    public static ReplayManager Instance;

    public int stageNumber;

    public EnemySpawner enemySpawner;
    public PoolManager poolManager;

    bool stageClear = false;

    //게임 재시작 변수
    public float restartTime = 3f;
    //재시작키 입력시간
    float restartInputTime = 0f;

    //UI
    [Header("UI")]
    [SerializeField] GameObject panel;
    [SerializeField] TextMeshProUGUI mainText;
    [SerializeField] TextMeshProUGUI descText;
    bool superText = false;
    float textChangeTime = 0f;
    float textChangeDelay = 1f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}