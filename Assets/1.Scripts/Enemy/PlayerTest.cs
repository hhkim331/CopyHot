using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //�÷��̾� �̵�
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector3(h, 0, v) * Time.deltaTime * 5f);

        //�÷��̾� ȸ��
        float r = Input.GetAxisRaw("Mouse X");
        transform.Rotate(new Vector3(0, r, 0) * Time.deltaTime * 500f);

        //źȯ �߻�
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
    }

    //������ źȯ �߻�
    public GameObject bulletFactory;
    public GameObject firePoint;
    void Fire()
    {
        //�Ѿ� ���忡�� �Ѿ� ����
        GameObject bullet = Instantiate(bulletFactory);
        //�Ѿ� ���� ��ġ
        bullet.transform.position = firePoint.transform.position;
        //�Ѿ� ���� ����
        bullet.transform.forward = firePoint.transform.forward;


    }
}
