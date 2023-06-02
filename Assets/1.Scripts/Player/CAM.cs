using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAM : MonoBehaviour
{
    //ȸ�����ɿ���
    public bool useVertical = false;
    public bool useHorizontal = false;
    //���콺�� ȸ���� ����
    float rotX = 0;
    float rotY = 0;
    //���콺�� ȸ�� �ӵ�
    public float rotSpeed = 500f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���콺�� �������� �޾ƿ´�.
        float mx = Input.GetAxis("Mouse X");
        float my = -Input.GetAxis("Mouse Y");

        //���콺�� �������� ����
        //���࿡ useHorizontal�� true ���
        if (useHorizontal == true)
        {
            //�¿��� ȸ������ �����Ѵ�.
            rotX += mx * Time.deltaTime * rotSpeed;
        }
        //���࿡ useVertical�� true��
        if (useVertical == true)
        {
            //���Ʒ��� ȸ������ �����Ѵ�.
            rotY += my * Time.deltaTime * rotSpeed;
        }
        //���Ʒ� ȸ���� ���� ����
        rotY = Mathf.Clamp(rotY, -90f, 90);
        //�����δ�.
        transform.localEulerAngles = new Vector3(rotY, rotX, 0);

    }
}
