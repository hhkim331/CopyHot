using DynamicMeshCutter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCutter : CutterBehaviour
{
    public static DeathCutter Instance;
    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public void Cut(Transform cuttingTarget, Transform cutterTransform)
    {
        var targets = cuttingTarget.GetComponentsInChildren<MeshTarget>();
        foreach (var target in targets)
        {
            Cut(target, cutterTransform.position, cutterTransform.up, null, OnCreated);
        }
    }

    void OnCreated(Info info, MeshCreationData cData)
    {
        MeshCreation.TranslateCreatedObjects(info, cData.CreatedObjects, cData.CreatedTargets, Separation);
    }
}
