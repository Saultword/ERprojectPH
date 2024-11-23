using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TowerPointManagement : MonoBehaviour
{
    public Image timerBar; // 计时条的UI Image组件
    private float timeRemaining = 10f; // 计时时间
    public LayerMask playerLayer; // Player的层

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
            timerBar.fillAmount = timeRemaining / 10f; // 更新计时条
            yield return null;
        }
        Destroy(gameObject); // 10秒后销毁gameobject
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
