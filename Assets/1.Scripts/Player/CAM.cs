using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAM : MonoBehaviour
{
    //회전가능여부
    public bool useVertical = false;
    public bool useHorizontal = false;
    //마우스의 회전을 누적
    float rotX = 0;
    float rotY = 0;
    //마우스의 회전 속도
    public float rotSpeed = 800f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //마우스의 움직임을 받아온다.
        float mx = Input.GetAxis("Mouse X");
        float my = -Input.GetAxis("Mouse Y");

        //마우스의 움직임을 누적
        //만약에 useHorizontal이 true 라면
        if (useHorizontal == true)
        {
            //좌우의 회전값을 누적한다.
            rotX += mx * Time.deltaTime * rotSpeed;
        }
        //만약에 useVertical이 true면
        if (useVertical == true)
        {
            //위아래로 회전값을 누적한다.
            rotY += my * Time.deltaTime * rotSpeed;
        }
        //위아래 회전의 값을 제한
        rotY = Mathf.Clamp(rotY, -90f, 90);
        //움직인다.
        transform.localEulerAngles = new Vector3(rotY, rotX, 0);

    }
}
