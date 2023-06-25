using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPunch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.root.tag == "Player")
        {
            other.GetComponent<PlayerMove>().Die();
        }
    }
}
