using DynamicMeshCutter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : CutterBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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

            //�Ѿ��� ��ġ�� �߽����� Plane ����
            Vector3 _from = transform.position;
            Vector3 _to = transform.position + transform.forward * 10f;

            Plane plane = new Plane(_from, _to, Camera.main.transform.position);
            var targets = root.GetComponentsInChildren<MeshTarget>();
            foreach (var target in targets)
            {
                Cut(target, _to, plane.normal, null, OnCreated);
            }

            ////�浹�� ���� ���ӿ�����Ʈ ����
            //Destroy(collision.gameObject);
            //�ڱ� �ڽŵ� ����
            Destroy(gameObject);




        }
    }

    //public void Cut()
    //{
    //    var roots = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
    //    foreach (var root in roots)
    //    {
    //        if (!root.activeInHierarchy)
    //            continue;
    //        var targets = root.GetComponentsInChildren<MeshTarget>();
    //        foreach (var target in targets)
    //        {
    //            Cut(target, transform.position, transform.forward, null, OnCreated);
    //        }
    //    }
    //}

    void OnCreated(Info info, MeshCreationData cData)
    {
        MeshCreation.TranslateCreatedObjects(info, cData.CreatedObjects, cData.CreatedTargets, Separation);
    }
}
