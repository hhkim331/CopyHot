using DynamicMeshCutter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : CutterBehaviour
{
    Collider coll;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider>();

        //10초 뒤에 자기 자신을 파괴
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //총알 앞으로 날라가게 하기
        transform.position += transform.forward * Time.deltaTime * 10f;
    }

    //충돌처리
    private void OnCollisionEnter(Collision collision)
    {
        //충돌한 상대방 게임오브젝트의 태그값 비교
        if (collision.gameObject.tag == "Enemy")
        {
            Transform root = collision.transform.root;

            var targets = root.GetComponentsInChildren<MeshTarget>();
            foreach (var target in targets)
            {
                Cut(target, transform.position, transform.up, null, OnCreated);
            }

            ////충돌한 상대방 게임오브젝트 삭제
            //Destroy(collision.gameObject);
            //자기 자신도 삭제
            //Destroy(gameObject);

            coll.enabled = false;


        }
    }

    void OnCreated(Info info, MeshCreationData cData)
    {
        MeshCreation.TranslateCreatedObjects(info, cData.CreatedObjects, cData.CreatedTargets, Separation);
    }
}