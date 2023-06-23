using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadManager : MonoBehaviour
{

    bool shoot;
    public bool Shoot
    {
        get { return shoot; }
        set
        {
            shoot = value;
            if (value == true) currentTime = 0;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    public static ReloadManager instance;

    public RectTransform crossHair;
    public bool reload = false;

    float currentTime = 0;
    float per;


    float maxCount; //  지정한 시간초 동안 내가 회전하고싶다.
    // Start is called before the first frame update
    void Start()
    {
        shoot = false;

    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        per = (currentTime / PlayFire.instance.attackDelay);
        // 각도 / 0.5 * 100 = 회전 퍼센트
        // 딜레이 퍼센트, 회전퍼센트를 맞춘다.
        // 100퍼센트가 되면 시간이 지나면 회전
        // per값이 100프로가 되면 회전을 종료
        if (shoot == true)
        {
            crossHair.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0, 0, 90), per);
            crossHair.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            GetWeapon.instance.weaponPos.localRotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(-45, 0, 0), per);
            if (currentTime >= PlayFire.instance.attackDelay)
            {
                //현재시간이 최대시간을 넘게되면 초기화
                crossHair.rotation = Quaternion.Euler(0, 0, 0);
                crossHair.localScale = Vector3.one;
                GetWeapon.instance.weaponPos.localRotation = Quaternion.Euler(0,0,0);
                shoot = false;
            }
        }


    }

    public void Reload()
    {
        //crossHair.rotation = Quaternion.Euler(new Vector3(0, 0, 90) * 5 * Time.deltaTime) ;
        crossHair.rotation = Quaternion.Slerp(crossHair.rotation, Quaternion.Euler(0, 0, 180), 20 * Time.unscaledDeltaTime);
    }
}
