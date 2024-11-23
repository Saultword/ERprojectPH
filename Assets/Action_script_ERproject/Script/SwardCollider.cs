using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SwardCollider : MonoBehaviour
{
    // 定义 SteamVR 动作
    public SteamVR_Action_Vibration hapticAction = SteamVR_Input.GetAction<SteamVR_Action_Vibration>("Haptic");

    // 定义一个公共变量来选择层
    public LayerMask targetLayer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        print("SwordOnCollisionEnter");
        // 检查碰撞的对象是否在指定的层
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            Debug.Log("sword attack hited");
            // 获取手柄设备
            SteamVR_Input_Sources hand = SteamVR_Input_Sources.RightHand; // 可以根据需要指定具体的手柄

            // 触发手柄震动
            hapticAction.Execute(0, 0.5f, 100, 0.5f, hand);
            
        }
    }
}
