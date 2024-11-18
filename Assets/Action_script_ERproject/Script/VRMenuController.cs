using UnityEngine;
using Valve.VR;

public class VRMenuController : MonoBehaviour
{
    public SteamVR_Action_Boolean menuAction; // �˵���������
    public GameObject menuCanvas; // �˵�Canvas

    private bool isMenuVisible = false;

    private void Update()
    {
        if (menuAction.GetStateDown(SteamVR_Input_Sources.Any))
        {
            ToggleMenu();
        }
    }

    private void ToggleMenu()
    {
        isMenuVisible = !isMenuVisible;
        menuCanvas.SetActive(isMenuVisible);
    }
}