using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    //총알개수
    public int bulletCount = 4;
    
    //총알이 발사되는 지점
    public GameObject firePosition;

    //생성할 총알 오브젝트
    public GameObject BulletFactory;

    public override void Unset()
    {
        base.Unset();
        if(bulletCount==0)
            Destroy(gameObject);
    }

    public override void Attack()
    {
        base.Attack();

        if (owner == W_Owner.Player)
            bulletCount--;

        //총알을 소환한다
        GameObject bullet = Instantiate(BulletFactory);
        //총알이 소환되는 위치를 지정
        bullet.transform.position = firePosition.transform.position;
        bullet.transform.forward = firePosition.transform.forward;
    }
}