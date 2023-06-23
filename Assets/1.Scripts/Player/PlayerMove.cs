using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 2.0f; // 이동 속도
    public float jumpForce = 5.0f; // 점프하는 힘
    bool isGround = true; // 땅에 붙어있는가?
    Rigidbody rigid; // Rigidbody를 가져올 변수


    public CAM cam1;
    public CAM cam2;
    public PlayFire playFire;
    public GetWeapon getWeapon;



    // Start is called before the first frame update
    void Start()
    {
        // Rigidbody를 가져온다.
        rigid = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        playerMove();
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

    void playerMove()
    {
        //키보드값을 누적시킬 변수선언
        float h;
        float v;
        // 키보드의 입력만 받아옴
        bool w = Input.GetKey(KeyCode.W);
        bool a = Input.GetKey(KeyCode.A);
        bool s = Input.GetKey(KeyCode.S);
        bool d = Input.GetKey(KeyCode.D);
        //입력받을 키보드의 값을 설정
        if (w)
        {
            h = 1;
        }
        else if (s)
        {
            h = -1;
        }
        else
        {
            h = 0;
        }

        if (a)
        {
            v = -1;
        }
        else if (d)
        {
            v = 1;
        }//가만히 있을시 0
        else
        {
            v = 0;
        }
        //이동값을 저장
        Vector3 dirH = transform.right * v;
        Vector3 dirV = transform.forward * h;
        Vector3 dir = dirH + dirV;
        dir.Normalize();
        // 이동값을 좌표에 반영
        //transform.position = transform.position + dir * moveSpeed * Time.deltaTime;
        //rigid.MovePosition(transform.position + dir * moveSpeed * Time.deltaTime);
        rigid.velocity = dir * moveSpeed;

        // 만약에 스페이스바가 눌려있을때 땅에붙어있다면
        if (Input.GetKey(KeyCode.Space) && isGround)
        {
            //점프를뛴다
            rigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            //isGround값을 초기화
            isGround = false;
        }
    }

    //캐릭터가 죽었을때
    public void Die()
    {
        //컴포넌트들을 죽임
        cam1.enabled = false;
        cam2.enabled = false;
        playFire.enabled = false;
        getWeapon.enabled = false;
        this.enabled = false;
        //스테이지매니저에있는 스테이지실패 함수를 가져옴
        StageManager.Instance.StageFALL();
    }


}
