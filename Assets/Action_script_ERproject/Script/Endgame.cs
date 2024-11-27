using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endgame : MonoBehaviour
{
    public int Basehealth = 4;
    public LayerMask enemyLayer; // 添加LayerMask变量
    public GameObject gameOverUI; // 添加游戏结束UI
    public GameObject victoryUI; // 添加胜利UI
    public Camera vrCamera; // 添加VR摄像机
    public bool victory = false;

    // Start is called before the first frame update
    void Start()
    {
        Basehealth = 4;
        gameOverUI.SetActive(false); // 确保游戏开始时游戏结束UI是隐藏的
        victoryUI.SetActive(false); // 确保游戏开始时胜利UI是隐藏的
    }

    // Update is called once per frame
    void Update()
    {
        if (victory)
        {
            Time.timeScale = 0; // 暂停游戏
            victoryUI.SetActive(true); // 显示胜利UI
            victoryUI.transform.position = vrCamera.transform.position + vrCamera.transform.forward * 2; // 将胜利UI放置在VR摄像机前方
            victoryUI.transform.LookAt(vrCamera.transform); // 使胜利UI面向VR摄像机
            victoryUI.transform.Rotate(0, 180, 0); // 旋转180度矫正方向
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            Basehealth -= 1;

            if (Basehealth <= 0 && !victory)
            {
                Time.timeScale = 0; // 暂停游戏
                gameOverUI.SetActive(true); // 显示游戏结束UI
                gameOverUI.transform.position = vrCamera.transform.position + vrCamera.transform.forward * 2; // 将游戏结束UI放置在VR摄像机前方
                gameOverUI.transform.LookAt(vrCamera.transform); // 使游戏结束UI面向VR摄像机
                gameOverUI.transform.Rotate(0, 180, 0); // 旋转180度矫正方向
            }
        }
    }
}
