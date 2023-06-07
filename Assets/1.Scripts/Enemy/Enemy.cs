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

    public Weapon.WeaponType e_WeaponType = Weapon.WeaponType.None;

    GameObject player;
    NavMeshAgent nav;

    //소환 id
    int id;

    //적 애니메이션
    [SerializeField] EnemyAnimatorController enemyAnimatorController;

    [Header("무기 장착 위치")]
    [SerializeField] Transform weaponPos;
    Weapon myWeapon;  //현재 장착 무기

    //스폰 이펙트
    public float spawnEffectTime = 0.5f;

    //공격가능여부
    bool isAttackable = true;
    //공격가능 사거리
    float attackRange = 2f;
    //주먹공격 사거리
    public float punchRange = 2f;
    //공격 딜레이
    float attackDelay = 0f;
    //공격 쿨타임
    float attackCoolTime = 2f;
    //펀치 공격 쿨타임
    public float punchCoolTime = 2f;
    //더미 공격 오브젝트
    public GameObject attackDummy;

    //줍기 사거리
    public float pickUpRange = 1.5f;
    //줍기 딜레이
    float pickUpDelay = 0.5f;
    //줍기 행동 시간
    float pickUpTime = 0f;
    //줍기 타게팅 오브젝트
    GameObject pickUpTarget;

    //스턴상태 지속시간
    float stunDuration = 1f;
    //스턴 시간
    float stunTime = 0f;

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
    }

    void Idle()
    {
        //공격 불가능
        if (isAttackable)
        {
            //플레이어와 거리가 멀어지면 Move로 전환
            float pDistance = Vector3.Distance(transform.position, player.transform.position);
            if (pDistance > attackRange) e_State = E_State.Move;
        }
        else
            e_State = E_State.Move;
    }

    void Move()
    {
        //플레이어와의 거리계산
        float pDistance = Vector3.Distance(transform.position, player.transform.position);

        //총을 들고있는 경우
        if (e_WeaponType == Weapon.WeaponType.Range)
        {
            //총의 사선
            bool fire = Physics.Raycast(weaponPos.position, weaponPos.forward, out RaycastHit fireHit);
            bool range = Physics.Raycast(weaponPos.position, player.transform.position - weaponPos.position, out RaycastHit rangeHit);

            if (fire && fireHit.transform.gameObject == player)
            {
                nav.ResetPath();
                e_State = E_State.Attack;
            }
            else if (range && rangeHit.transform.gameObject == player)
            {
                //플레이어 방향 바라보기
                Vector3 dir = player.transform.position - transform.position;
                dir.y = 0;
                Quaternion rot = Quaternion.LookRotation(dir);
                transform.rotation = Quaternion.Lerp(transform.rotation, rot, 10f * Time.deltaTime);
                nav.ResetPath();
            }
            else
            {
                nav.SetDestination(player.transform.position);
            }
        }
        //근접공격을 해야하는 경우
        else if (pDistance < attackRange)
        {
            //플레이어와 가까워지면 추격중지
            nav.ResetPath();
            //공격 가능 상태인경우 Attack
            if (isAttackable) e_State = E_State.Attack;
            //공격 불가능 상태인경우 Idle
            else e_State = E_State.Idle;
        }
        else
        {
            if (e_WeaponType == Weapon.WeaponType.None)
            {
                //스테이지에 존재하는 모든 무기와의 거리계산
                GameObject[] weapons = GameObject.FindGameObjectsWithTag("Weapon");
                float wDistance = float.MaxValue;
                Vector3 wPos = Vector3.zero;

                //타겟이 존재하는 경우
                if (pickUpTarget != null)
                {
                    //타겟과의 거리를 계산
                    wDistance = Vector3.Distance(transform.position, pickUpTarget.transform.position);

                    //타겟의 소유자가 있는 경우 타겟을 null로 초기화
                    if (pickUpTarget.GetComponent<Weapon>().owner != Weapon.W_Owner.None)
                        pickUpTarget = null;

                    //타겟이 사거리 밖으로 벗어난 경우 타겟을 null로 초기화
                    if (wDistance > detectRange)
                        pickUpTarget = null;

                }

                if (pickUpTarget == null)
                {
                    wDistance = detectRange;
                    foreach (GameObject weapon in weapons)
                    {
                        float curDistance = Vector3.Distance(transform.position, weapon.transform.position);

                        if (curDistance < wDistance)
                        {
                            //주인이 있는 경우 무시
                            if (weapon.GetComponent<Weapon>().owner != Weapon.W_Owner.None)
                                continue;

                            wDistance = curDistance;
                            pickUpTarget = weapon;
                        }
                    }
                }

                if (pickUpTarget != null)
                    wPos = pickUpTarget.transform.position;

                //무기보다 플레이어가 더 가까운경우 플레이어를 추적
                if (pDistance <= wDistance)
                {
                    nav.SetDestination(player.transform.position);
                }
                else if (pickUpTarget != null)
                {
                    if (wDistance <= pickUpRange)
                    {
                        e_State = E_State.PickUp;
                    }
                    else if (wDistance <= detectRange)
                    {
                        nav.SetDestination(wPos);
                        //nav.SetDestination(player.transform.position);
                    }
                }
                else
                {
                    //아무런 해당사항이 없는 경우 추적
                    nav.SetDestination(player.transform.position);
                }
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

        float pDistance = Vector3.Distance(transform.position, player.transform.position);
        //공격 딜레이
        attackDelay += Time.deltaTime;
        switch (e_WeaponType)
        {
            case Weapon.WeaponType.None:
                //공격 가능 사거리에서 플레이어가 벗어난 경우 Move 전환
                if (pDistance > attackRange)
                {
                    e_State = E_State.Move;
                    return;
                }

                //공격 딜레이가 끝난 경우 공격
                if (attackDelay >= attackCoolTime)
                {
                    attackDelay = 0;
                    StartCoroutine(PunchCoroutine());
                    //myWeapon.Attack();
                }
                break;
            case Weapon.WeaponType.Melee:
                //공격 가능 사거리에서 플레이어가 벗어난 경우 Move 전환
                if (pDistance > attackRange)
                {
                    e_State = E_State.Move;
                    return;
                }
                //공격 딜레이가 끝난 경우 공격
                if (attackDelay >= attackCoolTime)
                {
                    attackDelay = 0;
                    myWeapon.Attack();
                }
                break;
            case Weapon.WeaponType.Range:
                //공격 가능 사거리에서 플레이어가 벗어난 경우 Move 전환
                //총의 사선에 플레이어가 있는지 체크
                if (Physics.Raycast(weaponPos.position, weaponPos.forward, out RaycastHit hit))
                {
                    //플레이어가 총의 사선에 있으면 공격
                    if (hit.transform.gameObject == player)
                    {
                        //공격 딜레이가 끝난 경우 공격
                        if (attackDelay >= attackCoolTime)
                        {
                            attackDelay = 0;
                            myWeapon.Attack();
                        }
                    }
                    //플레이어가 총의 사선에 없으면 추적
                    else
                    {
                        e_State = E_State.Move;
                    }
                }
                else
                {
                    e_State = E_State.Move;
                }

                break;
        }
    }

    IEnumerator PunchCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        attackDummy.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        attackDummy.SetActive(false);
    }

    void Stunned()
    {
        stunTime += Time.deltaTime;
        if (stunTime >= stunDuration)
        {
            stunTime = 0;
            e_State = E_State.Idle;
        }
    }

    void PickUp()
    {
        pickUpTime += Time.deltaTime;
        if (pickUpTime <= pickUpDelay)
        {
            //무기를 줍는중
            myWeapon = pickUpTarget.GetComponent<Weapon>();
            myWeapon.Set(weaponPos, Weapon.W_Owner.Enemy);
            e_WeaponType = myWeapon.weaponType;
            attackRange = myWeapon.attackRange;
            attackCoolTime = myWeapon.attackCoolTime;
        }
        else if (pickUpTime <= pickUpDelay * 2)
        {
            //무기를 줍고 일어나는 중
        }
        else
        {
            //줍기 완료
            e_State = E_State.Idle;
        }
    }

    public void Set(EnemySpawnData enemySpawnData, bool immediate)
    {
        id = enemySpawnData.id;

        transform.position = enemySpawnData.position;
        transform.eulerAngles = enemySpawnData.rotation;

        e_State = E_State.Idle;

        //무기 장착
        if(enemySpawnData.defaultWeapon != 0)
        {
            GameObject newWeapon = Instantiate(GameManager.Instance.totalEnemySpawnData.GetWeapon(enemySpawnData.defaultWeapon));
            myWeapon = newWeapon.GetComponent<Weapon>();
            myWeapon.Set(weaponPos, Weapon.W_Owner.Enemy);
            e_WeaponType = myWeapon.weaponType;
            attackRange = myWeapon.attackRange;
            attackCoolTime = myWeapon.attackCoolTime;

            e_WeaponType = myWeapon.weaponType;
        }
        else
            e_WeaponType = Weapon.WeaponType.None;


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

    //무기 떨구기
    void Drop(bool fly)
    {
        if (myWeapon != null)
        {
            myWeapon.Unset();
            //캐릭터가 보고있는 방향의 우측 상단방향으로 힘을 가하기
            if (fly)
                myWeapon.GetComponent<Rigidbody>().AddForce((transform.forward + transform.right * Random.Range(0.1f, 0.5f) + transform.up * Random.Range(0.5f, 1f)).normalized * 5, ForceMode.Impulse);

            myWeapon = null;
            e_WeaponType = Weapon.WeaponType.None;
            attackRange = punchRange;
            attackCoolTime = punchCoolTime;
        }
    }

    public void Hurt()
    {
        Drop(true);
        e_State = E_State.Stunned;
        stunTime = 0;
        nav.ResetPath();
    }

    public void Die()
    {
        Drop(false);
        e_State = E_State.Die;
        nav.ResetPath();

        StageManager.Instance.enemySpawner.CheckStepEnd(id);
        Destroy(gameObject, 3f);
    }
}
