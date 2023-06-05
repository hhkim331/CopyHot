using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    Enemy enemy;


    public Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemy.e_State)
        {
            case Enemy.E_State.Idle:
                Idle();
                break;
            case Enemy.E_State.Move:
                Move();
                break;
            case Enemy.E_State.Attack:
                Attack();
                break;
            case Enemy.E_State.Stunned:
                Stunned();
                break;
            case Enemy.E_State.PickUp:
                PickUp();
                break;
            case Enemy.E_State.Die:
                Die();
                break;
        }
    }

    public void ChangeState(Enemy.E_State state)
    {
        switch (state)
        {
            case Enemy.E_State.Idle:
                Idle();
                break;
            case Enemy.E_State.Move:
                Move();
                break;
            case Enemy.E_State.Attack:
                Attack();
                break;
            case Enemy.E_State.Stunned:
                Stunned();
                break;
            case Enemy.E_State.PickUp:
                PickUp();
                break;
            case Enemy.E_State.Die:
                Die();
                break;
        }
    }

    public void ChangeWeapon(Weapon.WeaponType weaponType)
    {

    }

    void Idle()
    {
        switch (enemy.e_WeaponType)
        {
            case Weapon.WeaponType.None:
                break;
            case Weapon.WeaponType.Melee:
                break;
            case Weapon.WeaponType.Range:
                break;
        }
    }

    void Move()
    {
        switch (enemy.e_WeaponType)
        {
            case Weapon.WeaponType.None:
                break;
            case Weapon.WeaponType.Melee:
                break;
            case Weapon.WeaponType.Range:
                break;
        }

    }

    void Attack()
    {
        switch (enemy.e_WeaponType)
        {
            case Weapon.WeaponType.None:
                break;
            case Weapon.WeaponType.Melee:
                break;
            case Weapon.WeaponType.Range:
                break;
        }
    }

    void Stunned()
    {

    }

    void PickUp()
    {

    }

    void Die()
    {

    }
}