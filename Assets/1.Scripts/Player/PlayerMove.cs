using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    public float moveSpeed = 2.0f; // 이동 속도
    public float jumpForce = 5.0f; // 점프하는 힘
    private bool isGround = true; // 땅에 붙어있는가?
    Rigidbody rigid; // Rigidbody를 가져올 변수

    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody를 가져온다.
        rigid = GetComponent<Rigidbody>();           
    }
    // Update is called once per frame
    void Update()
    {
        //  키보드에 따른 이동량 측정
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");
        
        //이동값을 저장
        Vector3 dirH = transform.right * h;
        Vector3 dirV = transform.forward * v;
        Vector3 dir = dirH + dirV;
        // 이동값을 좌표에 반영
        //transform.position = transform.position + dir * moveSpeed * Time.deltaTime;
        rigid.MovePosition(transform.position + dir * moveSpeed * Time.deltaTime);

        // 만약에 스페이스바가 눌려있을때 땅에붙어있다면
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            //점프를뛴다
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //isGround값을 초기화
            isGround = false;
        }
    }

    // 충돌감지
    private void OnCollisionEnter(Collision collision)
    {
        // 만약에 게임오브젝트태그가 Ground와 충돌한다면
        if (collision.gameObject.CompareTag("Ground"))
        {
            // isGround값을 true로 한다.
            isGround = true;
        }

    }

}
