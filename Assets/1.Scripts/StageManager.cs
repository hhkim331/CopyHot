using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance;

    public int stageNumber;

    public EnemySpawner enemySpawner;
    public PoolManager poolManager;

    //UI


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

    /// <summary>
    /// 스테이지 클리어
    /// </summary>
    public void StageClear()
    {

    }
}
