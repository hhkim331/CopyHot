using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFire : MonoBehaviour
{
    //생성할 총알 오브젝트
    public GameObject BulletFactory;
    //총알이 발사되는 지점
    public GameObject firePosition;
    //가하는 힘
    public float firePower = 1000f;
    //무기를 들고있는 위치
    public GameObject weanPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //만약에 플레이어가 마우스 왼쪽을 클릭한다면
        if (Input.GetButtonDown("Fire1"))
        {
            
            //총알을 소환한다
            GameObject bullet = Instantiate(BulletFactory);
            //총알이 소환되는 위치를 지정
            //불렛의 위치를 = 총구의 위치
            bullet.transform.position = firePosition.transform.position;
            //불렛의 앞 = 총구의 앞
            bullet.transform.forward = firePosition.transform.forward;
        }
    }
}
