using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public int stageNumber;

    public EnemySpawner enemySpawner;

    // Start is called before the first frame update
    void Start()
    {
        StageSpawnData stageSpawnData = GameManager.Instance.totalEnemySpawnData.stageSpawnDatas[stageNumber];

        enemySpawner.Set(stageSpawnData);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
