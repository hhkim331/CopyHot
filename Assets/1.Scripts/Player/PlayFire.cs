using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayFire : MonoBehaviour
{
    //������ �Ѿ� ������Ʈ
    public GameObject BulletFactory;
    //�Ѿ��� �߻�Ǵ� ����
    public GameObject firePosition;
    //���ϴ� ��
    public float firePower = 1000f;
    //���⸦ ����ִ� ��ġ
    public GameObject weanPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���࿡ �÷��̾ ���콺 ������ Ŭ���Ѵٸ�
        if (Input.GetButtonDown("Fire1"))
        {
            
            //�Ѿ��� ��ȯ�Ѵ�
            GameObject bullet = Instantiate(BulletFactory);
            //�Ѿ��� ��ȯ�Ǵ� ��ġ�� ����
            //�ҷ��� ��ġ�� = �ѱ��� ��ġ
            bullet.transform.position = firePosition.transform.position;
            //�ҷ��� �� = �ѱ��� ��
            bullet.transform.forward = firePosition.transform.forward;
        }
    }
}
