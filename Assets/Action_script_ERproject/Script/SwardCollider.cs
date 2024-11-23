using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class SwardCollider : MonoBehaviour
{
    // ���� SteamVR ����
    public SteamVR_Action_Vibration hapticAction = SteamVR_Input.GetAction<SteamVR_Action_Vibration>("Haptic");

    // ����һ������������ѡ���
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
        // �����ײ�Ķ����Ƿ���ָ���Ĳ�
        if (((1 << collision.gameObject.layer) & targetLayer) != 0)
        {
            Debug.Log("sword attack hited");
            // ��ȡ�ֱ��豸
            SteamVR_Input_Sources hand = SteamVR_Input_Sources.RightHand; // ���Ը�����Ҫָ��������ֱ�

            // �����ֱ���
            hapticAction.Execute(0, 0.5f, 100, 0.5f, hand);
            
        }
    }
}
