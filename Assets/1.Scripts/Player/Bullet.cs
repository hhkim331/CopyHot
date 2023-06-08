using DynamicMeshCutter;
using Redcode.Pools;
using UnityEngine;

public class Bullet : MonoBehaviour, IPoolObject
{
    Collider coll;
    MeshRenderer mesh;
    bool hit = false;
    float lifeTime = 0f;

    //오브젝트 풀안에서 꺼냈는가?
    bool inPool = true;

    // Start is called before the first frame update
    void Awake()
    {
        coll = GetComponent<Collider>();
        mesh = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeTime+=Time.deltaTime;
        if (lifeTime > 5f && !inPool)
        {
            inPool = true;
            StageManager.Instance.poolManager.TakeToPool<Bullet>("Bullets", this);
        }


        if (hit) return;
        //총알 앞으로 날라가게 하기
        transform.position += transform.forward * Time.deltaTime * 20f;
    }

    //충돌처리
    private void OnCollisionEnter(Collision collision)
    {
        //충돌한 상대방 게임오브젝트의 태그값 비교
        if (collision.gameObject.tag == "Enemy" && !hit)
        {
            Enemy enemy = collision.transform.root.GetComponent<Enemy>();
            if(enemy.e_State != Enemy.E_State.Die)
            {
                enemy.Die();

                hit = true;
                Transform root = collision.transform.root;
                DeathCutter deathCutter = StageManager.Instance.poolManager.GetFromPool<DeathCutter>();
                deathCutter.CutTriple(root, transform);
            }
        }

        //충돌 비활성화
        coll.enabled = false;
        mesh.enabled = false;

        if(!inPool)
        {
            inPool = true;
            StageManager.Instance.poolManager.TakeToPool<Bullet>("Bullets", this);
        }
    }

    public void OnCreatedInPool()
    {
    }

    public void OnGettingFromPool()
    {
        inPool = false;
        hit = false;
        lifeTime = 0f;
        coll.enabled = true;
        mesh.enabled = true;
    }
}