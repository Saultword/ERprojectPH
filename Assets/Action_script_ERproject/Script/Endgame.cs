using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endgame : MonoBehaviour
{
    public int Basehealth = 4;
    public LayerMask enemyLayer; // ���LayerMask����
    public GameObject gameOverUI; // �����Ϸ����UI
    public GameObject victoryUI; // ���ʤ��UI
    public Camera vrCamera; // ���VR�����
    public bool victory = false;

    // Start is called before the first frame update
    void Start()
    {
        Basehealth = 4;
        gameOverUI.SetActive(false); // ȷ����Ϸ��ʼʱ��Ϸ����UI�����ص�
        victoryUI.SetActive(false); // ȷ����Ϸ��ʼʱʤ��UI�����ص�
    }

    // Update is called once per frame
    void Update()
    {
        if (victory)
        {
            Time.timeScale = 0; // ��ͣ��Ϸ
            victoryUI.SetActive(true); // ��ʾʤ��UI
            victoryUI.transform.position = vrCamera.transform.position + vrCamera.transform.forward * 2; // ��ʤ��UI������VR�����ǰ��
            victoryUI.transform.LookAt(vrCamera.transform); // ʹʤ��UI����VR�����
            victoryUI.transform.Rotate(0, 180, 0); // ��ת180�Ƚ�������
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            Basehealth -= 1;

            if (Basehealth <= 0 && !victory)
            {
                Time.timeScale = 0; // ��ͣ��Ϸ
                gameOverUI.SetActive(true); // ��ʾ��Ϸ����UI
                gameOverUI.transform.position = vrCamera.transform.position + vrCamera.transform.forward * 2; // ����Ϸ����UI������VR�����ǰ��
                gameOverUI.transform.LookAt(vrCamera.transform); // ʹ��Ϸ����UI����VR�����
                gameOverUI.transform.Rotate(0, 180, 0); // ��ת180�Ƚ�������
            }
        }
    }
}
