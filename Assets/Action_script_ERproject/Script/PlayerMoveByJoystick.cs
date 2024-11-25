using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayerMoveByJoystick : MonoBehaviour
{
    // ���� SteamVR ����
    public SteamVR_Action_Vector2 moveAction = SteamVR_Input.GetAction<SteamVR_Action_Vector2>("MovePlayerByJoystick");
    public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");
    public SteamVR_Action_Boolean switchAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Switch");

    // �����ƶ��ٶ�
    public float moveSpeed = 2.0f;

    // �����Ƿ�ʹ��teleport�ƶ�
    private bool useTeleport = false;

    // �� SteamVR �� teleporting GameObject
    public GameObject teleportingGameObject;

    // �� VR ���
    public Transform vrCamera;

    // Update is called once per frame
    private void Start()
    {
        //teleportingGameObject.SetActive(useTeleport);
        moveSpeed = 5.0f;
    }
    void Update()
    {
        // ����Ƿ���A���л��ƶ���ʽ
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
            // ����ҡ���ƶ�
            moveAction.RemoveAllListeners(SteamVR_Input_Sources.Any);

            // ����Ƿ���teleport��
            if (teleportAction.GetStateDown(SteamVR_Input_Sources.Any))
            {
                Teleport();
            }
        }
        else
        {
            // ����teleport�ƶ�
            teleportAction.RemoveAllListeners(SteamVR_Input_Sources.Any);

            // ʹ��ҡ���ƶ�
            Vector2 moveValue = moveAction.GetAxis(SteamVR_Input_Sources.Any);
            Vector3 moveDirection = new Vector3(moveValue.x, 0, moveValue.y);

            // ��ȡ VR �����ǰ������
            Vector3 cameraForward = vrCamera.forward;
            cameraForward.y = 0; // ���Դ�ֱ����
            cameraForward.Normalize();

            // �����ƶ�����
            Vector3 move = cameraForward * moveDirection.z + vrCamera.right * moveDirection.x;
            move.y = 0; // ���Դ�ֱ����

            transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);
        }
    }

    void Teleport()
    {
        // ʵ��teleport�߼������罫��Ҵ��͵�Ŀ��λ��
        // ������Ը��ݾ�������ʵ��teleport����
        Debug.Log("Teleporting...");
    }
}
