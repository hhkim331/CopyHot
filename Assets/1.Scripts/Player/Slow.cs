﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    bool attack;
    bool move;
    float currentTime = 0f;
    float slowTime = 0.2f;
    //시간을 느리게 한다.




    //마우스 왼쪽클릭, 이동,
    float v;
    float h;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (attack)
        {
            //시간 누적
            currentTime += Time.deltaTime;
            //시간이 지나면
            if (currentTime > slowTime)
            {
                //속도를 바꾸겠다
                currentTime = 0;
                attack = false;
            }
        }

        if (Input.GetButtonDown("Fire1"))
        {
            currentTime = 0;
            attack = true;
        }

        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        {
            move = true;
        }
        else
        {
            move = false;
        }
        if(move == false && attack == false)
        {
            Time.timeScale = 0.05f;
        }
        else
        {
            Time.timeScale = 1f;
        }

        //공격
        //이동


        //if (Input.GetButtonDown("Fire1") || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.W))
        //{
        //    Time.timeScale = 1;
        //}
        //else
        //{
        //    Time.timeScale = 0.05f;
        //}
    }
}