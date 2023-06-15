using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWeapon : MonoBehaviour
{
    //카메라의 시선
    public Camera playerCam;
    //광선의 사거리
    public float distance = 2.8f;
    //지정한 레이어콜라이더만 충돌
    public LayerMask Target;
    //무기공장
    public GameObject pistolFactory;
    public GameObject arFactory;
    public GameObject batFactory;
    //들고있는지 확인
    public bool wPos = false;
    public Weapon playerWeapon;
    //무기를 장착할 위치 지정
    public Transform weaponPos;
    public GameObject getWeapon;

    public bool weaponDel = false;
    float setDelay = 0.1f;    //장착 대기시간
    float setDelayTime = 0f;    //장착대기중시간

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (weaponDel)   //장착 대기중이다
        {
            setDelayTime += Time.deltaTime;
            if (setDelayTime > setDelay)
            {
                weaponDel = false;
            }
        }

        //나중에----- 무기가 있는 경우 리턴시킨다.

        pistol();
        ar();
        bat();
        Interaction();
    }

    void pistol()
    {
        //키를 누른다.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //지정된 위치에 오브젝트가 있다면
            if (wPos == true)
            {
                foreach (Transform child in weaponPos)
                {
                    Destroy(child.gameObject);
                }
            }
            //게임오브젝트.복사한다.(게임오브젝트.하이라키창에서("건"), 웨폰포스포지션에, 방향을 그대로).부모 오브젝트 = 
            Weapon weapon = GameObject.Instantiate(pistolFactory, weaponPos.position, Quaternion.identity).GetComponent<Weapon>();
            weapon.Set(weaponPos, global::Weapon.W_Owner.Player);
            wPos = true;
        }

    }

    void ar()
    {
        //키를 누른다.
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //지정된 위치에 오브젝트가 있다면
            if (wPos == true)
            {
                foreach (Transform child in weaponPos)
                {
                    Destroy(child.gameObject);
                }
            }
            //게임오브젝트.복사한다.(게임오브젝트.하이라키창에서("건"), 웨폰포스포지션에, 방향을 그대로).부모 오브젝트 = 
            Weapon weapon = GameObject.Instantiate(arFactory, weaponPos.position, Quaternion.identity).GetComponent<Weapon>();
            weapon.Set(weaponPos, global::Weapon.W_Owner.Player);
            wPos = true;
        }
    }

    void bat()
    {
        //키를 누른다.
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //지정된 위치에 오브젝트가 있다면
            if (wPos == true)
            {
                foreach (Transform child in weaponPos)
                {
                    Destroy(child.gameObject);
                }
            }
            //게임오브젝트.복사한다.(게임오브젝트.하이라키창에서("건"), 웨폰포스포지션에, 방향을 그대로).부모 오브젝트 = 
            Weapon weapon = GameObject.Instantiate(batFactory, weaponPos.position, Quaternion.identity).GetComponent<Weapon>();
            weapon.Set(weaponPos, global::Weapon.W_Owner.Player);
            wPos = true;
        }
    }

    public void Interaction()
    {
        //광선의 발사 위치를 카메라 뷰포트의 정중앙으로 설정
        Vector3 rayOrigin = playerCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));
        //발사방향은 카메라의 앞쪽
        Vector3 rayDir = playerCam.transform.forward;
        //씬화면에서만 볼수있는 광선
        Debug.DrawRay(rayOrigin, rayDir * distance, Color.black);
        //광선에 감지된 콜라이더의 정보를 담는다.
        RaycastHit hit;

        //Raycast에게(발사위치,방향,hit...,광선거리,레이어)를 넘겨준다.
        if (Physics.Raycast(rayOrigin, rayDir, out hit, distance, Target))
        {
            //광선에 충돌 감지된 콜라이더 컴포넌트를 가진 오브젝트를 지정
            GameObject hitTarget = hit.collider.gameObject;
            if(wPos == false)
            {
                if (Input.GetButtonDown("Fire1"))
                {
                    //충동감지된 오브젝트를 자식 오브젝트로 가져온다.
                    playerWeapon = hitTarget.transform.root.GetComponent<Weapon>();
                    playerWeapon.Set(weaponPos, Weapon.W_Owner.Player);
                    wPos = true;
                    ////충돌감지된 오브젝트의 색을 바꿈
                    //hitTarget.GetComponent<Renderer>().material.color = Color.red;

                    //장착딜레이를 시작해라
                    weaponDel = true;
                    //장착 딜레이시간 초기화
                    setDelayTime = 0f;
                }
            }
            
        }
    }

}
