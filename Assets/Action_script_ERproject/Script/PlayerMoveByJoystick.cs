using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerMoveByJoystick : MonoBehaviour
{
    // 定义 SteamVR 动作
    public SteamVR_Action_Vector2 moveAction = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("MovePlayerByJoystick");
    public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
    public SteamVR_Action_Boolean switchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Switch");

    // 定义移动速度
    public float moveSpeed = 2.0f;

    // 定义是否使用teleport移动
    private bool useTeleport = false;

    // 绑定 SteamVR 的 teleporting GameObject
    public GameObject teleportingGameObject;

    // 绑定 VR 相机
    public Transform vrCamera;

    // Update is called once per frame
    private void Start()
    {
        //teleportingGameObject.SetActive(useTeleport);
        moveSpeed = 5.0f;
    }
    void Update()
    {
        // 检查是否按下A键切换移动方式
        if (switchAction.GetStateDown(SteamVR_Input_Sources.Any))
        {
            useTeleport = !useTeleport;
            //teleportingGameObject.SetActive(useTeleport);

            if (useTeleport)
            {
                print("change move type to teleport");
            }
            else
            {
                print("change move type to run");
            }
        }

        if (useTeleport)
        {
            // 禁用摇杆移动
            moveAction.RemoveAllListeners(SteamVR_Input_Sources.Any);

            // 检查是否按下teleport键
            if (teleportAction.GetStateDown(SteamVR_Input_Sources.Any))
            {
                Teleport();
            }
        }
        else
        {
            // 禁用teleport移动
            teleportAction.RemoveAllListeners(SteamVR_Input_Sources.Any);

            // 使用摇杆移动
            Vector2 moveValue = moveAction.GetAxis(SteamVR_Input_Sources.Any);
            Vector3 moveDirection = new Vector3(moveValue.x, 0, moveValue.y);

            // 获取 VR 相机的前向向量
            Vector3 cameraForward = vrCamera.forward;
            cameraForward.y = 0; // 忽略垂直方向
            cameraForward.Normalize();

            // 计算移动方向
            Vector3 move = cameraForward * moveDirection.z + vrCamera.right * moveDirection.x;
            move.y = 0; // 忽略垂直方向

            transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    void Teleport()
    {
        // 实现teleport逻辑，例如将玩家传送到目标位置
        // 这里可以根据具体需求实现teleport功能
        Debug.Log("Teleporting...");
    }
}
