using DynamicMeshCutter;
using Redcode.Pools;
using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Bullet : MonoBehaviour, IPoolObject
{
    public Weapon.W_Owner owner = Weapon.W_Owner.None;

    Collider coll;
    MeshRenderer mesh;
    bool hit = false;
    float lifeTime = 0f;

    TrailRenderer trail;

    //오브젝트 풀안에서 꺼냈는가?
    bool inPool = true;

    public GameObject particle;

    // Start is called before the first frame update
    void Awake()
    {
        coll = GetComponent<Collider>();
        mesh = GetComponent<MeshRenderer>();
        trail = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > 5f && !inPool)
        {
            inPool = true;
            owner = Weapon.W_Owner.None;
            StageManager.Instance.poolManager.TakeToPool<Bullet>("Bullets", this);
            trail.Clear();
        }

        //if (hit) return;
        //총알 앞으로 날라가게 하기
        transform.position += transform.forward * Time.deltaTime * 20f;
    }

    //충돌처리
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PostProcessing")) return;

        if (!hit)
        {
            //충돌한 상대방 게임오브젝트의 태그값 비교
            if (other.transform.root.CompareTag("Enemy"))
            {
                Enemy enemy = other.transform.root.GetComponent<Enemy>();
                if (enemy.e_State != Enemy.E_State.Die)
                {
                    enemy.Die();

                    DeathCutter deathCutter = StageManager.Instance.poolManager.GetFromPool<DeathCutter>();
                    deathCutter.CutTriple(other.transform.root, transform);
                }

                //피격 파티클 생성
                GameObject pObject = Instantiate(particle);
                pObject.transform.position = transform.position;
                pObject.transform.forward = other.transform.root.forward;

                Destroy(pObject, 2f);
            }

            if (other.transform.root.gameObject.CompareTag("Player") && owner == Weapon.W_Owner.Enemy)
            {
                other.transform.root.gameObject.GetComponent<PlayerMove>().Die();
            }
            hit = true;
        }

        ////충돌 비활성화
        //coll.enabled = false;
        //mesh.enabled = false;

        if (other.CompareTag("Door"))
        {
            coll.isTrigger = false;
            mesh.enabled = false;
            trail.enabled = false;
        }
        else
        {
            if (!inPool)
            {
                inPool = true;
                owner = Weapon.W_Owner.None;
                StageManager.Instance.poolManager.TakeToPool<Bullet>("Bullets", this);
            }
        }

        trail.Clear();
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(!hit)
    //    {
    //        //충돌한 상대방 게임오브젝트의 태그값 비교
    //        if (collision.gameObject.tag == "Enemy")
    //        {
    //            Enemy enemy = collision.transform.root.GetComponent<Enemy>();
    //            if (enemy.e_State != Enemy.E_State.Die)
    //            {
    //                enemy.Die();

    //                DeathCutter deathCutter = StageManager.Instance.poolManager.GetFromPool<DeathCutter>();
    //                deathCutter.CutTriple(collision.transform.root, transform);
    //            }
    //        }

    //        if (collision.gameObject.tag == "Player" && owner == Weapon.W_Owner.Enemy)
    //        {
    //            collision.gameObject.GetComponent<PlayerMove>().Die();
    //            //충돌상대가 플레이어면
    //            Destroy(gameObject);
    //        }
    //        hit = true;
    //    }

    //    ////충돌 비활성화
    //    //coll.enabled = false;
    //    //mesh.enabled = false;

    //    if (!inPool)
    //    {
    //        inPool = true;
    //        owner = Weapon.W_Owner.None;
    //        StageManager.Instance.poolManager.TakeToPool<Bullet>("Bullets", this);
    //    }

    //    trail.Clear();
    //}

    public void OnCreatedInPool()
    {
        inPool = false;
        hit = false;
        lifeTime = 0f;
        //coll.enabled = true;
        //mesh.enabled = true;
    }

    public void OnGettingFromPool()
    {
        inPool = false;
        hit = false;
        lifeTime = 0f;
        //coll.enabled = true;
        //mesh.enabled = true;
    }
}