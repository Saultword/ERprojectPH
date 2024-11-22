using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GunSteam : MonoBehaviour
{
    public SteamVR_Input_Sources handType; // 使用的手
    public SteamVR_Action_Boolean shootAction; // 射击动作
    public GameObject bulletPrefab; // 子弹预制体
    public Transform bulletSpawn; // 子弹生成位置
    public float bulletSpeed = 20f; // 子弹速度
    public int maxAmmo = 10; // 最大弹药数
    public float fireRate = 0.5f; // 射击间隔（秒）
    public AudioClip shootSound; // 射击音效
    public AudioClip reloadSound; // 装弹音效

    private int currentAmmo; // 当前弹药数
    private AudioSource audioSource; // 音频源
    private float nextFireTime = 0f; // 下一次射击的时间
    private Interactable interactable; // 交互组件

    void Start()
    {
        currentAmmo = maxAmmo;
        audioSource = GetComponent<AudioSource>();
        interactable = GetComponent<Interactable>();
    }

    void Update()
    {
        // 只有在枪被持握时才允许射击
        if (interactable.attachedToHand != null && shootAction.GetStateDown(handType) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (currentAmmo > 0)
        {
            // 创建子弹
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = bulletSpawn.forward * bulletSpeed;

            // 播放射击音效
            if (shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }

            currentAmmo--;
        }
        else
        {
            // 播放装弹音效
            if (reloadSound != null)
            {
                audioSource.PlayOneShot(reloadSound);
            }

            // 重新装弹
            currentAmmo = maxAmmo;
        }
    }
}
