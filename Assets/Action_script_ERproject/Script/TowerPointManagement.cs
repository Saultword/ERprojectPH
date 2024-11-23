using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TowerPointManagement : MonoBehaviour
{
    public Image timerBar; // ��ʱ����UI Image���
    private float timeRemaining = 10f; // ��ʱʱ��
    public LayerMask playerLayer; // Player�Ĳ�

    void Start()
    {
        if (timerBar == null)
        {
            Debug.LogError("Timer bar UI Image is not assigned.");
            return;
        }
        StartCoroutine(Timer());
    }

    IEnumerator Timer()
    {
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerBar.fillAmount = timeRemaining / 10f; // ���¼�ʱ��
            yield return null;
        }
        Destroy(gameObject); // 10�������gameobject
    }

    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & playerLayer) != 0)
        {
            equipment playerEquipment = other.GetComponent<equipment>();
            if (playerEquipment != null)
            {
                playerEquipment.bullets += 40;
                playerEquipment.turretModules += 1;
                Destroy(gameObject);
            }
        }
    }
}
