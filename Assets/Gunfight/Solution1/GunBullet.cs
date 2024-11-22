using UnityEngine;

public class GunBullet : MonoBehaviour
{
    public float damage = 10f;

    private void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞对象是否有Health组件
        Health health = collision.gameObject.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }

        // 销毁子弹
        Destroy(gameObject);
    }
}