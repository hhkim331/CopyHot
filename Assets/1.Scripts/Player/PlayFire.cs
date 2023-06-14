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
    //레이저
    Ray ray;
    //무기의 위치
    public Transform weaponPos;
    public GetWeapon getWeapon;

    GameObject playerWeapon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Fire1"))
        {
            //총구의 위치 = firePos게임오브젝트의 위치
            firePos = Camera.main.transform.position + Camera.main.transform.forward + (Camera.main.transform.right - Camera.main.transform.up) * 0.1f;
            //총구의 방향 = 메인카메라에서부터 마우스 위치까지의 레이저
            dir = Camera.main.ScreenPointToRay(Input.mousePosition).direction;
            //레이저 방향 = 총구 위치에서 시작
            ray = new Ray(firePos, dir);
            //총알을 소환한다
            Bullet bullet = StageManager.Instance.poolManager.GetFromPool<Bullet>();
            bullet.owner = Weapon.W_Owner.Player;
            //GameObject bullet = Instantiate(BulletFactory);
            //총알이 소환되는 위치를 지정
            bullet.transform.position = firePos;
            //총알의 정면방향을 ray의 정면으로 지정
            bullet.transform.forward = ray.direction;
        }


        //웨폰포스에 무언가 있다.
        //무언가있다면 그것이 무엇인지 확인해라 태그를 체크해서
        //총을 들고있다면 마우스 클릭시 총알을 발사한다.

        //근접무기를 들고있다면 마우스 클릭시 벤다.
        //맨손이라면 아무것도 하지 않는다.
        //던지기
        //마우스 우클릭을하면
        if (Input.GetButtonDown("Fire2"))
        {
            if (getWeapon.playerWeapon != null)
            {
                //던진다
                getWeapon.playerWeapon.Throw();
                getWeapon.wPos = false;
                getWeapon.playerWeapon = null;
            }
        }
    }

}
