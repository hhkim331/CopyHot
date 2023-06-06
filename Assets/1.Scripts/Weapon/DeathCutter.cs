﻿using DynamicMeshCutter;
using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCutter : CutterBehaviour, IPoolObject
{
    //public static DeathCutter Instance;

    Transform targetFather;
    //세번자르기
    Transform tCutterTransform;

    //protected override void Awake()
    //{
    //    base.Awake();
    //    Instance = this;
    //}
    float waitforCut = 0.1f;
    float waitTime = 0f;

    protected override void Update()
    {
        base.Update();

        if(targetFather!=null)
        {
            waitTime += Time.unscaledDeltaTime;
            if (waitTime > waitforCut)
            {
                waitTime = 0f;
                targetFather = null;
                StageManager.Instance.poolManager.TakeToPool<DeathCutter>("Cutters", this);
            }
        }
    }

    public void Cut(Transform cuttingTarget, Vector3 cutterPosition, Vector3 cutterNormal)
    {
        if (targetFather != null) return;
        targetFather = cuttingTarget;
        var targets = cuttingTarget.GetComponentsInChildren<MeshTarget>();
        foreach (var target in targets)
        {
            Cut(target, cutterPosition, cutterNormal, null, OnCreated);
        }
    }

    public void CutTriple(Transform cuttingTarget, Transform cutterTransform)
    {
        if (targetFather != null) return;
        targetFather = cuttingTarget;
        tCutterTransform = cutterTransform;
        var targets = cuttingTarget.GetComponentsInChildren<MeshTarget>();
        foreach (var target in targets)
        {
            Cut(target, cutterTransform.position, cutterTransform.up, null, Cut2);
        }
    }

    void OnCreated(Info info, MeshCreationData cData)
    {
        int cLength = cData.CreatedTargets.Length;
        for (int i = 0; i < cLength; i++)
            cData.CreatedObjects[i].transform.parent = targetFather;

        MeshCreation.TranslateCreatedObjects(info, cData.CreatedObjects, cData.CreatedTargets, Separation);
    }

    void Cut2(Info info, MeshCreationData cData)
    {
        int cLength = cData.CreatedTargets.Length;
        if (cLength == 0) return;
        for (int i = 0; i < cLength; i++)
            cData.CreatedObjects[i].transform.parent = targetFather;

        for (int i = 0; i < cLength; i++)
        {
            Cut(cData.CreatedTargets[i], tCutterTransform.position, (tCutterTransform.up + tCutterTransform.right).normalized, null, Cut3);
        }
    }

    void Cut3(Info info, MeshCreationData cData)
    {
        int cLength = cData.CreatedTargets.Length;
        if (cLength == 0) return;
        for (int i = 0; i < cLength; i++)
            cData.CreatedObjects[i].transform.parent = targetFather;

        for (int i = 0; i < cLength; i++)
        {
            Cut(cData.CreatedTargets[i], tCutterTransform.position, (tCutterTransform.up - tCutterTransform.right).normalized, null, OnCreated);
        }
    }

    public void OnCreatedInPool()
    {
    }

    public void OnGettingFromPool()
    {
    }
}
