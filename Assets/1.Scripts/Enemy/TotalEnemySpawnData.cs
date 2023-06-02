using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class EnemySpawnData
{
    //아이디
    public int id = 0;
    //스텝
    public int step = 0;
    //생성시간
    public float createTime = 0f;
    //위치
    public Vector3 position = Vector3.zero;
    //회전방향
    public Vector3 rotation = Vector3.zero;
    //기본 장비상태
    public int defaultWeapon = 0;
}

[Serializable]
public class StageSpawnData
{
    public EnemySpawnData[] enemySpawnDatas;
}

[CreateAssetMenu(fileName = "TotalEnemySpawnData", menuName = "Scriptable Object/TotalEnemySpawnData")]
public class TotalEnemySpawnData : ScriptableObject
{
    public int stageCount = 0;
    public StageSpawnData[] stageSpawnDatas;
}