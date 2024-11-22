using UnityEngine;

public class GunGripDetector : MonoBehaviour
{
    public bool isHeld = false;

    private void OnTriggerEnter(Collider other)
    {
        // 检测手部控制器是否进入触发器区域
        if (other.CompareTag("Hand"))
        {
            Debug.Log("gun_helded");
            isHeld = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 检测手部控制器是否离开触发器区域
        if (other.CompareTag("Hand"))
        {
            Debug.Log("unhelded");
            isHeld = false;
        }
    }
}