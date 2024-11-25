using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
using System.Collections;
using UnityEngine.UI;

public class GunSteam : MonoBehaviour
{
    public SteamVR_Input_Sources handType; // 使用的手
    public SteamVR_Action_Boolean shootAction; // 射击动作
    public SteamVR_Action_Boolean reloadAction; // 装弹动作
    public GameObject bulletPrefab; // 子弹预制体
    public Transform bulletSpawn; // 子弹生成位置
    public float bulletSpeed = 20f; // 子弹速度
    public int maxAmmo = 10; // 最大弹药数
    public float fireRate = 0.5f; // 射击间隔（秒）
    public AudioClip shootSound; // 射击音效
    public AudioClip reloadSound; // 装弹音效
    public float shootVolume = 1.0f; // 射击音效音量
    public float reloadVolume = 1.0f; // 装弹音效音量
    public Canvas bulletUICanvas; // 子弹UI的Canvas
    public Text bulletUIText; // 显示子弹数量的Text
    public equipment playerEquipment; // 引用Player的equipment脚本

    private int currentAmmo; // 当前弹药数
    private AudioSource audioSource; // 音频源
    private float nextFireTime = 0f; // 下一次射击的时间
    private Interactable interactable; // 交互组件
    private bool isReloading = false; // 是否正在装弹

    void Start()
    {
        currentAmmo = maxAmmo;
        audioSource = GetComponent<AudioSource>();
        interactable = GetComponent<Interactable>();
        bulletUICanvas.gameObject.SetActive(false); // 初始隐藏子弹UI

        // 将子弹UI的Canvas设置为枪的子对象，并设置其位置
        bulletUICanvas.transform.SetParent(transform);
        bulletUICanvas.transform.localPosition = new Vector3(0.2f, 0.2f, 0f); // 根据需要调整位置
        bulletUICanvas.transform.localRotation = Quaternion.identity;
    }

    void Update()
    {
        if (reloadAction.GetStateDown(handType) && currentAmmo < maxAmmo && !isReloading)
        {
            if (reloadSound != null && interactable.attachedToHand != null)
            {
                isReloading = true;
                audioSource.PlayOneShot(reloadSound, reloadVolume);
                StartCoroutine(ReloadAfterSound(reloadSound.length));
            }
        }
        // 只有在枪被持握且不在装弹时才允许射击
        if (interactable.attachedToHand != null)
        {
            bulletUICanvas.gameObject.SetActive(true); // 显示子弹UI
            bulletUIText.text = " " + currentAmmo + " / " + playerEquipment.bullets; // 更新子弹数量显示
            if (shootAction.GetStateDown(handType) && Time.time >= nextFireTime && !isReloading)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
        else
        {
            bulletUICanvas.gameObject.SetActive(false); // 隐藏子弹UI
        }
    }

    void Shoot()
    {
        if (currentAmmo > 0&&interactable.attachedToHand != null)
        {
            // 创建子弹
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = bulletSpawn.forward * bulletSpeed;

            // 播放射击音效
            if (shootSound != null)
            {
                audioSource.PlayOneShot(shootSound, shootVolume);
            }

            currentAmmo--;
        }
        else
        {
            // 播放装弹音效
            if (reloadSound != null&& interactable.attachedToHand != null)
            {
                isReloading = true;
                audioSource.PlayOneShot(reloadSound, reloadVolume);
                StartCoroutine(ReloadAfterSound(reloadSound.length));
            }
        }
    }

    private IEnumerator ReloadAfterSound(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (playerEquipment.bullets > 0)
        {
            int bulletsNeeded = maxAmmo - currentAmmo;
            int bulletsToReload = Mathf.Min(bulletsNeeded, playerEquipment.bullets, 10);
            currentAmmo += bulletsToReload;
            playerEquipment.bullets -= bulletsToReload;
        }
        isReloading = false;
    }
}
