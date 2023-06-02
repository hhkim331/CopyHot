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

    //���� ����Ʈ
    public float spawnEffectTime = 0.5f;


    //���ݰ��ɿ���
    bool isAttackable = true;
    //���ݰ��� ��Ÿ�
    public float attackRange = 1.5f;
    //���� ������
    public float attackDelay = 1f;
    //�����ൿ �ð�
    public float attackTime = 0f;

    //�ݱ� ��Ÿ�
    public float pickUpRange = 1.5f;
    //�νİ��� ��Ÿ�
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
        //�÷��̾�� �Ÿ��� �־����� Move�� ��ȯ
        //���� �Ұ��� ���°� Ǯ���� Attack���� ��ȯ

    }

    void Move()
    {
        //�÷��̾���� �Ÿ����
        float pDistance = Vector3.Distance(transform.position, player.transform.position);

        //���� ����ִ� ���
        if (e_WeaponType == E_WeaponType.Range)
        {
            ////���� �缱�� �÷��̾ �ִ��� üũ
            //if(Physics.Raycast(transform.position, player.transform.position - transform.position, out RaycastHit hit, detectRange))
            //{
            //    //�÷��̾ ���� �缱�� ������ ����
            //    if(hit.transform.gameObject == player)
            //    {
            //        nav.SetDestination(player.transform.position);
            //    }
            //    //�÷��̾ ���� �缱�� ������ Idle
            //    else
            //    {
            //        e_State = E_State.Idle;
            //    }
            //}
        }
        //���������� �ؾ��ϴ� ���
        else if (pDistance < attackRange)
        {
            //�÷��̾�� ��������� �߰�����
            nav.enabled = false;





            //���� ���� �����ΰ�� Attack
            if(isAttackable) e_State = E_State.Attack;
            //���� �Ұ��� �����ΰ�� Idle
            else e_State = E_State.Idle;
        }
        else
        {
            //���������� �����ϴ� ��� ������� �Ÿ����
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

            //���⺸�� �÷��̾ �� ������� �÷��̾ ����
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
                //�ƹ��� �ش������ ���� ��� ����
                nav.SetDestination(player.transform.position);
            }
        }
    }

    void Attack()
    {
        //������ �Ұ����� ������ ��� Idle�� ��ȯ
        if (!isAttackable)
        {
            e_State = E_State.Idle;
            return;
        }

        //���� ���� ��Ÿ����� �÷��̾ ��� ��� Move�� ��ȯ
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
        //���� ����

        if (!immediate)
            SpawnEffect();
    }

    void SpawnEffect()
    {
        //��Ż + ���� �Ұ�����
    }

    void ChangeState()
    {

    }

}
