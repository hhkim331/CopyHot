using System;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class EnemySpawnData
{
    //���̵�
    public int id = 0;
    //����
    public int step = 0;
    //�����ð�
    public float createTime = 0f;
    //��ġ
    public Vector3 position = Vector3.zero;
    //ȸ������
    public Vector3 rotation = Vector3.zero;
    //�⺻ ������
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