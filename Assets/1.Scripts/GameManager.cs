using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public int stage;

    public GameObject gameStartMassage;
    bool gamestart = false;

    public TotalEnemySpawnData totalEnemySpawnData;
    public GameObject slow;
    float gametime;
    public GameObject playercam;
    public GameObject maincamera;

    private void Start()
    {
        //화면 중앙에 커서 고정
        Cursor.lockState = CursorLockMode.Locked;
        //커서 보이지않음
        Cursor.visible = false;
        gameStartMassage.SetActive(true);
        Time.timeScale = 0;
        gamestart = true;
        playercam.GetComponent<CAM>().enabled = false;
        maincamera.GetComponent<CAM>().enabled = false;
    }

    private void Update()
    {
        if (gamestart == true)
        {
            gametime += Time.unscaledTime;
            if (gametime > 3000)
            {
                gameStartMassage.SetActive(false);
                slow.SetActive(true);
                playercam.GetComponent<CAM>().enabled = true;
                maincamera.GetComponent<CAM>().enabled = true;
            }
        }
    }

}
