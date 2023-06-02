using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어 이동
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * 5f);

        //플레이어 회전
        float r = Input.GetAxisRaw("Mouse X");
        transform.Rotate(new Vector3(0, r, 0) * Time.deltaTime * 500f);

        //탄환 발사
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    //간단한 탄환 발사
    public GameObject bulletFactory;
    public GameObject firePoint;
    void Fire()
    {
        //총알 공장에서 총알 생성
        GameObject bullet = Instantiate(bulletFactory);
        //총알 생성 위치
        bullet.transform.position = firePoint.transform.position;
        //총알 생성 방향
        bullet.transform.forward = firePoint.transform.forward;


    }
}
