using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //�浿����
    private void OnCollisionEnter(Collision collision)
    {
        //�ı��Ѵ�
        Destroy(gameObject);
    }
}
