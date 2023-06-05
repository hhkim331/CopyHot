using DynamicMeshCutter;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Collider coll;
    MeshRenderer mesh;
    bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider>();
        mesh = GetComponent<MeshRenderer>();

        //10초 뒤에 자기 자신을 파괴
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (hit) return;
        //총알 앞으로 날라가게 하기
        transform.position += transform.forward * Time.deltaTime * 10f;
    }

    //충돌처리
    private void OnCollisionEnter(Collision collision)
    {
        //충돌한 상대방 게임오브젝트의 태그값 비교
        if (collision.gameObject.tag == "Enemy" && !hit)
        {
            collision.transform.root.GetComponent<Enemy>().Hurt();

            hit = true;
            Transform root = collision.transform.root;

            //DeathCutter.Instance.Cut(root, transform);
        }

        //충돌 비활성화
        coll.enabled = false;
        mesh.enabled = false;


        //1초 뒤에 자기 자신을 파괴
        Destroy(gameObject, 1f);
    }
}