using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TowerPointManagement : MonoBehaviour
{
    public Image timerBar; // 计时条的UI Image组件
    private float timeRemaining = 10f; // 计时时间
    public LayerMask playerLayer; // Player的层
    public AudioClip itemSound; // 拾取音效
    private AudioSource audioSource; // 音频源
    public float pickVolume = 1.0f; // 拾取音效音量

    void Start()
    {
        pickVolume = 4.0f;
        audioSource = GetComponent<AudioSource>();
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
                audioSource.PlayOneShot(itemSound, pickVolume);
                StartCoroutine(DestroyAfterSound());
            }
        }
    }

    private IEnumerator DestroyAfterSound()
    {
        yield return new WaitForSeconds(itemSound.length);
        Destroy(gameObject);
    }
}
