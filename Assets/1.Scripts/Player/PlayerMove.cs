using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float moveSpeed = 2.0f; // �̵� �ӵ�
    public float jumpForce = 5.0f; // �����ϴ� ��
    private bool isGround = true; // ���� �پ��ִ°�?
    Rigidbody rigid; // Rigidbody�� ������ ����

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody�� �����´�.
        rigid = GetComponent<Rigidbody>();           
    }

    // Update is called once per frame
    void Update()
    {
       

        //  Ű���忡 ���� �̵��� ����
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        
        //�̵����� ����
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        Vector3 dir = dirH + dirV;
        // �̵����� ��ǥ�� �ݿ�
        //transform.position = transform.position + dir * moveSpeed * Time.deltaTime;
        rigid.MovePosition(transform.position + dir * moveSpeed * Time.deltaTime);

        // ���࿡ �����̽��ٰ� ���������� �����پ��ִٸ�
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            //�������ڴ�
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //isGround���� �ʱ�ȭ
            isGround = false;
        }

        

    }

    // �浹����
    private void OnCollisionEnter(Collision collision)
    {
        // ���࿡ ���ӿ�����Ʈ�±װ� Ground�� �浹�Ѵٸ�
        if (collision.gameObject.CompareTag("Ground"))
        {
            // isGround���� true�� �Ѵ�.
            isGround = true;
        }

    }
}
