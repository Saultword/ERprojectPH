using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Gamemenuingame : MonoBehaviour
{
    public GameObject menuUI; // �˵�UI
    public Camera vrCamera; // VR�����
    public SteamVR_Action_Boolean toggleMenuAction; // ����һ��SteamVR�Ĳ������������ڼ��A������
    public SteamVR_Input_Sources handType = SteamVR_Input_Sources.RightHand;
    //public Hand hand; // ����һ��Hand�������ڼ���ֲ�����

    private bool isMenuVisible = false; // �˵��Ƿ�ɼ�

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

        menuUI.SetActive(false); // ȷ���˵�UI��ʼʱ�����ص�
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
            // ���˵�������VR�����ǰ��
            menuUI.transform.position = vrCamera.transform.position + vrCamera.transform.forward * 2;
            menuUI.transform.LookAt(vrCamera.transform);
            menuUI.transform.Rotate(0, 180, 0); // ��ת180�Ƚ�������
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
