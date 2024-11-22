using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class GunSteam : MonoBehaviour
{
    public SteamVR_Input_Sources handType; // ʹ�õ���
    public SteamVR_Action_Boolean shootAction; // �������
    public GameObject bulletPrefab; // �ӵ�Ԥ����
    public Transform bulletSpawn; // �ӵ�����λ��
    public float bulletSpeed = 20f; // �ӵ��ٶ�
    public int maxAmmo = 10; // ���ҩ��
    public float fireRate = 0.5f; // ���������룩
    public AudioClip shootSound; // �����Ч
    public AudioClip reloadSound; // װ����Ч

    private int currentAmmo; // ��ǰ��ҩ��
    private AudioSource audioSource; // ��ƵԴ
    private float nextFireTime = 0f; // ��һ�������ʱ��
    private Interactable interactable; // �������

    void Start()
    {
        currentAmmo = maxAmmo;
        audioSource = GetComponent<AudioSource>();
        interactable = GetComponent<Interactable>();
    }

    void Update()
    {
        // ֻ����ǹ������ʱ���������
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
            // �����ӵ�
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = bulletSpawn.forward * bulletSpeed;

            // ���������Ч
            if (shootSound != null)
            {
                audioSource.PlayOneShot(shootSound);
            }

            currentAmmo--;
        }
        else
        {
            // ����װ����Ч
            if (reloadSound != null)
            {
                audioSource.PlayOneShot(reloadSound);
            }

            // ����װ��
            currentAmmo = maxAmmo;
        }
    }
}
