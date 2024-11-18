using UnityEngine;
using Valve.VR;

public class VRMenuController : MonoBehaviour
{
    public SteamVR_Action_Boolean menuAction; // 菜单唤出动作
    public GameObject menuCanvas; // 菜单Canvas

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