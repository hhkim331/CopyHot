using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana : Weapon
{
    bool isAttack = false;

    private void Update()
    {
        if (!isAttack) return;

        //소유자가 플레이어인 경우
        if (owner == W_Owner.Player)
        {

        }
        //소유자가 적인 경우
        else if (owner == W_Owner.Enemy)
        {
            //무기 오른쪽에서 왼쪽으로 회전시키기
            transform.localRotation = Quaternion.Euler(0, transform.localRotation.eulerAngles.y - 180 * Time.deltaTime * attackSpeed, 90);
        }

        if (transform.localRotation.eulerAngles.y < 280 && transform.localRotation.eulerAngles.y>90)
        {
            isAttack = false;
            col.enabled = false;
            transform.localRotation = Quaternion.Euler(0, 80, 90);
        }
    }

    public override void Set(Transform weaponPos, W_Owner owner)
    {
        base.Set(weaponPos, owner);
        transform.localRotation = Quaternion.Euler(0, 80, 90);
    }

    public override void Unset()
    {
        base.Unset();
    }

    public override void Attack()
    {
        base.Attack();
        //무기 휘두르기
        isAttack = true;
        col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (owner == W_Owner.Player)
        {
            //적 피격
            if (other.transform.root.tag == "Enemy")
            {
                Enemy enemy = other.transform.root.GetComponent<Enemy>();
                if (enemy.e_State != Enemy.E_State.Die)
                {
                    Debug.Log("적 맞음!");
                    //DeathCutter.Instance.Cut(other.transform.root, transform.position, transform.right);
                    DeathCutter deathCutter = StageManager.Instance.poolManager.GetFromPool<DeathCutter>();
                    deathCutter.Cut(other.transform.root, transform.position, transform.right);
                }
            }
        }
        else if (owner == W_Owner.Enemy)
        {
            //플레이어 피격
            if (other.transform.root.tag == "Player")
            {
                Debug.Log("플레이어 맞음!");
            }
        }
    }
}
