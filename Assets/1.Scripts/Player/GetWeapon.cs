using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWeapon : MonoBehaviour
{

    //�������
    public GameObject gunFactory;
    public GameObject arFactory;
    public GameObject batFactory;
    //����ִ��� Ȯ��
    public bool wPos = false;
    //���⸦ ������ ��ġ ����
    public Transform weaponPos;
    //���� ����
    public GameObject Weapon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        //Ű�� ������.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //������ ��ġ�� ������Ʈ�� �ִٸ�
            if (wPos == true)
            {                
                // �ı��ϰ�
                Destroy(weaponPos.gameObject);
            }
            //������ ��ġ�� ������Ʈ�� ���� �����´�
            GameObject Gun = Instantiate(gunFactory);
            //���ӿ�����Ʈ.�����Ѵ�.(���ӿ�����Ʈ.���̶�Űâ����("��"), �������������ǿ�, ������ �״��).�θ� ������Ʈ = 
            GameObject.Instantiate(GameObject.Find("Gun(Clone)"), weaponPos.position, Quaternion.identity).transform.parent = this.gameObject.transform;
            wPos = true;
            Debug.Log(wPos);
        }
        //Ű�� ������.
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //������ ��ġ�� ������Ʈ�� �ִٸ�
            if (wPos == true)
            {
                // �ı��ϰ�
                Destroy(weaponPos.gameObject);
            }
            //������ ��ġ�� ������Ʈ�� ���� �����´�
            GameObject AR = Instantiate(arFactory);
            GameObject.Instantiate(GameObject.Find("ar(Clone)"), weaponPos.position, Quaternion.identity).transform.parent = this.gameObject.transform;
            wPos = true;
            Debug.Log(wPos);
        }
        //Ű�� ������.
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //������ ��ġ�� ������Ʈ�� �ִٸ�
            if (wPos == true)
            {
                // �ı��ϰ�
                Destroy(weaponPos.gameObject);
            }
            //������ ��ġ�� ������Ʈ�� ���� �����´�
            GameObject Bat = Instantiate(batFactory);
            GameObject.Instantiate(GameObject.Find("Bat(Clone)"), weaponPos.position, Quaternion.identity).transform.parent = this.gameObject.transform;
            wPos = true;
            Debug.Log(wPos);
        }
    }
    //�浹����
    //private void OnTriggerStay(Collider other)
    //{
    //    //���� ���� �±װ� �޸� ���ǰ� �浹�Ѵٸ�
    //    if (other.tag == "Weapon")
    //    {
    //        //Ű�� ������
    //        if (Input.GetButtonDown("Fire1"))
    //        {
    //            //������Ʈ�� �ı��ϰ�
    //            Destroy(other.gameObject);
    //            //������ ��ġ�� ������Ʈ�� �ִٸ�
    //            if (wPos == true)
    //            {
    //                // �ı��ϰ�
    //                Destroy(weaponPos.gameObject);
    //            }
    //            //������ ��ġ�� ������Ʈ�� ���� �����´�
    //            GameObject.Instantiate(GameObject.Find("Gun"), weaponPos.position, Quaternion.identity).transform.parent = this.gameObject.transform;
    //            wPos = true;
    //        }

    //    }
    //}

}
