using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetWeapon : MonoBehaviour
{

    //무기공장
    public GameObject gunFactory;
    public GameObject arFactory;
    public GameObject batFactory;
    //들고있는지 확인
    public bool wPos = false;
    //무기를 장착할 위치 지정
    public Transform weaponPos;
    //무기 지정
    public GameObject Weapon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        //키를 누른다.
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //지정된 위치에 오브젝트가 있다면
            if (wPos == true)
            {                
                // 파괴하고
                Destroy(weaponPos.gameObject);
            }
            //지정된 위치에 오브젝트를 새로 꺼내온다
            GameObject Gun = Instantiate(gunFactory);
            //게임오브젝트.복사한다.(게임오브젝트.하이라키창에서("건"), 웨폰포스포지션에, 방향을 그대로).부모 오브젝트 = 
            GameObject.Instantiate(GameObject.Find("Gun(Clone)"), weaponPos.position, Quaternion.identity).transform.parent = this.gameObject.transform;
            wPos = true;
            Debug.Log(wPos);
        }
        //키를 누른다.
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //지정된 위치에 오브젝트가 있다면
            if (wPos == true)
            {
                // 파괴하고
                Destroy(weaponPos.gameObject);
            }
            //지정된 위치로 오브젝트를 새로 꺼내온다
            GameObject AR = Instantiate(arFactory);
            GameObject.Instantiate(GameObject.Find("ar(Clone)"), weaponPos.position, Quaternion.identity).transform.parent = this.gameObject.transform;
            wPos = true;
            Debug.Log(wPos);
        }
        //키를 누른다.
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //지정된 위치에 오브젝트가 있다면
            if (wPos == true)
            {
                // 파괴하고
                Destroy(weaponPos.gameObject);
            }
            //지정된 위치로 오브젝트를 새로 꺼내온다
            GameObject Bat = Instantiate(batFactory);
            GameObject.Instantiate(GameObject.Find("Bat(Clone)"), weaponPos.position, Quaternion.identity).transform.parent = this.gameObject.transform;
            wPos = true;
            Debug.Log(wPos);
        }
    }
    //충돌감지
    //private void OnTriggerStay(Collider other)
    //{
    //    //만약 무기 태그가 달린 물건과 충돌한다면
    //    if (other.tag == "Weapon")
    //    {
    //        //키를 누른다
    //        if (Input.GetButtonDown("Fire1"))
    //        {
    //            //오브젝트를 파괴하고
    //            Destroy(other.gameObject);
    //            //지정된 위치에 오브젝트가 있다면
    //            if (wPos == true)
    //            {
    //                // 파괴하고
    //                Destroy(weaponPos.gameObject);
    //            }
    //            //지정된 위치로 오브젝트를 새로 꺼내온다
    //            GameObject.Instantiate(GameObject.Find("Gun"), weaponPos.position, Quaternion.identity).transform.parent = this.gameObject.transform;
    //            wPos = true;
    //        }

    //    }
    //}

}
