using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    //총알개수
    public int bulletCount = 4;
    
    //총알이 발사되는 지점
    public GameObject firePosition;

    ////생성할 총알 오브젝트
    //public GameObject BulletFactory;

    private void Update()
    {
        if (isThrow)
            spinMiddle.Rotate(Vector3.right * 1000 * Time.deltaTime);
    }

    public override void Unset()
    {
        base.Unset();
        if(bulletCount==0)
            Destroy(gameObject);
    }

    public override void Attack()
    {
        base.Attack();

        SoundManager.Instance.PlaySFXFromObject(transform.position, "pistol_fire");

        if (owner == W_Owner.Player)
            bulletCount--;

        //총알을 소환한다
        Bullet bullet = StageManager.Instance.poolManager.GetFromPool<Bullet>();
        //GameObject bullet = Instantiate(BulletFactory);
        //총알이 소환되는 위치를 지정
        bullet.owner = owner;
        bullet.transform.position = firePosition.transform.position;
        bullet.transform.forward = firePosition.transform.forward;
    }

    public override void Throw()
    {
        base.Throw();

        //클릭을했을때 날아가게 한다.(리지드바디의 AddForce를 이용하여 힘을 가한다.)
        rb.AddForce(transform.forward * 5, ForceMode.Impulse);
        //rb.AddTorque(Vector3.up * 45, ForceMode.Impulse);
        //rb.AddTorque(Vector3.right * 45, ForceMode.Impulse);
    }
}