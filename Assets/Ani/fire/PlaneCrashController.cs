using UnityEngine;

public class PlaneCrashController : MonoBehaviour
{
    public GameObject bigExplosionEffectPrefab;
    void Start()
    {

    }
    private void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        // 检测到碰撞时触发更大的特效
        GameObject explosion = Instantiate(bigExplosionEffectPrefab, transform.position, transform.rotation);
        // 销毁飞机对象
        Destroy(explosion, 2f);
        Destroy(gameObject);
    }
}
