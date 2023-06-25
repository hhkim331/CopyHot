using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballBat : Weapon
{
    //내구도
    public int durability = 3;
    bool isAttack = false;

    //활성 시간
    float activeTime = 0;
    //피벗용 부모
    //Transform pivotParent;

    //private void Awake()
    //{
    //    pivotParent = transform.root;
    //}

    private void Update()
    {
        if (isThrow)
            spinMiddle.Rotate(Vector3.right * 1000 * Time.deltaTime);

        //if (!isAttack) return;

        //activeTime+=Time.deltaTime;

        ////소유자가 플레이어인 경우
        //if (owner == W_Owner.Player)
        //{
        //    //무기 오른쪽에서 왼쪽으로 회전시키기
        //    pivotParent.localRotation = Quaternion.Euler(0, pivotParent.localRotation.eulerAngles.y - 180 * Time.deltaTime * attackSpeed, -90);
        //}
        ////소유자가 적인 경우
        //else if (owner == W_Owner.Enemy)
        //{
        //    //무기 오른쪽에서 왼쪽으로 회전시키기
        //}

        //if(activeTime>attackActiveTime)
        //    AttackEnd();
    }

    public override void Set(Transform weaponPos, W_Owner owner)
    {
        base.Set(weaponPos, owner);
        spinMiddle.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public override void Unset()
    {
        base.Unset();
    }

    public override void Attack()
    {
        base.Attack();
        //무기 휘두르기
        activeTime = 0f;
        isAttack = true;
        col.enabled = true;
    }

    public override void AttackEnd()
    {
        base.AttackEnd();
        //무기 휘두르기 끝
        activeTime = 0f;
        isAttack = false;
        col.enabled = false;
    }

    public override void Throw()
    {
        base.Throw();

        //클릭을했을때 날아가게 한다.(리지드바디의 AddForce를 이용하여 힘을 가한다.)
        rb.AddForce(transform.forward * 5, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(owner == W_Owner.Player)
        {
            //적 피격
            if (other.transform.root.tag == "Enemy")
            {
                Enemy enemy = other.transform.root.GetComponent<Enemy>();
                if (enemy.e_State != Enemy.E_State.Die)
                {
                    Debug.Log("적 맞음!");
                    //DeathCutter.Instance.Cut(other.transform.root, transform.position, transform.right);
                    DeathCutter deathCutter = PoolManager.Instance.GetFromPool<DeathCutter>();
                    deathCutter.CutTriple(other.transform.root, transform);
                    durability--;
                }
            }
        }
        else if(owner == W_Owner.Enemy)
        {
            //플레이어 피격
            if (other.transform.root.tag == "Player")
            {
                other.transform.root.GetComponent<PlayerMove>().Die();
            }
        }
    }
}
