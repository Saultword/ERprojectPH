using UnityEngine;
using Valve.VR;
using static UnityEngine.Input;
public class GunController : MonoBehaviour
{
    public Transform gunBarrel; // 枪管的位置
    public GameObject bulletPrefab; // 子弹预制体
    public float bulletSpeed = 100f; // 子弹速度

    public GunGripDetector gunGripDetector; // 引用枪的握把检测器
    public SteamVR_Action_Boolean booleanAction;//20241119更新，添加手柄输入检测，试图替换OVR版本的
    private bool isTriggerPressed = false;

    void Update()
    {

        // 检测扳机是否被按下
        //202411120测试频闭掉gungripdetector isheld
        if (booleanAction.stateDown && gunGripDetector.isHeld)
        {
            Debug.LogError("pressed.");
            if (!isTriggerPressed)
            {
                FireGun();
                isTriggerPressed = true;
            }
        }
        else
        {
            isTriggerPressed = false;
        }
    }

    void FireGun()
    {
        // 生成子弹
        GameObject bullet = Instantiate(bulletPrefab, gunBarrel.position, gunBarrel.rotation);

        // 获取子弹的Rigidbody组件
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        // 检查是否成功获取到Rigidbody组件
        if (rb != null)
        {
            // 设置子弹的速度
            rb.velocity = gunBarrel.forward * bulletSpeed;
            Debug.Log("Bullet fired.");
        }
        else
        {
            Debug.LogError("Bullet prefab does not have a Rigidbody component.");
        }

        // 播放枪声
        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null)
        {
            audio.Play();
        }

        // 播放枪口火焰动画
        ParticleSystem muzzleFlash = GetComponent<ParticleSystem>();
        if (muzzleFlash != null)
        {
            muzzleFlash.Play();
        }
    }
}