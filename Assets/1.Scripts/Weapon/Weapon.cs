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

    public Collider col;
    public Rigidbody rb;

    public bool isThrow = false;

    //무기 장착
    public virtual void Set(Transform weaponPos, W_Owner owner)
    {
        this.owner = owner;
        transform.parent = weaponPos;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

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

    public virtual void Throw()
    {
        isThrow = true;
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
        else if(collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Ground"))
        {
            gameObject.GetComponent<Rigidbody>().useGravity = true;
        }
       

    }
}
