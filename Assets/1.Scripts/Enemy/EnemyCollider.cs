using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    Enemy enemy;
    BoxCollider boxCollider;

    [SerializeField] Transform[] pivots;


    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
        boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy.e_State == Enemy.E_State.Die)
        {
            boxCollider.enabled = false;
            return;
        }

        //가장 높은 피벗과 가장 낮은 피벗의 y값을 구한다.
        float maxY = float.MinValue;
        float minY = float.MaxValue;
        foreach (Transform pivot in pivots)
        {
            if (maxY < pivot.position.y)
                maxY = pivot.position.y;
            if (minY > pivot.position.y)
                minY = pivot.position.y;
        }

        //콜라이더의 높이를 구한다.
        float height = maxY - minY;
        //콜라이더의 높이를 적용한다.
        boxCollider.size = new Vector3(boxCollider.size.x, height, boxCollider.size.z);
        //콜라이더의 중심을 구한다.
        float centerY = minY + (height / 2f);
        //콜라이더의 중심을 적용한다.
        boxCollider.center = new Vector3(boxCollider.center.x, centerY, boxCollider.center.z);
    }
}
