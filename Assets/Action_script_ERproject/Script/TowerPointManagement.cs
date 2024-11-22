using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TowerPointManagement : MonoBehaviour
{
    public Image timerBar; // ��ʱ����UI Image���
    private float timeRemaining = 10f; // ��ʱʱ��

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
}
