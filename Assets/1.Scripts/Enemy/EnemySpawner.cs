using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    StageSpawnData stageSpawnData;
    int curStep = 0;
    float stepTime = 0f;

    //�ش� ���ܵ��� ��ȯ�� ������ ���̵� �����ϴ� ����Ʈ
    List<int> spawnIdList;

    public GameObject enemyPrefab;

    //// Start is called before the first frame update
    //void Start()
    //{
    //}

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
        Spawn(0);
    }

    public void Spawn(int step)
    {
        curStep = step;

        //�������̵� ����Ʈ �ʱ�ȭ
        if (spawnIdList != null) spawnIdList.Clear();
        else spawnIdList = new List<int>();

        foreach (EnemySpawnData enemySpawnData in stageSpawnData.enemySpawnDatas)
        {
            if (enemySpawnData.step != step)
                continue;

            if (enemySpawnData.createTime == -1)
                SpawnEnemy(enemySpawnData, true);
        }
    }

    /// <summary>
    /// �� ��ȯ
    /// </summary>
    /// <param name="enemySpawnData"></param>
    /// <param name="immediate"></param>
    void SpawnEnemy(EnemySpawnData enemySpawnData, bool immediate = false)
    {
        spawnIdList.Add(enemySpawnData.id);

        GameObject enemy = Instantiate(enemyPrefab);
        enemy.GetComponent<Enemy>().Set(enemySpawnData, immediate);
    }
}
