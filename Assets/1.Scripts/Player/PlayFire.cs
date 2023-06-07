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
    //총구 위치
    Vector3 firePos;
    //총구 방향
    Vector3 dir;
    //ray
    Ray ray;
    //무기의 위치
    public Transform weaponPos;
    //던지는 힘
    public float throwPower = 700;
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
            //총구의 위치 = firePos게임오브젝트의 위치
            firePos = Camera.main.transform.position + Camera.main.transform.forward;
            //총구의 방향 = 메인카메라에서부터 마우스 위치까지의 ray
            dir = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
            //ray 방향으로 총구 위치에서 시작하는 ray
            ray = new Ray(firePos, dir);
            //총알을 소환한다
            GameObject bullet = Instantiate(BulletFactory);
            //총알이 소환되는 위치를 지정
            bullet.transform.position = firePos;
            //총알의 정면방향을 ray의 정면으로 지정
            bullet.transform.forward = ray.direction;
        }
        //만약 플레이어가 마우스 우클릭을 한다면
        if (Input.GetButtonDown("Fire2"))
        {
            //가지고있는 무기를 던진다.
            foreach (Transform child in weaponPos)
            {
                
                //총구의 위치 = firePos게임오브젝트의 위치
                firePos = Camera.main.transform.position + Camera.main.transform.forward;
                //총구의 방향 = 메인카메라에서부터 마우스 위치까지의 ray
                dir = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
                //ray 방향으로 총구 위치에서 시작하는 ray
                ray = new Ray(firePos, dir);
                weaponPos.transform.position = firePos;
                weaponPos.transform.forward = ray.direction;

            }
        }
    }
}
