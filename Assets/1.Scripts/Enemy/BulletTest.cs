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

        //10�� �ڿ� �ڱ� �ڽ��� �ı�
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        //�Ѿ� ������ ���󰡰� �ϱ�
        transform.position += transform.forward * Time.deltaTime * 10f;
    }

    //�浹ó��
    private void OnCollisionEnter(Collision collision)
    {
        //�浹�� ���� ���ӿ�����Ʈ�� �±װ� ��
        if (collision.gameObject.tag == "Enemy")
        {
            Transform root = collision.transform.root;

            var targets = root.GetComponentsInChildren<MeshTarget>();
            foreach (var target in targets)
            {
                Cut(target, transform.position, transform.up, null, OnCreated);
            }

            ////�浹�� ���� ���ӿ�����Ʈ ����
            //Destroy(collision.gameObject);
            //�ڱ� �ڽŵ� ����
            //Destroy(gameObject);

            coll.enabled = false;


        }
    }

    void OnCreated(Info info, MeshCreationData cData)
    {
        MeshCreation.TranslateCreatedObjects(info, cData.CreatedObjects, cData.CreatedTargets, Separation);
    }
}