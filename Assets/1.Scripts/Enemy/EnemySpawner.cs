using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    StageSpawnData stageSpawnData;
    int lastStep = 0;
    int curStep = 0;
    float stepTime = 0f;

    //해당 스텝동안 소환된 적들의 아이디를 저장하는 리스트
    List<int> spawnIdList;
    //해당 스텝중 살아있는 적들의 아이디를 저장하는 리스트
    List<int> liveSpawnIdList;

    public GameObject enemyPrefab;

    //// Start is called before the first frame update
    //void Start()
    //{
    //}

    bool looped = false;

    // Update is called once per frame
    void Update()
    {
        stepTime += Time.deltaTime;

        foreach (EnemySpawnData enemySpawnData in stageSpawnData.enemySpawnDatas)
        {
            if (enemySpawnData.step != curStep)
                continue;

            if (stepTime > enemySpawnData.createTime && !spawnIdList.Contains(enemySpawnData.id))
                SpawnEnemy(enemySpawnData);
        }
    }

    public void Set(StageSpawnData stageSpawnData)
    {
        this.stageSpawnData = stageSpawnData;
        looped = stageSpawnData.SceneName == "Stage2"? true : false;

        foreach (EnemySpawnData enemySpawnData in stageSpawnData.enemySpawnDatas)
        {
            if(lastStep < enemySpawnData.step)
                lastStep = enemySpawnData.step;
        }
        curStep = 0;
        Spawn();
    }

    public void CheckStepEnd(int id)
    {
        liveSpawnIdList.Remove(id);
        if (liveSpawnIdList.Count > 0)
            return;

        if (curStep ==lastStep && !looped)
        {
            //스테이지 클리어
            StageManager.Instance.StageClear();
            return;
        }

        if (looped && curStep == lastStep)
        {
            Spawn();
            return;
        }

        curStep++;
        Spawn();
    }

    public void Spawn()
    {
        stepTime = 0;
        //아이디 리스트 초기화
        if (spawnIdList != null) spawnIdList.Clear();
        else spawnIdList = new List<int>();
        if (liveSpawnIdList != null) liveSpawnIdList.Clear();
        else liveSpawnIdList = new List<int>();

        foreach (EnemySpawnData enemySpawnData in stageSpawnData.enemySpawnDatas)
        {
            if (enemySpawnData.step != curStep)
                continue;

            if (enemySpawnData.createTime == -1)
                SpawnEnemy(enemySpawnData, true);
        }
    }

    /// <summary>
    /// 적 소환
    /// </summary>
    /// <param name="enemySpawnData"></param>
    /// <param name="immediate"></param>
    void SpawnEnemy(EnemySpawnData enemySpawnData, bool immediate = false)
    {
        spawnIdList.Add(enemySpawnData.id);
        liveSpawnIdList.Add(enemySpawnData.id);

        GameObject enemy = Instantiate(enemyPrefab);
        enemy.GetComponent<Enemy>().Set(enemySpawnData, immediate);
    }
}
