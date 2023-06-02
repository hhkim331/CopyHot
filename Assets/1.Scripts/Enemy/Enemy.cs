using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public enum E_State
    {
        Idle,
        Move,
        Attack,
        Stunned,
        PickUp,
        Die
    }
    public E_State e_State = E_State.Idle;

    public enum E_WeaponType
    {
        None,
        Melee,
        Range
    }
    public E_WeaponType e_WeaponType = E_WeaponType.None;

    GameObject player;

    NavMeshAgent nav;

    //스폰 이펙트
    public float spawnEffectTime = 0.5f;


    //공격가능여부
    bool isAttackable = true;
    //공격가능 사거리
    public float attackRange = 1.5f;
    //공격 딜레이
    public float attackDelay = 1f;
    //공격행동 시간
    public float attackTime = 0f;

    //줍기 사거리
    public float pickUpRange = 1.5f;
    //인식가능 사거리
    public float detectRange = 5f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        nav = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (e_State)
        {
            case E_State.Idle:
                Idle();
                break;
            case E_State.Move:
                Move();
                break;
            case E_State.Attack:
                Attack();
                break;
            case E_State.Stunned:
                Stunned();
                break;
            case E_State.PickUp:
                PickUp();
                break;
        }

        nav.SetDestination(player.transform.position);
    }

    void Idle()
    {
        //플레이어와 거리가 멀어지면 Move로 전환
        //공격 불가능 상태가 풀리면 Attack으로 전환

    }

    void Move()
    {
        //플레이어와의 거리계산
        float pDistance = Vector3.Distance(transform.position, player.transform.position);

        //총을 들고있는 경우
        if (e_WeaponType == E_WeaponType.Range)
        {
            ////총의 사선에 플레이어가 있는지 체크
            //if(Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hit, detectRange))
            //{
            //    //플레이어가 총의 사선에 있으면 추적
            //    if(hit.transform.gameObject == player)
            //    {
            //        nav.SetDestination(player.transform.position);
            //    }
            //    //플레이어가 총의 사선에 없으면 Idle
            //    else
            //    {
            //        e_State = E_State.Idle;
            //    }
            //}
        }
        //근접공격을 해야하는 경우
        else if (pDistance < attackRange)
        {
            //플레이어와 가까워지면 추격중지
            nav.enabled = false;





            //공격 가능 상태인경우 Attack
            if(isAttackable) e_State = E_State.Attack;
            //공격 불가능 상태인경우 Idle
            else e_State = E_State.Idle;
        }
        else
        {
            //스테이지에 존재하는 모든 무기와의 거리계산
            GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
            GameObject newWeapon;
            float wDistance = float.MaxValue;

            foreach (GameObject weapon in weapons)
            {
                float curDistance = Vector3.Distance(transform.position, weapon.transform.position);

                if(curDistance < wDistance)
                {
                    wDistance = curDistance;
                    newWeapon = weapon;
                }
            }

            //무기보다 플레이어가 더 가까운경우 플레이어를 추적
            if (pDistance < wDistance)
            {
                nav.SetDestination(player.transform.position);
            }
            else if (wDistance < pickUpRange)
            {
                e_State = E_State.PickUp;
            }
            else if (wDistance < detectRange)
            {
                nav.SetDestination(player.transform.position);
            }
            else
            {
                //아무런 해당사항이 없는 경우 추적
                nav.SetDestination(player.transform.position);
            }
        }
    }

    void Attack()
    {
        //공격이 불가능한 상태인 경우 Idle로 전환
        if (!isAttackable)
        {
            e_State = E_State.Idle;
            return;
        }

        //공격 가능 사거리에서 플레이어가 벗어난 경우 Move로 전환
        float pDistance = Vector3.Distance(transform.position, player.transform.position);
        if (pDistance > attackRange)
        {
            e_State = E_State.Move;
            return;
        }
    }

    void Stunned()
    {

    }

    void PickUp()
    {

    }

    public void Set(EnemySpawnData enemySpawnData, bool immediate)
    {
        transform.position = enemySpawnData.position;
        transform.eulerAngles = enemySpawnData.rotation;
        
        e_State = E_State.Idle;
        e_WeaponType = (E_WeaponType)enemySpawnData.defaultWeapon;
        //무기 장착

        if (!immediate)
            SpawnEffect();
    }

    void SpawnEffect()
    {
        //포탈 + 공격 불가상태
    }

    void ChangeState()
    {

    }

}
