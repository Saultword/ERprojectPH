using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Gamemenuingame : MonoBehaviour
{
    public GameObject menuUI; // 菜单UI
    public Camera vrCamera; // VR摄像机
    public SteamVR_Action_Boolean toggleMenuAction; // 定义一个SteamVR的布尔动作，用于检测A键按下
    public SteamVR_Input_Sources handType = SteamVR_Input_Sources.RightHand;
    //public Hand hand; // 定义一个Hand对象，用于检测手部动作

    private bool isMenuVisible = false; // 菜单是否可见

    // Start is called before the first frame update
    void Start()
    {
        if (toggleMenuAction == null)
        {
            Debug.LogError("Toggle menu action not assigned.");
        }



        if (vrCamera == null)
        {
            Debug.LogError("VR Camera not assigned.");
        }

        menuUI.SetActive(false); // 确保菜单UI初始时是隐藏的
    }

    // Update is called once per frame
    void Update()
    {
        if (toggleMenuAction.GetStateDown(handType))
        {
            ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        isMenuVisible = !isMenuVisible;
        menuUI.SetActive(isMenuVisible);

        if (isMenuVisible)
        {
            // 将菜单放置在VR摄像机前方
            menuUI.transform.position = vrCamera.transform.position + vrCamera.transform.forward * 2;
            menuUI.transform.LookAt(vrCamera.transform);
            menuUI.transform.Rotate(0, 180, 0); // 旋转180度矫正方向
        }
    }

    public void Exitgame()
    {
        Debug.Log("Button Pressed");
        Application.Quit();
    }

    public void Reloadgame()
    {
        string sceneName = "BeginnerAnimation";
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }
}
