using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;
using System.Collections;
using UnityEngine.UI;

public class GunSteam : MonoBehaviour
{
    public SteamVR_Input_Sources handType; // ʹ�õ���
    public SteamVR_Action_Boolean shootAction; // �������
    public SteamVR_Action_Boolean reloadAction; // װ������
    public GameObject bulletPrefab; // �ӵ�Ԥ����
    public Transform bulletSpawn; // �ӵ�����λ��
    public float bulletSpeed = 20f; // �ӵ��ٶ�
    public int maxAmmo = 10; // ���ҩ��
    public float fireRate = 0.5f; // ���������룩
    public AudioClip shootSound; // �����Ч
    public AudioClip reloadSound; // װ����Ч
    public float shootVolume = 1.0f; // �����Ч����
    public float reloadVolume = 1.0f; // װ����Ч����
    public Canvas bulletUICanvas; // �ӵ�UI��Canvas
    public Text bulletUIText; // ��ʾ�ӵ�������Text
    public equipment playerEquipment; // ����Player��equipment�ű�

    private int currentAmmo; // ��ǰ��ҩ��
    private AudioSource audioSource; // ��ƵԴ
    private float nextFireTime = 0f; // ��һ�������ʱ��
    private Interactable interactable; // �������
    private bool isReloading = false; // �Ƿ�����װ��

    void Start()
    {
        currentAmmo = maxAmmo;
        audioSource = GetComponent<AudioSource>();
        interactable = GetComponent<Interactable>();
        bulletUICanvas.gameObject.SetActive(false); // ��ʼ�����ӵ�UI

        // ���ӵ�UI��Canvas����Ϊǹ���Ӷ��󣬲�������λ��
        bulletUICanvas.transform.SetParent(transform);
        bulletUICanvas.transform.localPosition = new Vector3(0.2f, 0.2f, 0f); // ������Ҫ����λ��
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
        // ֻ����ǹ�������Ҳ���װ��ʱ���������
        if (interactable.attachedToHand != null)
        {
            bulletUICanvas.gameObject.SetActive(true); // ��ʾ�ӵ�UI
            bulletUIText.text = " " + currentAmmo + " / " + playerEquipment.bullets; // �����ӵ�������ʾ
            if (shootAction.GetStateDown(handType) && Time.time >= nextFireTime && !isReloading)
            {
                Shoot();
                nextFireTime = Time.time + fireRate;
            }
        }
        else
        {
            bulletUICanvas.gameObject.SetActive(false); // �����ӵ�UI
        }
    }

    void Shoot()
    {
        if (currentAmmo > 0&&interactable.attachedToHand != null)
        {
            // �����ӵ�
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.velocity = bulletSpawn.forward * bulletSpeed;

            // ���������Ч
            if (shootSound != null)
            {
                audioSource.PlayOneShot(shootSound, shootVolume);
            }

            currentAmmo--;
        }
        else
        {
            // ����װ����Ч
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
