using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum WeaponType
    {
        None,
        Melee,
        Range
    }
    public WeaponType weaponType = WeaponType.None;

    public enum W_Owner
    {
        None,
        Player,
        Enemy
    }
    public W_Owner owner = W_Owner.None;

    //공격속도
    public float attackSpeed = 1;
    //공격사거리
    public float attackRange = 2f;
    //공격 쿨타임
    public float attackCoolTime = 2f;
    //공격활성시간
    public float attackActiveTime = 1f;

    public Collider col;
    public Rigidbody rb;

    public bool isThrow = false;

    private void Update()
    {
        transform.forward = transform.root.forward;
    }

    //무기 장착
    public virtual void Set(Transform weaponPos, W_Owner owner)
    {
        this.owner = owner;
        transform.parent = weaponPos;
        transform.localPosition = Vector3.zero;

        col.isTrigger = true;
        col.enabled = false;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }


    //무기 해제
    public virtual void Unset()
    {
        owner = W_Owner.None;
        transform.parent = null;

        col.isTrigger = false;
        col.enabled = true;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
    }

    public virtual void Attack()
    {

    }

    public virtual void AttackEnd()
    {

    }

    public virtual void Throw()
    {
        isThrow = true;

        owner = W_Owner.None;
        transform.parent = null;

        col.isTrigger = false;
        col.enabled = true;
        rb.constraints = RigidbodyConstraints.None;

        //클릭을했을때 날아가게 한다.(리지드바디의 AddForce를 이용하여 힘을 가한다.)
        rb.AddForce(transform.forward * 5, ForceMode.Impulse);
        rb.AddTorque(Vector3.up * 45, ForceMode.Impulse);
        rb.AddTorque(Vector3.right * 45, ForceMode.Impulse);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (!isThrow) return;

        if(collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.transform.root.GetComponent<Enemy>();
            enemy.Hurt();
        }

        else if(collision.gameObject.CompareTag("bullet"))
        {
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        isThrow = false;
        rb.useGravity = true;
    }
}
