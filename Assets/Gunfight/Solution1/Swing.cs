using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 10;
    public float swingThreshold = 1.0f; // 挥砍速度阈值
    public float swingWindow = 0.5f; // 挥砍窗口时间
    private bool isSwinging = false;
    private float swingTimer = 0.0f;

    void Update()
    {
        // 检测挥砍动作
        if (GetComponent<Rigidbody>().velocity.magnitude > swingThreshold)
        {
            isSwinging = true;
            swingTimer = swingWindow;
        }

        // 更新挥砍窗口计时器
        if (isSwinging)
        {
            swingTimer -= Time.deltaTime;
            if (swingTimer <= 0)
            {
                isSwinging = false;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isSwinging && collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}


