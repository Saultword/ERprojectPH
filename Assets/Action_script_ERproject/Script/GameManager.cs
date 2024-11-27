using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class GameManager : MonoBehaviour
{
    public bool isstart = false;
    public GameObject start_menu;
    public GameObject option_menu;
    public GameObject all_menu;
    public GameObject gametitle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setisstarted()
    {
        Debug.Log("Button Pressed");
        isstart = true;
        all_menu.SetActive(false);
        gametitle.SetActive(false);
    }

    public void HideStartMenuAndShowOptionMenu()
    {
        if (start_menu != null && option_menu != null)
        {
            // ����start_menu
            start_menu.SetActive(false);

            // ��option_menu�Ƶ�start_menu��λ��
            option_menu.transform.position = start_menu.transform.position;

            // ��ʾoption_menu
            option_menu.SetActive(true);
        }
    }
    public void HideOptionMenuAndShowStartMenu()
    {
        if (start_menu != null && option_menu != null)
        {
            // ����start_menu
            option_menu.SetActive(false);

            start_menu.SetActive(true);


        }
    }
    public void Exitgame()
    {
        Debug.Log("Button Pressed");
        Application.Quit();
    }
}